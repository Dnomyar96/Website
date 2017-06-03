using Dnomyar.Data.MTG;
using Dnomyar.Data.MTG.Models;
using Dnomyar.Web.MTG.Models.Home;
using MtgApiManager.Lib.Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dnomyar.Web.MTG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomeVM();
            var dbHandler = new DbHandler();
            vm.Cards = dbHandler.GetAllCards();

            return View(vm);
        }
        public ActionResult Cards()
        {
            var vm = new HomeVM();
            var dbHandler = new DbHandler();
            vm.Cards = dbHandler.GetAllCards();

            return View(vm);
        }

        public async Task<ActionResult> Insert()
        {
            var dbHandler = new DbHandler();
            var service = new CardService();
            var cards = new List<MtgApiManager.Lib.Model.Card>();
            var done = false;
            var count = 1;
            while (!done)
            {
                var result = await service.Where(x => x.SetName, "Amonkhet").Where(x => x.Page, count).Where(x => x.PageSize, 1000).AllAsync();
                if (result.IsSuccess)
                {
                    if (result.Value.Count > 0)
                    {
                        foreach (var card in result.Value)
                        {
                            cards.Add(card);
                        }
                    }
                    else
                    {
                        done = true;
                    }
                }
                count++;
            }

            var results = new List<string>();
            foreach (var card in cards)
            {
                var name = "";
                var flavor = "";
                var set = "";
                var setName = "";
                var text = "";

                if (card.Name != null)
                    name = card.Name.Replace("'", "**");

                if (card.Flavor != null)
                    flavor = card.Flavor.Replace("'", "**");

                if (card.Set != null)
                    set = card.Set.Replace("'", "**");

                if (card.SetName != null)
                    setName = card.SetName.Replace("'", "**");

                if (card.Text != null)
                    text = card.Text.Replace("'", "**");

                var cardToAdd = new Card()
                {
                    Name = name,
                    Colors = card.Colors,
                    Flavor = flavor,
                    ImageUrl = card.ImageUrl.AbsoluteUri,
                    Loyalty = card.Loyalty,
                    ManaCost = card.ManaCost,
                    Power = card.Power,
                    Rarity = card.Rarity,
                    Set = set,
                    SetName = setName,
                    Text = text,
                    Toughness = card.Toughness,
                    Type = card.Type
                };

                results.Add(dbHandler.InsertCard(cardToAdd));
            }

            return RedirectToAction("Cards");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}