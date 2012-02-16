using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
namespace tmf
{
    public static class ApplicationHelper
    {
        public static bool HasMobileSpecificViews
        {

            get
            {
                bool configCheck;
                bool.TryParse(ConfigurationManager.AppSettings["HasMobileSpecificViews"], out configCheck);
                return configCheck;

            }
        }
        /// <summary>
        /// Used to enable debugging using alternative devices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsSupportedMobileDevice(this HttpRequestBase request)
        {

            bool isMobile = request.Browser.IsMobileDevice;
            string userAgent = request.UserAgent.ToLowerInvariant();

            isMobile = isMobile || (userAgent.Contains("iphone")
                || userAgent.Contains("blackberry")
                || userAgent.Contains("mobile")
                || userAgent.Contains("windows ce")
                || userAgent.Contains("opera mini")
                || userAgent.Contains("palm")
               || userAgent.Contains("fennec")
               || userAgent.Contains("adobeair")
               || userAgent.Contains("ripple")

                );
            return isMobile;

        }

        public static string MobileViewsDirectoryName
        {
            get
            {
                string directoryName = ConfigurationManager.AppSettings["MobileViewsDirectoryName"];
                return !string.IsNullOrEmpty(directoryName) ? String.Format("{0}/", directoryName) : string.Empty;
            }
        }

    }
}