using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.MenuFunction;
using Grpc.Core;  

namespace ECP.B2b.Main.GrpcProxy.System
{

    public static class MenuFunctionProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION> baseProxy;
        static MenuFunctionProxy()
        {
            baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION>(typeof(MenuFunctionProxy));
        }
        //单独服务方法在此 Method 定义.....



        public static ServerServiceDefinition BindService(IMenuFunctionProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....


            return builder.Build();
        }

        public interface IMenuFunctionProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION>
        {
            //单独服务方法在此扩展  .....

        }


        public class MenuFunctionProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION>
        {
            public MenuFunctionProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....

            }

            //单独服务方法在此扩展  .....
        }
    }
}
