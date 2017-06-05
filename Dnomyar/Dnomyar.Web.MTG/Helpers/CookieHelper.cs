using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnomyar.Web.MTG.Helpers
{
    public static class CookieHelper
    {
        public static void CreateUserCookie(int userId)
        {
            HttpContext.Current.Response.Cookies["userId"].Value = userId.ToString();
        }

        public static void DeleteUserCookie()
        {
            if (HttpContext.Current.Request.Cookies["userId"] != null)
                HttpContext.Current.Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
        }

        public static int GetCurrentUserId()
        {
            if (HttpContext.Current.Request.Cookies["userId"] != null)
            {
                var userId = HttpContext.Current.Request.Cookies["userId"].Value;
                return Convert.ToInt32(userId);
            }

            return -1;
        }
    }
}