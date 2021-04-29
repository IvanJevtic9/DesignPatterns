using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flyweight
{
    public class FormattingText // Besmisleno je da cuvamo listu za svako slovo. Mozemo da cuvamo info o range-u i da na taj nacin modifikujemo text
    {
        private readonly string plainText;
        private bool[] capatalize;
        public FormattingText(string plainText)
        {
            this.plainText = plainText;
            capatalize = new bool[plainText.Length];
        }

        public void Capatilize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                capatalize[i] = true;
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                var c = capatalize[i] ? char.ToUpper(plainText[i]) : plainText[i];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }

    public class FormattingTextImproved
    {
        private readonly string plainText;
        public List<Tuple<int, int>> capatalizeRange = new List<Tuple<int, int>>();

        public FormattingTextImproved(string plainText)
        {
            this.plainText = plainText;
        }

        // start je manje od end (treba napraviti start , count) Da ne bi mroal ida imamo proveru.
        public void Capatalize(int start, int end)
        {
            MergeRanges(start, end);
        }

        private void MergeRanges(int start, int end)
        {
            // Cuvamo tuple (jer zelimo samo capitals(start i end imamo samo), da zelimo tipa Bold, italic  => pravili bismo klasu
            var range = new Tuple<int, int>(start, end); 
            var addFlag = true;
            if (capatalizeRange.Count == 0)
            {
                capatalizeRange.Add(range);
            }

            for (var i = 0; i < capatalizeRange.Count; i++)
            {
                // Range je unutar naseg range-a , brisemo ga, idemo dalje
                if ((capatalizeRange[i].Item1 >= range.Item1 && capatalizeRange[i].Item2 < range.Item2) || (capatalizeRange[i].Item1 > range.Item1 && capatalizeRange[i].Item2 <= range.Item2))
                {
                    capatalizeRange.Remove(capatalizeRange[i]);
                    i--;
                }
                else if (capatalizeRange[i].Item1 <= range.Item1 && capatalizeRange[i].Item2 >= range.Item2)
                {
                    addFlag = false;
                    break;
                }
                else if ((range.Item1 >= capatalizeRange[i].Item1 && range.Item1 <= capatalizeRange[i].Item2 + 1) || (range.Item2 >= capatalizeRange[i].Item1 - 1 && range.Item2 <= capatalizeRange[i].Item2))
                {
                    range = new Tuple<int, int>(Math.Min(capatalizeRange[i].Item1, range.Item1), Math.Max(capatalizeRange[i].Item2, range.Item2));
                    capatalizeRange.Remove(capatalizeRange[i]);
                }
            }

            if (addFlag) capatalizeRange.Add(range);
        }
        public override string ToString()
        {
            var sb = new StringBuilder(plainText);
            foreach (var range in capatalizeRange)
            {
                for (int i = range.Item1; i <= range.Item2; i++)
                {
                    sb[i] = char.ToUpper(sb[i]);
                }
            }
            return sb.ToString();
        }
    }

    public class TextFormatting
    {
        public static void MainFunc(string[] args)
        {
            var p = new FormattingText("Hello world!");
            p.Capatilize(7, 10);
            p.Capatilize(6, 10);

            Console.WriteLine(p.ToString());
            var bp = new FormattingTextImproved("Hi my name is ivan jevtic");
            bp.Capatalize(0,1);
            bp.Capatalize(15, 16);
            bp.Capatalize(14, 17);
            bp.Capatalize(18, 24);

            Console.WriteLine(bp.ToString());

            Console.WriteLine();
        }
    }
}
