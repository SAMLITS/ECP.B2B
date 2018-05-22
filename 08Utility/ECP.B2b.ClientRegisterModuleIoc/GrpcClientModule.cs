using Autofac;
using ECP.B2b.Main.GrpcClient;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClient.System;
using ECP.B2b.Main.GrpcClientGroup.Interface.System; 
using ECP.Util.Common.Cache; 

namespace ECP.B2b.ClientRegisterModuleIoc
{
    /// <summary>
    /// Grpc Client客户端运行时的相关注入
    /// </summary>
    public class GrpcClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MenuClient>().As<IMenuClient>();

            builder.RegisterType<LookUpValuesAllClient>().As<ILookUpValuesAllClient>();
            builder.RegisterType<LookUpValuesClient>().As<ILookUpValuesClient>();
            builder.RegisterType<MessagesClient>().As<IMessagesClient>();
            builder.RegisterType<UserClient>().As<IUserClient>();

            builder.RegisterType<UserMenuClient>().As<IUserMenuClient>();

            builder.RegisterType<MenuFunctionClient>().As<IMenuFunctionClient>();


            builder.RegisterType<UserFunctionClient>().As<IUserFunctionClient>();
        }
    }
}
