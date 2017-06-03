using Dnomyar.Data.MTG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnomyar.Data.MTG
{
    public interface IDbHandler
    {
        string InsertCard(Card card);

        IEnumerable<Card> GetCardsBySet(string set);

        IEnumerable<Card> GetAllCards();

        Card GetCardById(int id);

        Card GetCardByName(int id);
    }
}
