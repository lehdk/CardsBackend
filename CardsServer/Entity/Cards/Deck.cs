using CardsServer.Extensions;
using static CardsServer.Entity.Cards.Card;

namespace CardsServer.Entity.Cards;

public class Deck
{
    public List<Card> Cards { get; }

    public Deck()
    {
        Cards = new List<Card>();
    }

    public Deck(int amountOfDecks, uint amountOfJokers = 0) : this()
    {
        if (amountOfDecks < 0) return;

        for (int i = 0; i < amountOfDecks; i++)
        {
            for (int suite = 0; suite < Enum.GetNames(typeof(Suites)).Length - 1; suite++) // Minus one to exclude None
            {
                for (int rank = 1; rank < Enum.GetNames(typeof(Ranks)).Length; rank++) // Minus one to exclude jokers
                {
                    Cards.Add(new Card((Suites)suite, (Ranks)rank));
                }
            }
        }

        if (amountOfJokers > 0)
        {
            for (uint jokers = 0; jokers < amountOfJokers; jokers++)
            {
                Cards.Add(new Card(Suites.None, Ranks.Joker));
            }
        }
    }

    public void AddCard(Card card)
    {
       Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }

    public void Shuffle(uint amount = 1)
    {
        Cards.Shuffle(amount);
    }
}
