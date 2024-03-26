using Autofac;

namespace ServerCore
{
    public sealed class ListenerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Listener>().As<IListener>().SingleInstance();
        }
    }
}
