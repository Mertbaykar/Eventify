using Eventify.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.ServiceExtensions
{
    internal static class TypeExtensions
    {
        #region Handler

        public static bool ImplementsHandler(Type type)
        {
            if (!(type.IsClass && !type.IsAbstract)) return false;
            return type.GetInterfaces().Any(IsHandlerInterface);
        }

        public static Type? FindHandlerInterface(Type classType)
        {
            return classType.GetInterfaces().FirstOrDefault(IsHandlerInterface);
        }

        public static bool IsHandlerInterface(Type type)
        {
            if (!(type.IsInterface && type.IsGenericType && type.GenericTypeArguments.Length == 1)) return false;
            return type.GetGenericTypeDefinition() == typeof(IEventHandler<>);
        }

        #endregion


        #region Saga

        public static bool ImplementsSaga(Type type)
        {
            if (!(type.IsClass && !type.IsAbstract)) return false;
            return type.GetInterfaces().Any(IsSagaInterface);
        }

        public static Type? FindSagaInterface(Type classType)
        {
            return classType.GetInterfaces().FirstOrDefault(IsSagaInterface);
        }

        public static bool IsSagaInterface(Type type)
        {
            if (!(type.IsInterface && type.IsGenericType && type.GenericTypeArguments.Length == 1)) return false;
            return type.GetGenericTypeDefinition() == typeof(ISaga<>);
        }

        #endregion
    }
}
