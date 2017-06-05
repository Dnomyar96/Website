using Dnomyar.Data.MTG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnomyar.Data.MTG.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public Roles Role { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
