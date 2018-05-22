using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.B2b.DbModel;
using ECP.B2b.Main.GrpcClient.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECP.Util.HtmlHelper;
using ECP.Util.HtmlHelper.Filter;

namespace ECP.B2b.Manager.Models
{
    [LoginManagerAuthorityFilter]
    public abstract class BaseController<PD, PP, M> : Controller where PD : class where PP : class where M : BaseModel
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public CurrentUserEntity CurrentUser => HttpContext.GetCurrentUser();


        private IBaseGrpcClient<PD, PP, M> _baseClient;
        public BaseController(IBaseGrpcClient<PD, PP, M> baseClient)
        {
            this._baseClient = baseClient;
        }


        [HttpGet()]
        public virtual ViewResult List()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<JsonResult> List(PP pageQueryParams)
        {
            var resultReply = await _baseClient.GetListByPage(pageQueryParams);
            return Json(resultReply);
        }

        [HttpGet]
        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<JsonResult> Add(M request)
        {
            request.CREATION_DATE = DateTime.Now;
            request.LAST_UPDATE_DATE = DateTime.Now;
            request.CREATOR = CurrentUser.USER;
            request.EDITOR = CurrentUser.USER;

            var result = await _baseClient.Add(request);
            return Json(result);
        }

        [HttpPost]
        public virtual async Task<JsonResult> AddRange(List<M> request)
        {
            request.ForEach(e => 
            {
                e.CREATION_DATE = DateTime.Now;
                e.LAST_UPDATE_DATE = DateTime.Now;
                e.CREATOR = CurrentUser.USER;
                e.EDITOR = CurrentUser.USER;
            }); 

            var result = await _baseClient.AddRange(request);
            return Json(result);
        }


        [HttpPost]
        public virtual JsonResult FindById(int ID)
        {
            M dbModel = _baseClient.FindById(new IdModel { ID = ID }).Result;
            return Json(dbModel);
        }

        [HttpGet]
        public virtual ActionResult Show()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Modify()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<JsonResult> Modify(M request)
        {
            request.LAST_UPDATE_DATE = DateTime.Now;
            request.EDITOR = CurrentUser.USER;

            var result = await _baseClient.Modify(request);
            return Json(result);
        }

        [HttpPost]
        public virtual async Task<JsonResult> ModifyDic(Dictionary<string, string> dictionary)
        {
            dictionary.Add("LAST_UPDATE_DATE", DateTime.Now.ToString());
            dictionary.Add("EDITOR", CurrentUser.USER);
            var result = await _baseClient.ModifyDic(dictionary);
            return Json(result);
        }


        [HttpPost]
        public virtual async Task<JsonResult> Remove(RemoveModel request)
        {
            request.LAST_UPDATE_DATE = DateTime.Now;
            request.EDITOR = CurrentUser.USER;

            var result = await _baseClient.Remove(request);
            return Json(result);
        }
    }
}
