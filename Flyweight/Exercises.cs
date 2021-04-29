using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight
{
    public class Sentence
    {
        private string[] words;
        private WordToken[] wordTokens;

        public Sentence(string plainText)
        {
            this.words = plainText.Split(' ');
            wordTokens = new WordToken[words.Length];
        }

        public WordToken this[int index]
        {
            get
            {
                if (wordTokens[index] == null) wordTokens[index] = new WordToken();
                return wordTokens[index];
            }
        }

        public override string ToString()
        {
            var result = new List<string>();
            int index = 0;
            foreach (var word in words)
            {
                if(wordTokens[index] == null)
                {
                    result.Add(words[index]);
                    index++;
                    continue;
                }
                var str = wordTokens[index].Capitalize ? words[index].ToUpper() : words[index];
                result.Add(str);
                index++;
            }
            return string.Join(" ", result);
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    public class Exercises
    {
        public static void MainFunc(string[] args)
        {
            var s = new Sentence("hello world!");
            s[1].Capitalize = true;
            Console.WriteLine(s);
        }
    }
}
