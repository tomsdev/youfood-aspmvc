using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace tmf
{
    class MobileViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            ViewEngineResult result = null;
            var request = controllerContext.HttpContext.Request;

            if (request.IsSupportedMobileDevice() && ApplicationHelper.HasMobileSpecificViews)
            {
                string viewPathAndName = ApplicationHelper.MobileViewsDirectoryName + viewName;
                result = base.FindView(controllerContext, viewPathAndName, masterName, true);


                if (result == null || result.View == null)
                {
                    result = base.FindView(controllerContext, viewPathAndName, masterName, false);
                }


            }
            else
            {
                result = base.FindView(controllerContext, viewName, masterName, useCache);
            }
            return result;
        }
    }
}
