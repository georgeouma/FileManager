using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Service.App_Start
{
    public class ContainerBootstrapper: IContainerAccessor, IDisposable
    {
        ContainerBootstrapper(IWindsorContainer container)
        {
            this.Container = container;
        }

        public IWindsorContainer Container { get; }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer().
                Install(FromAssembly.This());
            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
