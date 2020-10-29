using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_Repository
{

    public enum CardSuit { Hearts = 1, Diamonds, Spades, Clubs}
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public CardSuit Suit { get; set; }
        public string Display { get; set; }

        public Card(string name, int value, CardSuit suit, string display)
        {
            Name = name;
            Value = value;
            Suit = suit;
            Display = display;
        }

        //Without Display prop - not sure where Display will come from yet.
        public Card(string name, int value, CardSuit suit)
        {
            Name = name;
            Value = value;
            Suit = suit;
        }
    }
}
