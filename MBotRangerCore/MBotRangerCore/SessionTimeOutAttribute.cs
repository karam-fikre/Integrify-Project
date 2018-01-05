using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MBotRangerCore
{
    public class SessionTimeOutAttribute : ActionFilterAttribute, IActionFilter
    {

        public int actionType;
        public MbotAppData mAppData = new MbotAppData();
        public SessionTimeOutAttribute(int actionType)
        {
            this.actionType = actionType;
        }

        public SessionTimeOutAttribute(MbotAppData mAppData, bool ff)
        {

            this.mAppData = mAppData;
            //this.mAppData.IsItInUse = false;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            
            if (mAppData.IsItInUse)
            {
                if (actionType == 1)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Start", controller = "Home" }));

                }
            }
            base.OnActionExecuting(context);
        }
    }
}
