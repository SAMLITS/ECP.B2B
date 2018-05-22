using ECP.B2b.ModelDto.System.LookUpValues;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ECP.Util.Common.Extensions
{
    public static class ListJoinExtensions
    {
        private static void JSetVal<T, J>(List<J> j1, Type tType, string[] gz, T l)
        {
            object resVal = tType.GetProperty(gz[2]).GetValue(l);
            if (j1 != null)
            {
                Type j1Type = typeof(J);
                var resList = j1.Where(j => Convert.ToString(resVal) == Convert.ToString(j1Type.GetProperty(gz[3]).GetValue(j))).ToList();
                if (resList.Count == 0)
                    resVal = resVal.ToDbJoinNull();
                else
                    resVal = j1Type.GetProperty(gz[4]).GetValue(resList[0]);
                tType.GetProperty(gz[1]).SetValue(l, resVal);
            }
            else
            {
                tType.GetProperty(gz[1]).SetValue(l, resVal.ToDbJoinNull());
            }
        }

        public static void Join<T>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, params string[] gzArray)
        {
            list.Join<T, object, object, object, object, object, object, object, object, object, object>(lookup, null, null, null, null, null, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, params string[] gzArray)
        {
            list.Join<T, J1, object, object, object, object, object, object, object, object, object>(lookup, j1,null, null, null, null, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, object, object, object, object, object, object, object, object>(lookup, j1, j2, null, null, null, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, object, object, object, object, object, object, object>(lookup, j1, j2, j3, null, null, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, object, object, object, object, object, object>(lookup, j1, j2, j3, j4, null, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4, J5>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, J5, object, object, object, object, object>(lookup, j1, j2, j3, j4, j5, null, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4, J5, J6>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, List<J6> j6 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, J5, J6, object, object, object, object>(lookup, j1, j2, j3, j4, j5, j6, null, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4, J5, J6, J7>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, List<J6> j6 = null, List<J7> j7 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, J5, J6, J7, object, object, object>(lookup, j1, j2, j3, j4, j5, j6, j7, null, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4, J5, J6, J7, J8>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, List<J6> j6 = null, List<J7> j7 = null, List<J8> j8 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, J5, J6, J7, J8, object, object>(lookup, j1, j2, j3, j4, j5, j6, j7, j8, null, null, gzArray);
        }
        public static void Join<T, J1, J2, J3, J4, J5, J6, J7, J8, J9>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, List<J6> j6 = null, List<J7> j7 = null, List<J8> j8 = null, List<J9> j9 = null, params string[] gzArray)
        {
            list.Join<T, J1, J2, J3, J4, J5, J6, J7, J8, J9, object>(lookup, j1, j2, j3, j4, j5, j6, j7, j8, j9,null , gzArray);
        } 


        /// gzArray   lookup->DtoFieldName->ITEM_UOM->ITEM_UOM
        ///            码表固定代号->Dto需要被赋值的字段名->码表名称->被关联Code字段
        ///            j1->DtoFieldName->MjFieldName->SjFieldName->SrFieldName
        ///            对应参数名->Dto需要被赋值的字段名->Dto主表关联字段->从表关联字段->返回字段
        public static void Join<T, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10>(this List<T> list, List<LookUpValuesByTypeDto> lookup = null, List<J1> j1 = null, List<J2> j2 = null, List<J3> j3 = null, List<J4> j4 = null, List<J5> j5 = null, List<J6> j6 = null, List<J7> j7 = null, List<J8> j8 = null, List<J9> j9 = null, List<J10> j10 = null, params string[] gzArray)
        {
            Type tType = typeof(T);

            //l.PARTY_NAME = partyIdqueryList.Result.Where(p => l.PARTY_ID == p.ID).First().NAME1;
            list.ForEach(l =>
            {
                for (int i = 0; i < gzArray.Length; i++)
                {
                    string[] gz = global::System.Text.RegularExpressions.Regex.Split(gzArray[i], "->");
                    if (gz[0] == "lookup")
                    {
                        if (lookup != null)
                        {
                            tType.GetProperty(gz[1]).SetValue(l, lookup.GetLookupText(gz[2], Convert.ToString(tType.GetProperty(gz[3]).GetValue(l))));
                        }
                    }
                    else if (gz[0] == "j1")
                        JSetVal(j1, tType, gz, l);
                    else if (gz[0] == "j2")
                        JSetVal(j2, tType, gz, l);
                    else if (gz[0] == "j3")
                        JSetVal(j3, tType, gz, l);
                    else if (gz[0] == "j4")
                        JSetVal(j4, tType, gz, l);
                    else if (gz[0] == "j5")
                        JSetVal(j5, tType, gz, l);
                    else if (gz[0] == "j6")
                        JSetVal(j6, tType, gz, l);
                    else if (gz[0] == "j7")
                        JSetVal(j7, tType, gz, l);
                    else if (gz[0] == "j8")
                        JSetVal(j8, tType, gz, l);
                    else if (gz[0] == "j9")
                        JSetVal(j9, tType, gz, l);
                    else if (gz[0] == "j10")
                        JSetVal(j10, tType, gz, l);
                }
            });
        }

    }
}
