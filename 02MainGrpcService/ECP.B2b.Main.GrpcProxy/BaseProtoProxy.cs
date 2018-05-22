using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.Menu;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ECP.B2b.Main.GrpcProxy
{
    /// <summary>
    /// Proto Proxy 提取
    /// </summary>
    /// <typeparam name="PD">分页DTO</typeparam>
    /// <typeparam name="PP">分页Filter</typeparam>
    /// <typeparam name="M">操作上下文实体</typeparam>
    public class BaseProtoBufProxy<PD, PP, M> where PD : class where PP : class where M : class
    {
        public BaseProtoBufProxy(Type serviceType)
        {
            __ServiceName = serviceType.FullName;

            __Method_GetListByPage = GrpcServiceExtensions.BuildMethod<PP, PageResult<PD>>(__ServiceName, "GetListByPage");
            __Method_Add = GrpcServiceExtensions.BuildMethod<M, AjaxResult>(__ServiceName, "Add");
            __Method_AddRange = GrpcServiceExtensions.BuildMethod<List<M>, AjaxResult>(__ServiceName, "AddRange");

            __Method_Modify = GrpcServiceExtensions.BuildMethod<M, AjaxResult>(__ServiceName, "Modify");
            __Method_Remove = GrpcServiceExtensions.BuildMethod<RemoveModel, AjaxResult>(__ServiceName, "Remove");
            __Method_RemoveClear = GrpcServiceExtensions.BuildMethod<IdModel, AjaxResult>(__ServiceName, "RemoveClear");
            __Method_RemoveClearRange = GrpcServiceExtensions.BuildMethod<List<IdModel>, AjaxResult>(__ServiceName, "RemoveClearRange");

            __Method_FindById = GrpcServiceExtensions.BuildMethod<IdModel, M>(__ServiceName, "FindById");

            __Method_FindAll = GrpcServiceExtensions.BuildMethod<NullableParams, List<M>>(__ServiceName, "FindAll");

            __Method_ValidAdd_Modify = GrpcServiceExtensions.BuildMethod<M, string>(__ServiceName, "ValidAdd_Modify");
            __Method_ValidDelete = GrpcServiceExtensions.BuildMethod<RemoveModel, string>(__ServiceName, "ValidDelete");

            __Method_ModifyDic = GrpcServiceExtensions.BuildMethod<Dictionary<string, string>, AjaxResult>(__ServiceName, "ModifyDic");
            __Method_ModifyRangeDic = GrpcServiceExtensions.BuildMethod<string, AjaxResult>(__ServiceName, "ModifyRangeDic");

            __Method_FindNameListByIdList = GrpcServiceExtensions.BuildMethod<NameByIdParams, List<NameByIdDto>>(__ServiceName, "FindNameListByIdList");

            __Method_FindIdListByNameContains = GrpcServiceExtensions.BuildMethod<IdByNameContainsParams, List<NameByIdDto>>(__ServiceName, "FindIdListByNameContains");

            __Method_FindAllByEntity = GrpcServiceExtensions.BuildMethod<Dictionary<string, string>, List<M>>(__ServiceName, "__Method_FindAllByEntity");
            __Method_FindByEntity = GrpcServiceExtensions.BuildMethod<Dictionary<string, string>, M>(__ServiceName, "__Method_FindByEntity");
        }

        public readonly string __ServiceName;

        public readonly Method<PP, PageResult<PD>> __Method_GetListByPage;

        public readonly Method<M, AjaxResult> __Method_Add;
        public readonly Method<List<M>, AjaxResult> __Method_AddRange;

        public readonly Method<M, AjaxResult> __Method_Modify;

        public readonly Method<RemoveModel, AjaxResult> __Method_Remove;
        public readonly Method<IdModel, AjaxResult> __Method_RemoveClear;
        public readonly Method<List<IdModel>, AjaxResult> __Method_RemoveClearRange;

        public readonly Method<IdModel, M> __Method_FindById;

        public readonly Method<NullableParams, List<M>> __Method_FindAll;

        public readonly Method<M, string> __Method_ValidAdd_Modify;
        public readonly Method<RemoveModel, string> __Method_ValidDelete;

        public readonly Method<Dictionary<string, string>, AjaxResult> __Method_ModifyDic;
        public readonly Method<string, AjaxResult> __Method_ModifyRangeDic;


        public readonly Method<NameByIdParams, List<NameByIdDto>> __Method_FindNameListByIdList;

        public readonly Method<IdByNameContainsParams, List<NameByIdDto>> __Method_FindIdListByNameContains;

        public readonly Method<Dictionary<string, string>, List<M>> __Method_FindAllByEntity;
        public readonly Method<Dictionary<string, string>, M> __Method_FindByEntity;

        public ServerServiceDefinition.Builder InitBaseService(IEntityProxyBase<PD, PP, M> serviceImpl)
        {
            var builder = ServerServiceDefinition.CreateBuilder();
            builder.AddMethod(__Method_GetListByPage, serviceImpl.GetListByPage)
            .AddMethod(__Method_Add, serviceImpl.Add)
            .AddMethod(__Method_AddRange, serviceImpl.AddRange)
            .AddMethod(__Method_Modify, serviceImpl.Modify)
            .AddMethod(__Method_Remove, serviceImpl.Remove)
            .AddMethod(__Method_FindById, serviceImpl.FindById)
            .AddMethod(__Method_FindAll, serviceImpl.FindAll)
            .AddMethod(__Method_ValidAdd_Modify, serviceImpl.ValidAdd_Modify)
            .AddMethod(__Method_ValidDelete, serviceImpl.ValidDelete)
            .AddMethod(__Method_ModifyDic, serviceImpl.ModifyDic)
            .AddMethod(__Method_ModifyRangeDic, serviceImpl.ModifyRangeDic)
            .AddMethod(__Method_FindNameListByIdList, serviceImpl.FindNameListByIdList)
            .AddMethod(__Method_FindIdListByNameContains, serviceImpl.FindIdListByNameContains)
            .AddMethod(__Method_FindAllByEntity, serviceImpl.FindAllByEntity)
            .AddMethod(__Method_FindByEntity, serviceImpl.FindByEntity)
            .AddMethod(__Method_RemoveClear, serviceImpl.RemoveClear)
            .AddMethod(__Method_RemoveClearRange, serviceImpl.RemoveClearRange);

            return builder;
        }

    }
    /// <summary>
    /// 基础声明服务
    /// </summary>
    public interface IEntityProxyBase<PD, PP, M> where PD : class where PP : class where M : class
    {
        Task<PageResult<PD>> GetListByPage(PP request, ServerCallContext context);

        Task<AjaxResult> Add(M request, ServerCallContext context);
        Task<AjaxResult> AddRange(List<M> request, ServerCallContext context);

        Task<AjaxResult> Modify(M request, ServerCallContext context);

        Task<AjaxResult> Remove(RemoveModel request, ServerCallContext context);
        Task<AjaxResult> RemoveClear(IdModel request, ServerCallContext context);
        Task<AjaxResult> RemoveClearRange(List<IdModel> request, ServerCallContext context);


        Task<M> FindById(IdModel request, ServerCallContext context);

        Task<List<M>> FindAll(NullableParams request, ServerCallContext context);

        Task<string> ValidAdd_Modify(M request, ServerCallContext context);

        Task<string> ValidDelete(RemoveModel request, ServerCallContext context);

        Task<AjaxResult> ModifyDic(Dictionary<string, string> request, ServerCallContext context);
        Task<AjaxResult> ModifyRangeDic(string request, ServerCallContext context);


        Task<List<NameByIdDto>> FindNameListByIdList(NameByIdParams request, ServerCallContext context);

        Task<List<NameByIdDto>> FindIdListByNameContains(IdByNameContainsParams request, ServerCallContext context);

        Task<List<M>> FindAllByEntity(Dictionary<string, string> request, ServerCallContext context);

        Task<M> FindByEntity(Dictionary<string, string> request, ServerCallContext context);
    }

    /// <summary>
    /// 基础客户端
    /// </summary>
    /// <typeparam name="PD"></typeparam>
    /// <typeparam name="PP"></typeparam>
    /// <typeparam name="M"></typeparam>
    public class EntityProxyClient<PD, PP, M> : ProtoBufBaseClient where PD : class where PP : class where M : class
    {
        public EntityProxyClient(Channel channel, BaseProtoBufProxy<PD, PP, M> baseProxy) : base(channel)
        {
            GetListByPage = (r) => base.MethodTemp(r, baseProxy.__Method_GetListByPage);
            Add = (r) => base.MethodTemp(r, baseProxy.__Method_Add);
            AddRange = (r) => base.MethodTemp(r, baseProxy.__Method_AddRange);

            Modify = (r) => base.MethodTemp(r, baseProxy.__Method_Modify);
            Remove = (r) => base.MethodTemp(r, baseProxy.__Method_Remove);
            FindById = (r) => base.MethodTemp(r, baseProxy.__Method_FindById);
            FindAll = (r) => base.MethodTemp(r, baseProxy.__Method_FindAll);

            ValidAdd_Modify = (r) => base.MethodTemp(r, baseProxy.__Method_ValidAdd_Modify);
            ValidDelete = (r) => base.MethodTemp(r, baseProxy.__Method_ValidDelete);

            ModifyDic = (r) => base.MethodTemp(r, baseProxy.__Method_ModifyDic);
            ModifyRangeDic = (r) => base.MethodTemp(r, baseProxy.__Method_ModifyRangeDic);

            FindNameListByIdList = (r) => base.MethodTemp(r, baseProxy.__Method_FindNameListByIdList);
            FindIdListByNameContains = (r) => base.MethodTemp(r, baseProxy.__Method_FindIdListByNameContains);

            FindAllByEntity = (r) => base.MethodTemp(r, baseProxy.__Method_FindAllByEntity);
            FindByEntity = (r) => base.MethodTemp(r, baseProxy.__Method_FindByEntity);


            RemoveClear = (r) => base.MethodTemp(r, baseProxy.__Method_RemoveClear);
            RemoveClearRange = (r) => base.MethodTemp(r, baseProxy.__Method_RemoveClearRange);

        }

        public Func<PP, PageResult<PD>> GetListByPage;
        public Func<M, AjaxResult> Add;
        public Func<List<M>, AjaxResult> AddRange;
        public Func<M, AjaxResult> Modify;
        public Func<RemoveModel, AjaxResult> Remove;
        public Func<IdModel, M> FindById;
        public Func<NullableParams, List<M>> FindAll;

        public Func<M, string> ValidAdd_Modify;
        public Func<RemoveModel, string> ValidDelete;

        public Func<Dictionary<string, string>, AjaxResult> ModifyDic;
        public Func<string, AjaxResult> ModifyRangeDic;


        public Func<NameByIdParams, List<NameByIdDto>> FindNameListByIdList;
        public Func<IdByNameContainsParams, List<NameByIdDto>> FindIdListByNameContains;
        public Func<Dictionary<string, string>, List<M>> FindAllByEntity;
        public Func<Dictionary<string, string>, M> FindByEntity;


        public Func<IdModel, AjaxResult> RemoveClear;
        public Func<List<IdModel>, AjaxResult> RemoveClearRange;


        public EntityProxyClient<PD, PP, M> _ReturnThis()
        {
            return this;
        }
    }
}
