using System.Collections.Generic;

namespace SkipBo.App
{
    public class Discard
    {
        public List<Card> Cards { get; set; }

        public Discard()
        {
            Cards = new List<Card>();
        }
    }
}