using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormattedTextFlyweight
{
    public class FormattedText
    {
        private readonly string _plainText;
        private readonly bool[] _capitalize;

        public FormattedText(string plainText)
        {
            _plainText = plainText;
            _capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                _capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                sb.Append(_capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private readonly string _plainText;
        private readonly List<TextRange> _formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            _plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange {Start = start, End = end};
            _formatting.Add(range);
            return range;
        }

        public void CapitalizeRange(int start, int end)
        {
            var range = new TextRange
            {
                Start = start,
                End = end,
                Capitalize = true
            };
            _formatting.Add(range);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var j = 0; j < _plainText.Length; j++)
            {
                var c = _plainText[j];
                c = _formatting
                    .Where(range => range.Covers(j) && range.Capitalize)
                    .Aggregate(c, (current, range) => char.ToUpper(current));
                sb.Append(c);
            }
            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }


    class Program
    {
        static void Main()
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10,15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.CapitalizeRange(10, 15);
            bft.CapitalizeRange(20, 25);
            Console.WriteLine(bft);
        }
    }
}
