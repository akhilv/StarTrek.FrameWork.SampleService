using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using DryIoc;

namespace StarTrek.FrameWork.SampleService.Api.DI
{
    /// <summary>
    /// Dry IOC ServiceLocator - Note that the default one on web is not compatible with .NET Core 2.0
    /// </summary>
    public class DryIocServiceLocator : ServiceLocatorImplBase
    {
        private readonly Container _container;

        public DryIocServiceLocator(Container container)
        {
            this._container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public Container GetContainer()
        {
            return _container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            return key != null ? _container.Resolve(serviceType, key) : _container.Resolve(serviceType);
        }


        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            var serviceEnumType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)_container.Resolve(serviceEnumType, IfUnresolved.Throw);
        }
    }
}
