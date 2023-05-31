namespace CardsServer.Extensions;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list, uint amount = 1)
    {
        if (amount < 1) amount = 1;

        for (uint i = 0; i < amount; i++)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Shared.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
