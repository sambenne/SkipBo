using System;
using System.Collections.Generic;
using System.Threading;

namespace SkipBo.App.Core
{
    public class Deck
    {
        private static readonly Lazy<Deck> _instance = new Lazy<Deck>(() => new Deck());
        private Deck()
        {
            Cards = new List<Card>();
        }
        public static Deck Instance => _instance.Value;
        public List<Card> Cards { get; set; }

        public void Load()
        {
            for (var i = 1; i < 13; i++)
            {
                for (var l = 1; l < 13; l++)
                {
                    Cards.Add(Card.From(l.ToString()));
                }
            }

            for (var i = 0; i < 18; i++)
            {
                Cards.Add(Card.From("S"));
            }
        }

        public void Shuffle()
        {
            Cards.Shuffle();
        }

        public void AddSet(List<Card> cards)
        {
            Cards.AddRange(cards);
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random _local;

        public static Random ThisThreadsRandom => _local ?? (_local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
    }

    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
