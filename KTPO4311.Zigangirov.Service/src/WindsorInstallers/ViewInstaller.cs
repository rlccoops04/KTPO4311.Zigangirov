using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using KTPO4311.Zigangirov.Service.src.Views;

namespace KTPO4311.Zigangirov.Service.src.WindsorInstallers
{
    class ViewInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IView>().ImplementedBy<ConsoleView>().LifeStyle.Transient);
        }
    }
}
