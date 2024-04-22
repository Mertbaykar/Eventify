using Eventify.Persistence.Entity;
using Eventify.Persistence.Persistence;
using Eventify.Persistence.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eventify.Persistence.EventOperator
{
    internal class EventOperator : IEventOperator
    {
        private readonly IDbContextFactory<EventContext> dbContextFactory;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public EventOperator(IDbContextFactory<EventContext> dbContextFactory, IServiceScopeFactory serviceScopeFactory)
        {
            this.dbContextFactory = dbContextFactory;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<List<EventVM>> GetEventsWitHandlers()
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
            {
                var result = context.Event.Select(@event => new EventVM
                {
                    Id = @event.Id,
                    Data = @event.Data,
                    TypeName = @event.TypeName,
                    LastOccurredAt = @event.LastOccurredAt,
                    TryCount = @event.TryCount,

                    HandleResults = @event.HandleResults.Select(handleResult => new HandleResultVM
                    {
                        Id = handleResult.Id,
                        TryCount = handleResult.TryCount,
                        TypeName = handleResult.TypeName,
                        ErrorMessage = handleResult.ErrorMessage,
                        EventId = handleResult.EventId,
                        LastExecutedAt = handleResult.LastExecutedAt!.Value,
                        Success = handleResult.Success
                    }).ToList()

                }).ToList();

                return result;
            }
        }

        public async Task<HandleResult> TriggerHandler(Guid handlerId)
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
            {

                #region Handler

                var handleResult = await context.HandleResult
                                    .Include(x => x.Event)
                                    .FirstOrDefaultAsync(x => x.Id == handlerId);
                ArgumentNullException.ThrowIfNull(handleResult);


                Type handlerType = Type.GetType(handleResult.TypeName)!;

                ArgumentNullException.ThrowIfNull(handlerType);

                #endregion


                #region Event

                Type eventType = Type.GetType(handleResult.Event.TypeName)!;

                object eventData = JsonSerializer.Deserialize(handleResult.Event.Data, eventType)!;

                if (!(eventData is EventifyEvent))
                    throw new Exception("Type error while evaluating eventdata");

                #endregion

                using (var scope = serviceScopeFactory.CreateScope())
                {

                    // Example: IEventHandler<SomeEvent>
                    var handlerGenericType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    var handlerInstance = scope.ServiceProvider.GetServices(handlerGenericType).FirstOrDefault(x => x.GetType() == handlerType);
                    ArgumentNullException.ThrowIfNull(handlerInstance);

                    await ExecuteHandle(handlerInstance, eventData, handleResult);

                    await context.SaveChangesAsync();
                    return handleResult;
                }
            }
        }

        private async System.Threading.Tasks.Task ExecuteHandle(object handlerInstance, object eventData, HandleResult handleResult)
        {
            try
            {
                var handleMethod = handlerInstance.GetType().GetMethod("Handle");

                await (System.Threading.Tasks.Task)handleMethod.Invoke(handlerInstance, new[] { eventData });
                handleResult.MarkSuccess();
            }
            catch (Exception ex)
            {
                handleResult.MarkFail(ex.Message);
            }
        }
    }
}
