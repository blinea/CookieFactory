using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieFactory.Models
{
    public static class ControlPanelStatic
    {
        public static string Cookies()
        {
            string name = "Cookies";
            return name;
        }

        public static string Categories()
        {
            string name = "Categories";
            return name;
        }

        public static string Products()
        {
            string name = "Products";
            return name;
        }

        public static DateTime Now()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime;
        }
    }
}
