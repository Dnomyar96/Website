using Dnomyar.Data.MTG.Enums;
using Dnomyar.Data.MTG.Models;
using Dnomyar.Web.MTG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnomyar.Web.MTG
{
    public static class CurrentUser
    {
        public static User GetCurrentUser()
        {
            var userId = CookieHelper.GetCurrentUserId();

            var user = new User()
            {
                Id = 0,
                UserName = "Test",
                IsLoggedIn = false,
                Role = Roles.admin
            };

            if (userId >= 0)
                user.IsLoggedIn = true;

            return user;
        }
    }
}