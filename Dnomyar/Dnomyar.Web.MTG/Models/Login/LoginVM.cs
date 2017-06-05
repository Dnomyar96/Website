using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dnomyar.Web.MTG.Models.Login
{
    public class LoginVM
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string Error { get; set; }
    }
}