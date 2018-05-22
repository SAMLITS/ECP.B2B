using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.Main.GrpcClient.Interface.System;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECP.Util.Common.Extensions;
using ECP.B2b.ModelDto.System.LookUpValues;
using ECP.B2b.ModelDto; 
using ECP.B2b.Main.GrpcClient.Interface;  
using ECP_B2B_API_SDK.Helper; 

namespace ECP.B2b.Main.GrpcClientGroup
{
    public class ExtendSearchGroup
    {
        private ILookUpValuesClient _lookUpValuesClient; 
        public IMenuClient _menuClient; 

        public ExtendSearchGroup( ILookUpValuesClient lookUpValuesClient,  IMenuClient menuClient)
        { 
            _lookUpValuesClient = lookUpValuesClient; 
            _menuClient = menuClient; 
        }

        #region  FindIdListByNameContains ���u���d�{�X?�����?�u ��^ �]�tID�O�䥦���w�C
        private void AutoFindIdListByNameContains<QP>(out Task<List<NameByIdDto>> idQueryList, QP qParams, string wFields, string otherFields, string resSetField, Func<IdByNameContainsParams, Task<List<NameByIdDto>>> funcClient)
        {
            Task<List<NameByIdDto>> partyIdqueryList = null;

            Type qpType = qParams.GetType();
            string[] fieldArray = wFields.Split(",");
            bool isWhere = false;
            for (int i = 0; i < fieldArray.Length; i++)
            {
                var fieldVal = qpType.GetProperty(fieldArray[i]).GetValue(qParams);
                if (fieldVal != null && fieldVal.ToString().Trim() != "")
                {
                    isWhere = true;
                    break;
                }
            }
            if (isWhere)
            {
                IdByNameContainsParams idByNameContainsParams = new IdByNameContainsParams();
                Type inType = typeof(IdByNameContainsParams);
                Type qwType = typeof(IdByNameContainsParams.QueryWhereName);
                List<IdByNameContainsParams.QueryWhereName> queryList = new List<IdByNameContainsParams.QueryWhereName>();

                for (int i = 0; i < fieldArray.Length; i++)
                {
                    IdByNameContainsParams.QueryWhereName queryWhereName = new IdByNameContainsParams.QueryWhereName();
                    PropertyInfo likeProper = qpType.GetProperty("IS_LIKE_" + fieldArray[i]);
                    if (likeProper == null)
                        queryWhereName.IsLike = false;
                    else
                        queryWhereName.IsLike = (bool)likeProper.GetValue(qParams);

                    queryWhereName.NameVal = Convert.ToString(qpType.GetProperty(fieldArray[i]).GetValue(qParams));

                    queryList.Add(queryWhereName);
                }


                idByNameContainsParams.queryWheres = queryList;
                
                if (!string.IsNullOrEmpty(otherFields))
                {
                    string[] oFieldArray = otherFields.Split(",");
                    for (int i = 0; i < oFieldArray.Length; i++)
                    {
                        inType.GetProperty($"MappingDbField{ i + 1}").SetValue(idByNameContainsParams, oFieldArray[i]);
                    }
                }

                partyIdqueryList = funcClient(idByNameContainsParams);

                if (!string.IsNullOrEmpty(resSetField))
                {
                    qpType.GetProperty(resSetField).SetValue(qParams, partyIdqueryList.Result.GetIDS());
                }
            }
            idQueryList = partyIdqueryList;
        }

        /// <summary>
        /// ģ����ѯ���׷� ����ָ�����ֶ������õ�ָ���ֶ� ����
        /// </summary>
       /// <param name="idQueryList">��ѯ���</param>
        /// <typeparam name="QP">��ѯ��������</typeparam>
        /// <param name="qParams">��ѯ��������ʵ��</param>
        /// <param name="wFields">��ѯ�ֶ� ���ʹ��,����</param>
        /// <param name="otherFields">��Ҫ��ѯ�������������ֶ�</param>
        /// <param name="resSetField">�����ֶ��Զ���ֵ</param>
        /// <returns></returns>
        public void PartyIdByNameFind<QP>(out Task<List<NameByIdDto>> idQueryList, QP qParams, string wFields, string otherFields, string resSetField)
        {
            AutoFindIdListByNameContains(
                out idQueryList,
                qParams,
                wFields,
                otherFields,
                resSetField,
                s => _menuClient.FindIdListByNameContains(s)
                );
        } 
        #endregion

        #region FindNameListByIdList ���uid�d�{�X���w���r�q 
        private Task<List<NameByIdDto>> AutoFindNameListByIdList(Task<List<NameByIdDto>> idQueryList, Func<List<int>> func, string qFields, Func<NameByIdParams, Task<List<NameByIdDto>>> funcClient)
        {
            if (idQueryList == null)
            {
                Type inType = typeof(NameByIdParams);
                NameByIdParams nameByIdParams = new NameByIdParams();
                nameByIdParams.IdList = func();
                string[] fieldArray = qFields.Split(",");
                for (int i = 0; i < fieldArray.Length; i++)
                {
                    inType.GetProperty($"MappingDbField{i + 1}").SetValue(nameByIdParams, fieldArray[i]);
                }
                idQueryList = funcClient(nameByIdParams);
            }

            return idQueryList;
        }  

        /// <summary>
        /// ���u�����ID �d�{ �������w�C
        /// </summary>
        /// <param name="idQueryList"></param>
        public Task<List<NameByIdDto>> MenuNameByIdFind( Task<List<NameByIdDto>> idQueryList, Func<List<int>> func, string qFields)
        {
            return this.AutoFindNameListByIdList(idQueryList, func, qFields, s => _menuClient.FindNameListByIdList(s)); 
        } 
        #endregion



        /// <summary>
        /// ?�m�d�{?��d�{
        /// </summary>
        /// <param name="lookupList"></param>
        /// <param name="lookupNameArray"></param>
        public void JoinSearchLookup(out Task<List<LookUpValuesByTypeDto>> lookupList, params string[] lookupNameArray)
        {
            lookupList = null;
            if (lookupNameArray.Length > 0)
            {

                List<ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams> qParams = new List<ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams>();

                for (int i = 0; i < lookupNameArray.Length; i++)
                {
                    qParams.Add(new ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams { IsBetweenOt = true, LookUpName = lookupNameArray[i] });
                }
                lookupList = _lookUpValuesClient.GetLookUpValuesByType(qParams);
            }
        }
         
    }
}
