using ECP.B2b.AttributeModel;
using ECP.Util.Common.EFHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection; 

namespace ECP.Util.Common
{
    public class EntityAutoMapper
    {
        /// <summary>
        /// 将指定对象中的相同字段转换到目标实体对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="source">来源类型</param>
        /// <returns>转化后的目标实体</returns>
        public static T ConvertMapping<T,S>(S source)
        {
            // 构造一个要转换对象实例
            T _Instance = Activator.CreateInstance<T>();

            if (source != null)
            {
                Type _SrcT = source.GetType();
                Type _DestT = typeof(T);

                // 这里搜索目标类型的所有字段
                PropertyInfo[] _DestPropertys = _DestT.GetProperties();

                // 给目标类型中的所有字段根据来源实体进行赋值
                foreach (PropertyInfo property in _DestPropertys)
                {
                    //如果来源对象中存在目标属性才给其赋值
                    if (_SrcT.GetProperty(property.Name) != null)
                        property.SetValue(_Instance, _SrcT.GetProperty(property.Name).GetValue(source));
                }
            }
            return _Instance;
        }

        /// <summary>
        /// 将指定对象集合中的相同字段转换到目标实体集合对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="S">来源类型</typeparam>
        /// <param name="sources">来源集合</param>
        /// <returns>转化后的目标实体</returns>
        public static List<T> ConvertMappingList<T,S>(List<S> sources)
        {
            List<T> list = new List<T>();
            if (sources != null && sources.Count > 0)
            {
                sources.ForEach(s =>
                {
                    list.Add(ConvertMapping<T, S>(s));
                });
            }
            return list;
        }

        /// <summary>
        /// 将字典中的key转换填充到目标实体对象相同字段中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="sourceDic"></param> 
        public static void ConvertMappintDic<T>(T entity,Dictionary<string, string> sourceDic,bool isMatch=true)
        {
            Type tType = typeof(T);
            PropertyInfo property;
            foreach (var key in sourceDic.Keys)
            {
                property = tType.GetProperty(key);
                if (property != null)
                {
                    //进行赋值
                    if (!property.PropertyType.IsGenericType)
                    {
                        //非泛型
                        property.SetValue(entity, Convert.ChangeType(sourceDic[key], property.PropertyType));
                    }
                    else
                    {
                        //泛型Nullable<>
                        Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            if (!string.IsNullOrEmpty(sourceDic[key]))
                            {
                                property.SetValue(entity, Convert.ChangeType(sourceDic[key], Nullable.GetUnderlyingType(property.PropertyType)));
                            }
                            else
                            {
                                property.SetValue(entity,null);
                            }
                        }
                    }
                }
                else
                {
                    if (isMatch)
                    {
                        //要求全词匹配
                        throw new Exception("字典中的相关key在目标实体中不存在！");
                    }
                }
            } 
        }

        /// <summary>
        /// 自动生成查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public static IQueryable<T> QueryWhereJoinBuild<T, V>(IQueryable<T> queryable, V queryParams)
        {
            Type _MainType = typeof(T);
            Type _QueryType = queryParams.GetType();

            FilterCollection _filterCollection = new FilterCollection();

            PropertyInfo[] _QueryPropertys = _QueryType.GetProperties();
            _QueryPropertys.Where(q => !q.Name.Contains("IS_LIKE_") && q.GetCustomAttribute<NotQueryParamsBuildOptionsAttribute>() == null).ToList().ForEach(p =>
                {
                    string queryVal = Convert.ToString(p.GetValue(queryParams));
                    if (!string.IsNullOrEmpty(queryVal))
                    {
                        QueryParamsBuildOptionsAttribute _Qpboa = p.GetCustomAttribute<QueryParamsBuildOptionsAttribute>();
                        if (_Qpboa == null)
                            _Qpboa = new QueryParamsBuildOptionsAttribute(ExpressionOptions.Equals);

                        //DB字段名称   默认使用属性名称
                        string pName = p.Name;
                        PageQueryFieldMappingAttribute _Pqfma = p.GetCustomAttribute<PageQueryFieldMappingAttribute>();
                        if (_Pqfma != null)
                        {
                            pName = _Pqfma.EntityFieldName;
                        }

                        List<Filter> filters = new List<Filter> {
                        //同一个List<Filter>中的Filter将采用 OR
                                new Filter()
                                {
                                     PropertyName = pName,
                                     Value = p.GetValue(queryParams)
                                }
                            };

                        if (_Qpboa._Options == ExpressionOptions.Normal)
                        {
                            //不用特性指定的实体名称   就对应使用查询条件字段名称
                            if (Convert.ToBoolean(_QueryType.GetProperty("IS_LIKE_" + p.Name).GetValue(queryParams)))
                            {
                                //模糊查询
                                filters[0].Operation = ExpressionOptions.Contains;
                            }
                            else
                            {
                                //全词查询
                                filters[0].Operation = ExpressionOptions.Equals;
                            }
                        }
                        else
                        {
                            filters[0].Operation = _Qpboa._Options;
                        }


                        _filterCollection.Add(filters);
                    }
                });

            //生成条件Lambda表达树
            if (_filterCollection.Count > 0)
            {
                queryable = queryable.Where(LambdaExpressionBuilder.GetExpression<T>(_filterCollection));
            }


            //是否自动根据有效标识进行有效过滤  默认查询全部
            bool IS_QUERY_AVAIL = false;
            PropertyInfo availProperty =  _QueryType.GetProperty("IS_QUERY_AVAIL");
            if(availProperty!=null)
            {
                IS_QUERY_AVAIL = (bool)availProperty.GetValue(queryParams);
                if (IS_QUERY_AVAIL)
                    return AvailWhereJoinBuild(queryable);
            }
            return queryable;
        }

        /// <summary>
        /// 自动校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tModel"></param>
        /// <param name="funcValid"></param>
        /// <returns></returns>
        public static string ValidWhereJoinBuild<T>(T tModel, Func<Expression<Func<T, bool>>, int> funcValid)
        {
            Type type = typeof(T);
            IdentityGroupUniqueAttribute _Igua = type.GetCustomAttribute<IdentityGroupUniqueAttribute>();
            if (_Igua == null)
                return "1";
            
            var IdVal = type.GetProperty("ID").GetValue(tModel);
            List<Filter> IdFilterList = new List<Filter>
                {
                    new Filter{ Operation= ExpressionOptions.NotEquals, PropertyName="ID", Value=IdVal }
                };

            string[] singGroupArray = _Igua.groupUniqueArray.Split("|");
            FilterCollection _filterCollection = new FilterCollection();
            for (int i = 0; i < singGroupArray.Length; i++)
            {
                _filterCollection.Clear();

                //默认加上 ID！=ID
                _filterCollection.Add(IdFilterList);

                //一组
                string[] singArray = singGroupArray[i].Split(",");
                //第0个是消息编码
                for (int j = 1; j < singArray.Length; j++)
                {
                    bool IsNullIf = false;
                    string propertyName = singArray[j];
                    if(propertyName.IndexOf("[NULL]")>=0)
                    {
                        propertyName = propertyName.Replace("[NULL]", "");
                        IsNullIf = true;
                    }

                    var propertyVal = type.GetProperty(propertyName).GetValue(tModel);

                    if (IsNullIf)
                    {
                        if (propertyVal != null)
                            WhereAppend();
                    }
                    else
                    {
                        WhereAppend();
                    }

                    void WhereAppend() {
                        _filterCollection.Add(new List<Filter>
                        {
                            new Filter{ Operation= ExpressionOptions.Equals, PropertyName=propertyName, Value= propertyVal  }
                        });
                    }
                }


                //只有ID!=ID
                if (_filterCollection.Count == 1)
                    return "1";

                int validCount = funcValid(LambdaExpressionBuilder.GetExpression<T>(_filterCollection));
                if (validCount > 0)
                    return singArray[0];
            }

            return "1";
        }


        /// <summary>
        /// 数据有效查询条件生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public static IQueryable<T> AvailWhereJoinBuild<T>(IQueryable<T> queryable)
        {
            Type _MainType = typeof(T); 
            FilterCollection _filterCollection = new FilterCollection();

            AvailRemarkAttribute[] availList = (AvailRemarkAttribute[]) _MainType.GetCustomAttributes<AvailRemarkAttribute>();

            if(availList==null|| availList.Length==0)
            {
                throw new Exception("未找到校验数据有效性的特性标识 [AvailRemarkAttribute] ！");
            }
            else
            {
                availList.ToList().ForEach(a=>
                {
                    //校验字段名称是否存在
                    if (_MainType.GetProperty(a._bindProperName) == null)
                    {
                        throw new Exception($"未在 [{_MainType.Name}] 实体中找到需要做数据有效性校验的字段 [{a._bindProperName}] ！");
                    }
                    else
                    {
                        //同一个List<Filter>中的Filter将采用 OR
                        List<Filter> filters = new List<Filter>();
                      

                        if(a._avaliType== AvaliType.Equals)
                        {
                            filters.Add(new Filter()
                            {
                                PropertyName = a._bindProperName,
                                Value = a._bindProperValue,
                                Operation = ExpressionOptions.Equals
                            });
                        }
                        else if(a._avaliType == AvaliType.ContainsList)
                        {
                            filters.Add(new Filter()
                            {
                                PropertyName = a._bindProperName,
                                Value = ((string[])a._bindProperValue).ToList(),
                                Operation = ExpressionOptions.ContainsList
                            });
                        }
                        else if(a._avaliType == AvaliType.StartDate)
                        {
                            filters.Add(new Filter()
                            {
                                PropertyName = a._bindProperName,
                                Value = Convert.ToDateTime(a._bindProperValue),
                                Operation = ExpressionOptions.LessThanOrEqual
                            });
                        }
                        else if(a._avaliType == AvaliType.EndDate)
                        {
                            filters.Add(new Filter()
                            {
                                PropertyName = a._bindProperName,
                                Value = Convert.ToDateTime(a._bindProperValue),
                                Operation = ExpressionOptions.GreaterThanOrEqual
                            });
                        }

                        if(a._isContainsNull)
                        {
                            filters.Add(new Filter()
                            {
                                PropertyName = a._bindProperName,
                                Value = null,
                                Operation = ExpressionOptions.Equals
                            });
                        }

                        _filterCollection.Add(filters);
                    }
                });
            }

            //生成条件Lambda表达树
            if (_filterCollection.Count > 0)
            {
                queryable = queryable.Where(LambdaExpressionBuilder.GetExpression<T>(_filterCollection));
            }
            return queryable;
        }



        #region 用于protobuf与业务类之间的转换   基本已废弃

        /// <summary>
        /// 针对程序实体转换为ProtoBuf实体
        /// </summary>
        /// <typeparam name="T">ProtoBuf目标类型</typeparam>
        /// <param name="source">程序来源实体类型</param>
        /// <param name="dictionaryMapping">字段映射关系字典</param>
        /// <returns>转换后的ProtoBuf目标实体</returns>
        public static T ConvertProtoBuf<T>(Object source, Dictionary<string, string> dictionaryMapping = null)
        {
            Type _SrcT = source.GetType();
            Type _DestT = typeof(T);

            // 构造一个要转换对象实例
            T _Instance = Activator.CreateInstance<T>();

            // 这里搜索目标类型的所有字段
            PropertyInfo[] _DestPropertys = _DestT.GetProperties();

            // 对来源对象中到的所有字段去除_ 进行映射
            if (dictionaryMapping == null)
                dictionaryMapping = DirtionaryClear_Proper(_SrcT);

            // 给目标类型中的所有字段根据来源实体进行赋值
            foreach (PropertyInfo property in _DestPropertys)
            {
                if (dictionaryMapping.ContainsKey(property.Name))
                {
                    PropertyInfo _SrcProp = _SrcT.GetProperty(dictionaryMapping[property.Name]);
                    //如果来源对象中存在目标属性才给其赋值
                    if (_SrcProp != null)
                    {
                        if (_SrcProp.GetValue(source) != null && _SrcProp.GetValue(source).ToString() != "")
                            property.SetValue(_Instance, Convert.ChangeType(_SrcProp.GetValue(source), property.PropertyType));
                    }
                }
            }
            return _Instance;
        }

        /// <summary>
        /// 针对程序集合实体转换为ProtoBuf集合实体
        /// </summary>
        /// <typeparam name="T">ProtoBuf目标类型</typeparam>
        /// <typeparam name="V">程序来源实体类型</typeparam>
        /// <param name="sources">搭载程序来源实体集合</param>
        /// <returns>转换后的ProtoBuf目标类型集合</returns>
        public static List<T> ConvertProtoBufList<T, V>(List<V> sources)
        {
            List<T> list = new List<T>();
            Dictionary<string, string>  dictionaryMapping = DirtionaryClear_Proper(typeof(V));
            sources.ForEach(s =>
            {
                list.Add(ConvertProtoBuf<T>(s, dictionaryMapping));
            });

            return list;
        }




        /// <summary>
        /// 针对ProtoBuf实体转换为程序实体
        /// </summary>
        /// <typeparam name="T">程序目标类型</typeparam>
        /// <param name="source">ProtoBuf来源实体</param>
        /// <param name="dictionaryMapping">字段映射关系字典</param>
        /// <returns>转换后的程序目标实体</returns>
        public static T ConvertDbEntity<T>(Object source, Dictionary<string, string> dictionaryMapping = null)
        {
            Type _SrcT = source.GetType();
            Type _DestT = typeof(T);

            // 构造一个要转换对象实例
            T _Instance = Activator.CreateInstance<T>();

            // 这里搜索目标类型的所有字段
            PropertyInfo[] _SrctPropertys = _SrcT.GetProperties();

            // 对来源对象中到的所有字段去除_ 进行映射
            if (dictionaryMapping == null)
                dictionaryMapping = DirtionaryClear_Proper(_DestT);

            // 给目标类型中的所有字段根据来源实体进行赋值
            foreach (PropertyInfo property in _SrctPropertys)
            {
                if (dictionaryMapping.ContainsKey(property.Name))
                {
                    PropertyInfo _DestProp = _DestT.GetProperty(dictionaryMapping[property.Name]);
                    //如果来源对象中存在目标属性才给其赋值
                    //if (_SrcProp != null)
                    //{
                    if (property.GetValue(source) != null && property.GetValue(source).ToString() != "")
                        _DestProp.SetValue(_Instance, Convert.ChangeType(property.GetValue(source), _DestProp.PropertyType));
                    //}
                }
            }
            return _Instance;
        }

        /// <summary>
        /// 针对ProtoBuf集合实体转换为程序集合实体
        /// </summary>
        /// <typeparam name="T">程序来源实体类型</typeparam>
        /// <typeparam name="V">ProtoBuf目标类型</typeparam>
        /// <param name="sources">搭载ProtoBuf实体集合</param>
        /// <returns>转换后的程序集合实体</returns>
        public static List<T> ConvertDbEntityList<T, V>(List<V> sources)
        {
            List<T> list = new List<T>();
            Dictionary<string, string> dictionaryMapping = DirtionaryClear_Proper(typeof(V));
            sources.ForEach(s =>
            {
                list.Add(ConvertDbEntity<T>(s, dictionaryMapping));
            });
            return list;
        }



        /// <summary>
        /// 对来源对象中到的所有字段去除_ 进行字典映射
        /// </summary>
        /// <param name="_SrcT">映射类型</param>
        /// <returns>去除_下划线后的字段为key，原始字段为value</returns>
        public static Dictionary<string, string> DirtionaryClear_Proper(Type _SrcT)
        {
            Dictionary<string, string> dictionaryMapping = new Dictionary<string, string>();
            PropertyInfo[] _SrcPropertys = _SrcT.GetProperties();
            for (int i = 0; i < _SrcPropertys.Length; i++)
            {
                dictionaryMapping.Add(_SrcPropertys[i].Name.Replace("_", ""), _SrcPropertys[i].Name);
            }
            return dictionaryMapping;
        }



        public static IQueryable<T> QueryWhereJoinBuildProto<T, V>(IQueryable<T> queryable, V queryParams)
        {
            Type _MainType = typeof(T);
            Type _QueryType = queryParams.GetType();

            Dictionary<string, string> dictionaryMapping = DirtionaryClear_Proper(_MainType); 

            PropertyInfo[] _QueryPropertys = _QueryType.GetProperties();
            _QueryPropertys.Where(q => dictionaryMapping.ContainsKey(q.Name)).ToList().ForEach(p =>
              {
                  string queryVal = Convert.ToString(p.GetValue(queryParams));
                  if (!string.IsNullOrEmpty(queryVal))
                  {
                      if (_QueryType.GetProperty("ISLIKE" + p.Name) != null && Convert.ToBoolean(_QueryType.GetProperty("ISLIKE" + p.Name).GetValue(queryParams)))
                      {
                          queryable = queryable.Where(m => Convert.ToString(_MainType.GetProperty(dictionaryMapping[p.Name]).GetValue(m)).Contains(queryVal));
                      }
                      else
                          queryable = queryable.Where(m => Convert.ToString(_MainType.GetProperty(dictionaryMapping[p.Name]).GetValue(m)) == queryVal);
                  }

              });

            return queryable;
        }


        #endregion
        
    }
}
