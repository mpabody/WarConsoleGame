using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War_Repository;

namespace War_Console
{
    class GameUI
    {
        private WarRepo _repo = new WarRepo();
        private Queue<Card> _playerHand = new Queue<Card>();
        private Queue<Card> _dealerHand = new Queue<Card>();

        public void Run()
        {
            SeedCards();
            Deal();
            PrintHands();
            PlayGame();
            PrintHands();
            DeclareWinner();
        }
        private void PlayGame()
        {
            int rounds = 0;
            do
            {
                Console.WriteLine($"You have {_playerHand.Count()} cards remaining.\n" +
                    $"The dealer has {_dealerHand.Count()} cards remaining.\n\n\n\n");

                var playerNextCard = _playerHand.Peek();
                var dealerNextCard = _dealerHand.Peek();
                Console.WriteLine($"Your card is the {playerNextCard.Name}");
                Console.WriteLine($"The dealer's card is the {dealerNextCard.Name}");

                if (playerNextCard.Value > dealerNextCard.Value)
                {
                    Console.WriteLine("You win! The dealer's card is now yours!");
                    var myNewCard = _dealerHand.Dequeue();
                    var stillMyCard = _playerHand.Dequeue();
                    _playerHand.Enqueue(myNewCard);
                    _playerHand.Enqueue(stillMyCard);
                }
                else if (dealerNextCard.Value > playerNextCard.Value)
                {
                    Console.WriteLine("The dealer wins. Your card is now his.");
                    var stillDealerCard = _dealerHand.Dequeue();
                    var myOldCard = _playerHand.Dequeue();
                    _dealerHand.Enqueue(stillDealerCard);
                    _dealerHand.Enqueue(myOldCard);
                }
                else
                {
                    Console.WriteLine("It's a tie! This means War!");
                    War();
                }
                rounds++;
            }
            while (_playerHand.Count > 1 && _dealerHand.Count > 1 && rounds <= 100);
        }

        public List<Card> Shuffle()
        {
            var deck = _repo.GetEntireDeck();
            var shuffledDeck = new List<Card>();

            for (int i = deck.Count(); i > 0; i--)
            {
                Random rndm = new Random();
                int index = rndm.Next(deck.Count() - 1);
                shuffledDeck.Add(deck[index]);
                deck.Remove(deck[index]);
            }
            return shuffledDeck;
        }

        private void Deal()
        {
            List<Card> shuffledDeck = Shuffle();

            for (int i = 0; i <= shuffledDeck.Count - 1; i++)
            {
                var card = shuffledDeck[i];
                if (i % 2 == 0)
                {
                    _playerHand.Enqueue(card);
                }
                else
                {
                    _dealerHand.Enqueue(card);
                }
            }
        }

        private void War()
        {
            var holdCards = new List<Card>();
            var stillAtWar = true;
            while (stillAtWar)
            {
                var playerNextCard = _playerHand.Peek();
                var dealerNextCard = _dealerHand.Peek();
                holdCards.Add(_dealerHand.Dequeue());
                holdCards.Add(_playerHand.Dequeue());
                if (playerNextCard.Value > dealerNextCard.Value)
                {
                    holdCards.Add(_dealerHand.Dequeue());
                    holdCards.Add(_playerHand.Dequeue());
                    Console.WriteLine("You win! All the cards are yours!");
                    foreach (var card in holdCards)
                    {
                        _playerHand.Enqueue(card);
                    }
                    stillAtWar = false;
                }
                else if (dealerNextCard.Value > playerNextCard.Value)
                {
                    holdCards.Add(_dealerHand.Dequeue());
                    holdCards.Add(_playerHand.Dequeue());
                    Console.WriteLine("You lose. All the cards belong to the dealer now...");
                    foreach (var card in holdCards)
                    {
                        _dealerHand.Enqueue(card);
                    }
                    stillAtWar = false;
                }
                else if (dealerNextCard == null || playerNextCard == null)
                {
                    if (dealerNextCard == null)
                    {
                        foreach (var card in holdCards)
                        {
                            _playerHand.Enqueue(card);
                        }
                    }
                    else
                    {
                        foreach (var card in holdCards)
                        {
                            _dealerHand.Enqueue(card);
                        }
                    }
                    stillAtWar = false;
                }
                else
                {
                    Console.WriteLine("Another tie! Keep going!");
                }

            }
        }

        private void DeclareWinner()
        {
            if (_dealerHand.Count < _playerHand.Count)
            {
                Console.WriteLine("You win!!");
            }
            else if (_playerHand.Count < _dealerHand.Count)
            {
                Console.WriteLine("Dealer wins. Better luck next time.");
            }
            else
            {
                Console.WriteLine("No one wins this time! Both players still have the same number of cards.");
            }
            Console.WriteLine("Press any key to play again");
            Console.ReadKey();
        }

        private void PrintHands()
        {
            Console.WriteLine();
            Console.WriteLine("Player Cards\n");
            foreach (var card in _playerHand)
            {
                Console.WriteLine(card.Name);
            }
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Dealer Cards\n");
            foreach (var card in _dealerHand)
            {
                Console.WriteLine(card.Name);
            }
        }

        private void SeedCards()
        {
            var twoOfHearts = new Card("Two Of Hearts", 2, CardSuit.Hearts);
            var threeOfHearts = new Card("Three Of Hearts", 3, CardSuit.Hearts);
            var fourOfHearts = new Card("Four Of Hearts", 4, CardSuit.Hearts);
            var fiveOfHearts = new Card("Five Of Hearts", 5, CardSuit.Hearts);
            var sixOfHearts = new Card("Six Of Hearts", 6, CardSuit.Hearts);
            var sevenOfHearts = new Card("Seven Of Hearts", 7, CardSuit.Hearts);
            var eightOfHearts = new Card("Eight Of Hearts", 8, CardSuit.Hearts);
            var nineOfHearts = new Card("Nine Of Hearts", 9, CardSuit.Hearts);
            var tenOfHearts = new Card("Ten Of Hearts", 10, CardSuit.Hearts);
            var jackOfHearts = new Card("Jack Of Hearts", 11, CardSuit.Hearts);
            var queenOfHearts = new Card("Queen Of Hearts", 12, CardSuit.Hearts);
            var kingOfHearts = new Card("King Of Hearts", 13, CardSuit.Hearts);
            var aceOfHearts = new Card("Ace Of Hearts", 14, CardSuit.Hearts);

            _repo.AddCardToDeck(twoOfHearts);
            _repo.AddCardToDeck(threeOfHearts);
            _repo.AddCardToDeck(fourOfHearts);
            _repo.AddCardToDeck(fiveOfHearts);
            _repo.AddCardToDeck(sixOfHearts);
            _repo.AddCardToDeck(sevenOfHearts);
            _repo.AddCardToDeck(eightOfHearts);
            _repo.AddCardToDeck(nineOfHearts);
            _repo.AddCardToDeck(tenOfHearts);
            _repo.AddCardToDeck(jackOfHearts);
            _repo.AddCardToDeck(queenOfHearts);
            _repo.AddCardToDeck(kingOfHearts);
            _repo.AddCardToDeck(aceOfHearts);

            var twoOfDiamonds = new Card("Two Of Diamonds", 2, CardSuit.Diamonds);
            var threeOfDiamonds = new Card("Three Of Diamonds", 3, CardSuit.Diamonds);
            var fourOfDiamonds = new Card("Four Of Diamonds", 4, CardSuit.Diamonds);
            var fiveOfDiamonds = new Card("Five Of Diamonds", 5, CardSuit.Diamonds);
            var sixOfDiamonds = new Card("Six Of Diamonds", 6, CardSuit.Diamonds);
            var sevenOfDiamonds = new Card("Seven Of Diamonds", 7, CardSuit.Diamonds);
            var eightOfDiamonds = new Card("Eight Of Diamonds", 8, CardSuit.Diamonds);
            var nineOfDiamonds = new Card("Nine Of Diamonds", 9, CardSuit.Diamonds);
            var tenOfDiamonds = new Card("Ten Of Diamonds", 10, CardSuit.Diamonds);
            var jackOfDiamonds = new Card("Jack Of Diamonds", 11, CardSuit.Diamonds);
            var queenOfDiamonds = new Card("Queen Of Diamonds", 12, CardSuit.Diamonds);
            var kingOfDiamonds = new Card("King Of Diamonds", 13, CardSuit.Diamonds);
            var aceOfDiamonds = new Card("Ace Of Diamonds", 14, CardSuit.Diamonds);

            _repo.AddCardToDeck(twoOfDiamonds);
            _repo.AddCardToDeck(threeOfDiamonds);
            _repo.AddCardToDeck(fourOfDiamonds);
            _repo.AddCardToDeck(fiveOfDiamonds);
            _repo.AddCardToDeck(sixOfDiamonds);
            _repo.AddCardToDeck(sevenOfDiamonds);
            _repo.AddCardToDeck(eightOfDiamonds);
            _repo.AddCardToDeck(nineOfDiamonds);
            _repo.AddCardToDeck(tenOfDiamonds);
            _repo.AddCardToDeck(jackOfDiamonds);
            _repo.AddCardToDeck(queenOfDiamonds);
            _repo.AddCardToDeck(kingOfDiamonds);
            _repo.AddCardToDeck(aceOfDiamonds);

            var twoOfSpades = new Card("Two Of Spades", 2, CardSuit.Spades);
            var threeOfSpades = new Card("Three Of Spades", 3, CardSuit.Spades);
            var fourOfSpades = new Card("Four Of Spades", 4, CardSuit.Spades);
            var fiveOfSpades = new Card("Five Of Spades", 5, CardSuit.Spades);
            var sixOfSpades = new Card("Six Of Spades", 6, CardSuit.Spades);
            var sevenOfSpades = new Card("Seven Of Spades", 7, CardSuit.Spades);
            var eightOfSpades = new Card("Eight Of Spades", 8, CardSuit.Spades);
            var nineOfSpades = new Card("Nine Of Spades", 9, CardSuit.Spades);
            var tenOfSpades = new Card("Ten Of Spades", 10, CardSuit.Spades);
            var jackOfSpades = new Card("Jack Of Spades", 11, CardSuit.Spades);
            var queenOfSpades = new Card("Queen Of Spades", 12, CardSuit.Spades);
            var kingOfSpades = new Card("King Of Spades", 13, CardSuit.Spades);
            var aceOfSpades = new Card("Ace Of Spades", 14, CardSuit.Spades);

            _repo.AddCardToDeck(twoOfSpades);
            _repo.AddCardToDeck(threeOfSpades);
            _repo.AddCardToDeck(fourOfSpades);
            _repo.AddCardToDeck(fiveOfSpades);
            _repo.AddCardToDeck(sixOfSpades);
            _repo.AddCardToDeck(sevenOfSpades);
            _repo.AddCardToDeck(eightOfSpades);
            _repo.AddCardToDeck(nineOfSpades);
            _repo.AddCardToDeck(tenOfSpades);
            _repo.AddCardToDeck(jackOfSpades);
            _repo.AddCardToDeck(queenOfSpades);
            _repo.AddCardToDeck(kingOfSpades);
            _repo.AddCardToDeck(aceOfSpades);

            var twoOfClubs = new Card("Two Of Clubs", 2, CardSuit.Clubs);
            var threeOfClubs = new Card("Three Of Clubs", 3, CardSuit.Clubs);
            var fourOfClubs = new Card("Four Of Clubs", 4, CardSuit.Clubs);
            var fiveOfClubs = new Card("Five Of Clubs", 5, CardSuit.Clubs);
            var sixOfClubs = new Card("Six Of Clubs", 6, CardSuit.Clubs);
            var sevenOfClubs = new Card("Seven Of Clubs", 7, CardSuit.Clubs);
            var eightOfClubs = new Card("Eight Of Clubs", 8, CardSuit.Clubs);
            var nineOfClubs = new Card("Nine Of Clubs", 9, CardSuit.Clubs);
            var tenOfClubs = new Card("Ten Of Clubs", 10, CardSuit.Clubs);
            var jackOfClubs = new Card("Jack Of Clubs", 11, CardSuit.Clubs);
            var queenOfClubs = new Card("Queen Of Clubs", 12, CardSuit.Clubs);
            var kingOfClubs = new Card("King Of Clubs", 13, CardSuit.Clubs);
            var aceOfClubs = new Card("Ace Of Clubs", 14, CardSuit.Clubs);

            _repo.AddCardToDeck(twoOfClubs);
            _repo.AddCardToDeck(threeOfClubs);
            _repo.AddCardToDeck(fourOfClubs);
            _repo.AddCardToDeck(fiveOfClubs);
            _repo.AddCardToDeck(sixOfClubs);
            _repo.AddCardToDeck(sevenOfClubs);
            _repo.AddCardToDeck(eightOfClubs);
            _repo.AddCardToDeck(nineOfClubs);
            _repo.AddCardToDeck(tenOfClubs);
            _repo.AddCardToDeck(jackOfClubs);
            _repo.AddCardToDeck(queenOfClubs);
            _repo.AddCardToDeck(kingOfClubs);
            _repo.AddCardToDeck(aceOfClubs);
        }
    }
}
