using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient.Interface
{
    public interface IBaseGrpcClient<PD, PP, M> where PD : class where PP : class where M : class
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<PageResult<PD>> GetListByPage(PP queryParams);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> Add(M request);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> AddRange(List<M> request);


        /// <summary>
        /// 原始修改编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> Modify(M request);

        /// <summary>
        /// 乐观删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> Remove(RemoveModel request);

        /// <summary>
        /// 单个实体删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> RemoveClear(IdModel request);
        /// <summary>
        /// 批量实体删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> RemoveClearRange(List<IdModel> request);

        /// <summary>
        /// 根据ID查询详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<M> FindById(IdModel request);

        Task<List<M>> FindAll();


        /// <summary>
        /// 返回 1 则代表校验成功通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> ValidAdd_Modify(M request);
        /// <summary>
        /// 返回 1 则代表校验成功通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> ValidDelete(RemoveModel request);

        /// <summary>
        /// 字典修改方式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AjaxResult> ModifyDic(Dictionary<string, string> request);
        Task<AjaxResult> ModifyRangeDic(string jsonArray);

        /// <summary>
        /// 根据id查询指定名称
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<NameByIdDto>> FindNameListByIdList(NameByIdParams request );

        /// <summary>
        /// 根据名称查询满足id-name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<NameByIdDto>> FindIdListByNameContains(IdByNameContainsParams request);

        /// <summary>
        /// 根据实体条件查询满足条件的实体集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<M>> FindAllByEntity(Dictionary<string, string> request);
        /// <summary>
        /// 根据实体条件查询 的唯一实体 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<M> FindByEntity(Dictionary<string, string> request);
    }
}
