using Eventify.Persistence.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Eventify.Persistence.Dashboard.Components.Dashboard
{
    public partial class EventifyDashboard : ComponentBase
    {
        public List<EventVM> Events { get; set; } = new();
        public EventVM SelectedEvent { get; set; }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Events = await EventOperator.GetEventsWitHandlers();
        }

        private async System.Threading.Tasks.Task OpenDetailModal(Guid eventId)
        {
            SelectedEvent = Events.First(x => x.Id == eventId);
            await JS.InvokeVoidAsync("openModal", "detailModal");
        }

        private async System.Threading.Tasks.Task Copy2Clipboard(HandleResultVM handleResult)
        {
            await JS.InvokeVoidAsync("copyToClipboard", handleResult.ErrorMessage);
            handleResult.ShowCopyText = true;
            StateHasChanged();
            await System.Threading.Tasks.Task.Delay(2000);
            handleResult.ShowCopyText = false;
            StateHasChanged();
        }

        private async System.Threading.Tasks.Task CloseDetailModal()
        {
            await JS.InvokeVoidAsync("closeModal", "detailModal");
        }

        private async System.Threading.Tasks.Task OpenSuccessModal()
        {
            await JS.InvokeVoidAsync("openModal", "successModal");
        }

        private async System.Threading.Tasks.Task CloseSuccessModal()
        {
            await JS.InvokeVoidAsync("closeModal", "successModal");
        }

        private async System.Threading.Tasks.Task TriggerHandler(Guid handlerId)
        {
            try
            {
                var handleResult = SelectedEvent.HandleResults.First(x => x.Id == handlerId);
                var result = await EventOperator.TriggerHandler(handlerId);

                handleResult.ErrorMessage = result.ErrorMessage;
                handleResult.LastExecutedAt = result.LastExecutedAt!.Value;
                handleResult.Success = result.Success;
                handleResult.TryCount = result.TryCount;

                await OpenSuccessModal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
