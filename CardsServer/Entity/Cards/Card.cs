using System.Diagnostics;

namespace CardsServer.Entity.Cards;

[DebuggerDisplay("{Rank} of {Suite}")]
public class Card
{
    public enum Suites
    {
        Hearts = 0,
        Diamonds = 1,
        Clubs = 2,
        Spades = 3,
        None = 4
    }

    public enum Ranks
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Knight = 11,
        Queen = 12,
        King = 13,
        Joker = 14,
    }

    public Suites Suite { get; set; }
    public Ranks Rank { get; set; }

    public Card(Suites suite, Ranks rank)
    {
        Suite = suite;
        Rank = rank;
    }
}
