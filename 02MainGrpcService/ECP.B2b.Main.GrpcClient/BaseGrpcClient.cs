using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.Main.GrpcClient.Interface;
using ECP.B2b.Main.GrpcProxy;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECP.B2b.ModelDto;

namespace ECP.B2b.Main.GrpcClient
{
    public class BaseGrpcClient<PD, PP, M> : IBaseGrpcClient<PD, PP, M> where PD : class where PP : class,new() where M : class
    {
        protected string _Mname = typeof(M).Name.ToLower();
        protected Func<Channel,EntityProxyClient<PD, PP, M>> _ProxyClient;
        public BaseGrpcClient(Func<Channel, EntityProxyClient<PD, PP, M>> _proxyClient)
        { 
             this._ProxyClient = _proxyClient;
        }


        public async Task<AjaxResult> Add(M request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel (_Mname, "AddService").Result;
                var client =  this._ProxyClient(channel);
                var serverRes = client.Add(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
        public async Task<AjaxResult> AddRange(List<M> request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "AddRangeService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.AddRange(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }


        public async Task<PageResult<PD>> GetListByPage(PP queryParams)
        {
            return await Task.Run<PageResult<PD>>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel (_Mname, "PageService").Result;
                var client = this._ProxyClient(channel); 

                var serverRes = client.GetListByPage(queryParams);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> Modify(M request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ModifyService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.Modify(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> Remove(RemoveModel request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "RemoveService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.Remove(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<M> FindById(IdModel request)
        {
            return await Task.Run<M>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindByIdService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindById(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<List<M>> FindAll()
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindAllService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindAll(new NullableParams());
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<string> ValidAdd_Modify(M request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ValidAdd_ModifyService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.ValidAdd_Modify(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<string> ValidDelete(RemoveModel request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ValidDeleteService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.ValidDelete(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> ModifyDic(Dictionary<string, string> request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ModifyDicService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.ModifyDic(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
        public async Task<AjaxResult> ModifyRangeDic(string jsonArray )
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ModifyRangeDicService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.ModifyRangeDic(jsonArray);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        

        public async Task<List<NameByIdDto>> FindNameListByIdList(NameByIdParams request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindNameListByIdListService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindNameListByIdList(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<List<NameByIdDto>> FindIdListByNameContains(IdByNameContainsParams request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindIdListByNameContainsService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindIdListByNameContains(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<List<M>> FindAllByEntity(Dictionary<string, string> request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindAllByEntityService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindAllByEntity(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<M> FindByEntity(Dictionary<string, string> request)
        {
            return await Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindByEntityService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindByEntity(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> RemoveClear(IdModel request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "RemoveClearService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.RemoveClear(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> RemoveClearRange(List<IdModel> request)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "RemoveClearRangeService").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.RemoveClearRange(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}
