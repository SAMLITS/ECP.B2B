using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECP.B2b.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.ComEntity.Page;
using static ECP.B2b.ComEntity.Filter.LookUpValues.PageQueryParams;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;

namespace ECP.B2b.Manager.Areas.System.Controllers
{
    public class LookUpValuesController: BaseController<ModelDto.System.LookUpValues.PageResultReplyDto, ComEntity.Filter.LookUpValues.PageQueryParams, B2B_LOOKUP_VALUES>
    { 
        public ILookUpValuesClient _lookUpValuesClient;
        public ILookUpValuesGroup _lookUpValuesGroup;
        public LookUpValuesController(ILookUpValuesClient lookUpValuesClient, ILookUpValuesGroup lookUpValuesGroup) :base(lookUpValuesClient)
        {
            _lookUpValuesClient = lookUpValuesClient;
            _lookUpValuesGroup = lookUpValuesGroup;
        }

        /// <summary>
        /// 重写Add页面
        /// </summary>
        /// <returns></returns>
        public override ActionResult Add()
        {
            ViewBag.LOOKUP_VALUES_ALL_ID = Request.Query["LOOKUP_VALUES_ALL_ID"];
            ViewBag.LOOKUP_TYPE = Request.Query["LOOKUP_TYPE"];
            return base.Add();
        }

        /// <summary>
        /// 重写列表数据加载，处理码值
        /// </summary>
        /// <param name="pageQueryParams"></param>
        /// <returns></returns>
        public override async Task<JsonResult> List(PageQueryParams pageQueryParams)
        {
            var resultReply = await _lookUpValuesGroup.ListByPageGroup(pageQueryParams);
            return Json(resultReply);
        }

        [HttpPost]
        public async Task<JsonResult> GetLookUpValuesByType(LookUpValuesByTypeParams lookUpValuesByTypeParams)
        {
            var resultReply = await _lookUpValuesClient.GetLookUpValuesByType(new List<LookUpValuesByTypeParams> { lookUpValuesByTypeParams });
            return Json(resultReply);
        }



    }
}
