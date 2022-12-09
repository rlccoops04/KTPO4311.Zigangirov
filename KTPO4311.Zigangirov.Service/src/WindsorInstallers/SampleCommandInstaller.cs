using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KTPO4311.Zigangirov.Lib.src.SampleCommands;

namespace KTPO4311.Zigangirov.Service.src.WindsorInstallers
{
    class SampleCommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISampleCommand>().ImplementedBy<SampleCommandDecorator>().LifeStyle.Singleton,
                Component.For<ISampleCommand>().ImplementedBy<ExceptionCommandDecorator>().LifeStyle.Singleton,
                Component.For<ISampleCommand>().ImplementedBy<FirstCommand>().LifeStyle.Singleton
                );
        }
    }
}
