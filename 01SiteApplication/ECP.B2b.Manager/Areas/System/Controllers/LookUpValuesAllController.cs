using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECP.B2b.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.ComEntity.Page;
using static ECP.B2b.ComEntity.Filter.LookUpValuesAll.PageQueryParams;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Manager.Areas.System.Controllers
{
    public class LookUpValuesAllController: BaseController<ModelDto.System.LookUpValuesAll.PageResultReplyDto, ComEntity.Filter.LookUpValuesAll.PageQueryParams, B2B_LOOKUP_VALUES_ALL>
    { 
        public ILookUpValuesAllClient lookUpValuesAllClient;
        public LookUpValuesAllController(ILookUpValuesAllClient _lookUpValuesAllClient):base(_lookUpValuesAllClient)
        {
            lookUpValuesAllClient = _lookUpValuesAllClient;
        }

        /// <summary>
        /// 重写编辑页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Modify()
        {
            ViewBag.ID = Request.Query["ID"];
            return base.Modify();
        }
    }
}
