using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_Repository
{
    public class WarRepo
    {
        private readonly List<Card> _deck = new List<Card>();

        public List<Card> GetEntireDeck()
        {
            return _deck;
        }

        public void AddCardToDeck(Card card)
        {
            _deck.Add(card);
        }
    }
}
