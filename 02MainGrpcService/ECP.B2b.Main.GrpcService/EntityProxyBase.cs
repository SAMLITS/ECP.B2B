using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.Service.Interface;
using ECP.Util.Common;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECP.B2b.ModelDto;
using ECP.Util.Common.EFHelper;
using System.Linq;
using System.Reflection;
using ECP.Util.Common.Extensions;

namespace ECP.B2b.Main.GrpcService
{
    public abstract class EntityProxyBase<PD, PP, M> : IEntityProxyBase<PD, PP, M> where PD : class where PP : class where M : BaseModel, new()
    {
        public Func<IBaseService<M>> baseService;
        public Logger logger = new Logger(typeof(EntityProxyBase<PD, PP, M>));
        public bool IsAutoSelector = true;
        public BaseComMethod<M> baseComMethod;
        public EntityProxyBase(Func<IBaseService<M>> _baseService)
        {
            this.baseService = _baseService;
            baseComMethod = new BaseComMethod<M>(baseService, logger);
        }



        /// <summary>
        /// 分页方法 select 表达树
        /// </summary>
        protected Expression<Func<M, M>> _ListByPageSelector { get; set; } = null;

        public virtual Task<PageResult<PD>> GetListByPage(PP request, ServerCallContext context)
        {
            return baseComMethod.GetListByPage<PD, PP>(request, ExtendQueryListByPage, IsAutoSelector, _ListByPageSelector);
        }
        /// <summary>
        /// 分页扩展 lamada 查询   子类重写
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual IQueryable<M> ExtendQueryListByPage(IQueryable<M> queryable, PP request)
        {
            return queryable;
        }

        public virtual Task<AjaxResult> Add(M request, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            var validRes = this.ValidAdd_Modify(request, context).Result;

            return Task.Run<AjaxResult>(() =>
                {
                    if (validRes == "1")
                    {
                        request = baseService().Insert(request).Result;
                        if (request.ID > 0)
                        {
                            result.Result = DoResult.Success;
                            result.NumberMsg = 3010;
                        }
                        else
                        {
                            result.Result = DoResult.Failed;
                            result.NumberMsg = 3011;
                        }
                        result.Id = request.ID;
                    }
                    else
                    {
                        result.Result = DoResult.ValidError;
                        result.NumberMsg = Convert.ToInt32(validRes);
                    }
                    return result;
                }
            );
        }


        public virtual Task<AjaxResult> AddRange(List<M> request, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            return Task.Run<AjaxResult>(() =>
                {
                    request = baseService().InsertRange(request).Result;
                    if (request[0].ID > 0)
                    {
                        result.Result = DoResult.Success;
                        result.NumberMsg = 3010;
                    }
                    else
                    {
                        result.Result = DoResult.Failed;
                        result.NumberMsg = 3011;
                    }
                    return result;
                }
            );
        }

        public virtual Task<AjaxResult> Modify(M newModel, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            var validRes = this.ValidAdd_Modify(newModel, context).Result;
            return Task.Run<AjaxResult>(() =>
            {
                if (validRes == "1")
                {
                    if (baseService().UpdateModel(newModel).Result)
                    {
                        result.Result = DoResult.Success;
                        result.NumberMsg = 3010;
                    }
                    else
                    {
                        result.Result = DoResult.Failed;
                        result.NumberMsg = 3011;
                    }
                }
                else
                {
                    result.Result = DoResult.ValidError;
                    result.NumberMsg = Convert.ToInt32(validRes);
                }
                return result;
            });
        }

        public virtual Task<AjaxResult> Remove(RemoveModel request, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            var validRes = this.ValidDelete(request, context).Result;
            return Task.Run<AjaxResult>(() =>
            {
                if (validRes == "1")
                {
                    if (baseService().DeleteByFlag(request).Result)
                    {
                        result.Result = DoResult.Success;
                        result.NumberMsg = 3012;
                    }
                    else
                    {
                        result.Result = DoResult.Failed;
                        result.NumberMsg = 3013;
                    }
                }
                else
                {
                    result.Result = DoResult.ValidError;
                    result.NumberMsg = Convert.ToInt32(validRes);
                }
                return result;
            });
        }

        public virtual Task<M> FindById(IdModel request, ServerCallContext context)
        {
            return Task.Run<M>(() =>
            {
                return baseService().Find(request.ID).Result;
            });
        }

        /// <summary>
        /// 针对新增/编辑的校验
        /// </summary>
        /// <param name="request">校验对象</param>
        /// <returns>返回消息编码  1-代表校验通过</returns>
        public virtual Task<string> ValidAdd_Modify(M request, ServerCallContext context)
        {
            return Task.Run(() => EntityAutoMapper.ValidWhereJoinBuild(request, (s) => this.baseService().Count(s).Result));
        }

        /// <summary>
        /// 针对删除的校验
        /// </summary>
        /// <param name="request">校验对象</param>
        /// <returns>返回消息编码  1-代表校验通过</returns>
        public virtual Task<string> ValidDelete(RemoveModel request, ServerCallContext context)
        {
            return Task.Run(() => "1");
        }

        public virtual Task<List<M>> FindAll(NullableParams request, ServerCallContext context)
        {
            return Task.Run(() => this.baseService().FindAll());
        }

        public virtual Task<AjaxResult> ModifyDic(Dictionary<string, string> request, ServerCallContext context)
        {
            //拿到对象并根据字典进行相关字典修改
            M mEntity = baseService().Find(Convert.ToInt32(request["ID"])).Result;
            EntityAutoMapper.ConvertMappintDic(mEntity, request, false);

            AjaxResult result = new AjaxResult();
            var validRes = this.ValidAdd_Modify(mEntity, context).Result;
            return Task.Run<AjaxResult>(() =>
            {
                if (validRes == "1")
                {
                    if (baseService().Update(mEntity).Result)
                    {
                        result.Result = DoResult.Success;
                        result.NumberMsg = 3010;
                    }
                    else
                    {
                        result.Result = DoResult.Failed;
                        result.NumberMsg = 3011;
                    }
                }
                else
                {
                    result.Result = DoResult.ValidError;
                    result.NumberMsg = Convert.ToInt32(validRes);
                }
                return result;
            });
        }

        public virtual Task<AjaxResult> ModifyRangeDic(string jsonArray, ServerCallContext context)
        {
            var request = JsonHelper.ToObject<List<Dictionary<string, string>>>(jsonArray);

            //拿到对象并根据字典进行相关字典修改
            var entityList = baseService().FindAll(m => request.Select(s => Convert.ToInt32(s["ID"])).Contains(m.ID)).Result;
            entityList.ForEach(mEntity =>
            {
                var curDic = request.Where(d => Convert.ToInt32(d["ID"]) == mEntity.ID).First();
                EntityAutoMapper.ConvertMappintDic(mEntity, curDic, false);
            });
            AjaxResult result = new AjaxResult();
            return Task.Run<AjaxResult>(() =>
            {
                if (baseService().UpdateRange(entityList).Result)
                {
                    result.Result = DoResult.Success;
                    result.NumberMsg = 3010;
                }
                else
                {
                    result.Result = DoResult.Failed;
                    result.NumberMsg = 3011;
                }
                return result;
            });
        }

        /// <summary>
        /// 根据传入的list id集合 查询对应的名称集合
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task<List<NameByIdDto>> FindNameListByIdList(NameByIdParams request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                Dictionary<string, string> dictionaryMapping = new Dictionary<string, string>();


                var mList = baseService().FindAll<NameByIdDto>(
                   LambdaSelectBuilder.BuildSelect<M, NameByIdDto>(new Dictionary<string, string> {
                       { "NAME1", request.MappingDbField1 },
                       { "NAME2", request.MappingDbField2 },
                       { "NAME3", request.MappingDbField3 },
                       { "NAME4", request.MappingDbField4 },
                       { "NAME5", request.MappingDbField5 },
                       { "NAME6", request.MappingDbField6 },
                       { "NAME7", request.MappingDbField7 }
            })
                    , p => request.IdList.Contains(p.ID)
                    );
                return mList.Result;
            });
        }

        /// <summary>
        /// 根据传入的名称模糊查询满足模糊的所有ID进行返回
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task<List<NameByIdDto>> FindIdListByNameContains(IdByNameContainsParams request, ServerCallContext context)
        {
            return Task.Run(() =>
            {

                FilterCollection _filterCollection = new FilterCollection();
                Type type = typeof(IdByNameContainsParams);
                Type tType = typeof(M);
                PropertyInfo propertyInfo;

                for (int i = 0; i < request.queryWheres.Count; i++)
                {
                    var fieldName = type.GetProperty($"MappingDbField{i + 1}").GetValue(request);
                    if (fieldName != null)
                    {
                        if (!string.IsNullOrEmpty(request.queryWheres[i].NameVal))
                        {

                            propertyInfo = tType.GetProperty(fieldName.ToString());
                            List<Filter> filter = new List<Filter>
                        {
                         new Filter
                         {
                               PropertyName = fieldName.ToString(),
                               Value = request.queryWheres[i].NameVal.ChangeTypeExtend(propertyInfo)
                         }
                        };

                            if (request.queryWheres[i].IsLike)
                            {
                                filter[0].Operation = AttributeModel.ExpressionOptions.Contains;
                            }
                            else
                            {
                                filter[0].Operation = AttributeModel.ExpressionOptions.Equals;
                            }

                            _filterCollection.Add(filter);
                        }
                        else if (request.queryWheres[i].NameValIntList != null)
                        {
                            List<Filter> filter = new List<Filter>
                            {
                                 new Filter
                                 {
                                       PropertyName = fieldName.ToString(),
                                       Value = request.queryWheres[i].NameValIntList,
                                       Operation = AttributeModel.ExpressionOptions.ContainsList
                                    }
                            };
                            _filterCollection.Add(filter);
                        }
                    }
                }


                List<NameByIdDto> mList = baseService().FindAll(
                     LambdaSelectBuilder.BuildSelect<M, NameByIdDto>(new Dictionary<string, string> {
                       { "NAME1", request.MappingDbField1 },
                       { "NAME2", request.MappingDbField2 },
                       { "NAME3", request.MappingDbField3 },
                       { "NAME4", request.MappingDbField4 },
                       { "NAME5", request.MappingDbField5 },
                       { "NAME6", request.MappingDbField6 },
                       { "NAME7", request.MappingDbField7 }
                     })
                     , LambdaExpressionBuilder.GetExpression<M>(_filterCollection)).Result;

                return mList;
            });
        }

        public Task<List<M>> FindAllByEntity(Dictionary<string, string> request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                FilterCollection _filterCollection = new FilterCollection();
                Type tType = typeof(M);
                PropertyInfo propertyInfo;
                foreach (var key in request.Keys)
                {
                    propertyInfo = tType.GetProperty(key);
                    List<Filter> filter = new List<Filter>
                        {
                         new Filter
                         {
                               PropertyName = key,
                               Value = request[key].ChangeTypeExtend(propertyInfo),
                               Operation= AttributeModel.ExpressionOptions.Equals
                         }
                        };

                    _filterCollection.Add(filter);
                }

                List<M> mList = baseService().FindAll(LambdaExpressionBuilder.GetExpression<M>(_filterCollection)).Result;
                return mList;
            });
        }

        public async Task<M> FindByEntity(Dictionary<string, string> request, ServerCallContext context)
        {
            List<M> list = await this.FindAllByEntity(request, context);
            if (list != null && list.Count > 0)
                return list[0];
            return new M();
        }

        public Task<AjaxResult> RemoveClear(IdModel request, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            var validRes = this.ValidDelete(new RemoveModel() { ID = request.ID }, context).Result;
            return Task.Run<AjaxResult>(() =>
            {
                if (validRes == "1")
                {
                    if (baseService().Delete(request.ID).Result)
                    {
                        result.Result = DoResult.Success;
                        result.NumberMsg = 3012;
                    }
                    else
                    {
                        result.Result = DoResult.Failed;
                        result.NumberMsg = 3013;
                    }
                }
                else
                {
                    result.Result = DoResult.ValidError;
                    result.NumberMsg = Convert.ToInt32(validRes);
                }
                return result;
            });
        }

        public Task<AjaxResult> RemoveClearRange(List<IdModel> request, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            return Task.Run<AjaxResult>(() =>
            {
                if (baseService().DeleteRange(request.Select(s => s.ID).ToList()).Result)
                {
                    result.Result = DoResult.Success;
                    result.NumberMsg = 3012;
                }
                else
                {
                    result.Result = DoResult.Failed;
                    result.NumberMsg = 3013;
                }
                return result;
            });
        }
    }


    public class BaseComMethod<M> where M : BaseModel
    {
        private Func<IBaseService<M>> baseService;
        private Logger logger;
        public BaseComMethod(Func<IBaseService<M>> _baseService, Logger _logger)
        {
            baseService = _baseService;
            logger = _logger;
        }


        public virtual Task<PageResult<PD>> GetListByPage<PD,PP>(PP request, Func<IQueryable<M>, PP, IQueryable<M>> ExtendQueryListByPage, bool IsAutoSelector = true, Expression<Func<M, M>> _ListByPageSelector   = null)
        {
            return Task.Run<PageResult<PD>>(() =>
            {
                try
                {
                    var QueryParams = request.GetType().GetProperty("QueryParams").GetValue(request);
                    var PageInfo = (PageFilter)request.GetType().GetProperty("PageInfo").GetValue(request);


                    PageResult<PD> pageResult;
                    if (_ListByPageSelector == null && IsAutoSelector)
                    {
                        var _ListByPageAutoSelector = LambdaSelectBuilder.BuildSelect<M, PD>();
                        var awaitRes = this.baseService().PageQuery(q => ExtendQueryListByPage(EntityAutoMapper.QueryWhereJoinBuild(q, QueryParams), request), PageInfo, _ListByPageAutoSelector);

                        pageResult = awaitRes.Result.GetJsonConvetPageData(PageInfo);
                    }
                    else
                    {
                        var awaitRes = this.baseService().PageQuery(q => ExtendQueryListByPage(EntityAutoMapper.QueryWhereJoinBuild(q, QueryParams), request), PageInfo, _ListByPageSelector);
                        pageResult = EntityAutoMapper.ConvertMappingList<PD, M>(awaitRes.Result).GetJsonConvetPageData(PageInfo);
                    }

                    return pageResult;
                }
                catch (Exception ex)
                {
                    logger.Error(ex: ex);
                    return new PageResult<PD> { Total = -1 };
                }
            });
        }
    }
}
