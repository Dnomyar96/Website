using Dnomyar.Data.MTG.Models;
using System.Collections.Generic;

namespace Dnomyar.Web.MTG.Models.Home
{
    public class HomeVM
    {
        public IEnumerable<Card> Cards { get; set; }
    }
}