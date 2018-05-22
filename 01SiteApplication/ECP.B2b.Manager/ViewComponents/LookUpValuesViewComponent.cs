using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.Main.GrpcClient.Interface.System;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.B2b.Manager.ViewComponents
{
    [ViewComponent]
    public class LookUpValuesViewComponent : ViewComponent
    {
        public ILookUpValuesClient lookUpValuesClient;
        public LookUpValuesViewComponent(ILookUpValuesClient _lookUpValuesClient) 
        {
            lookUpValuesClient = _lookUpValuesClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(LookUpValuesByTypeParams lookUpValuesByTypeParams)
        {
            var resultReply = await lookUpValuesClient.GetLookUpValuesByType(new List<LookUpValuesByTypeParams> { lookUpValuesByTypeParams });
            ViewBag.defaultValue = lookUpValuesByTypeParams.defaultValue;
            return View(resultReply); 
        }

    }
}
