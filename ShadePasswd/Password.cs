using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace ShadePasswd
{

    class Password
    {
        // Parts of password
        public enum CombinationPart
        {
            /// <summary>
            /// Numbers
            /// </summary>
            Numbers,

            /// <summary>
            /// Letters
            /// </summary>
            Letters,

            /// <summary>
            /// Special Chars
            /// </summary>
            SpecialChars,

            /// <summary>
            /// RandomWords
            /// </summary>
            Words,
        }

        // Letters to use in password
        private static readonly char[] LETTERS = new char[] 
        { 
            'q','w','e','r','t','y',
            'u','i','o','p','a','s',
            'd','f','g','h','j','k',
            'l','z','x','c','v','b',
             'n','m' 
        };

        // Special chars to use in password
        private static readonly char[] SPECIAL = new char[]
        {
            '!','@','#','$','%','^',
            '&','*','(',')','_','+',
            '=','[',']','{','}',':',
            ';','"','\'','|','\\',
            ',','<','>','.','?','/'
        };

        private static readonly char[] NUMBERS = new char[]
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        private static string GetRandomWord()
        {
            string[] allWords = File.ReadAllLines("word_list.txt");
            return allWords[new Random().Next(allWords.Length)];
        }

        private static char[] GetCharArray(CombinationPart part)
        {
            switch(part)
            {
                case CombinationPart.Numbers: return NUMBERS;
                case CombinationPart.Letters: return LETTERS;
                case CombinationPart.SpecialChars: return SPECIAL;
                default: return new char[] { };
            }
        }

        // Generate Random Password
        public static string Generate(CombinationPart[] passwordParts,int length,bool randomSize)
        {
            string passwd = string.Empty;
            Random rand = new Random();

            while(passwd.Length < length)
            {
                CombinationPart randomPart = passwordParts[rand.Next(passwordParts.Length)];

                if (randomPart == CombinationPart.Letters || randomPart == CombinationPart.Numbers || randomPart == CombinationPart.SpecialChars)
                {
                    char[] charsArray = GetCharArray(randomPart);
                    passwd += charsArray[rand.Next(charsArray.Length)];
                }
            }

            // Adding random word to password
            if(passwordParts.Contains(CombinationPart.Words))
            {
                string word = GetRandomWord();
                string t_passwd = string.Empty;

                // cut word
                if(word.Length > passwd.Length/3)
                {
                    word = word.Substring(0, passwd.Length / 3);
                }
                int offset = rand.Next(passwd.Length - word.Length);
                
                //temp word
                string temp_word = string.Empty;
                for(int i =0;i<offset;i++) { temp_word += " "; }
                temp_word += word;
                int extreaoffset = passwd.Length - (word.Length + offset);
                for (int i =0;i < extreaoffset; i++) { temp_word += " "; }
                word = temp_word;

                for(int i =0;i< passwd.Length;i++)
                {
                    if(word[i] != ' ')
                    {
                        t_passwd += word[i];
                    }
                    else
                    {
                        t_passwd += passwd[i];
                    }
                }

                passwd = t_passwd;
            }

            if(randomSize)
            {
                string t_passwd = string.Empty;
                for(int i=0;i< passwd.Length;i++)
                {
                    if(rand.Next(10) > 5)
                    {
                        t_passwd += passwd[i].ToString().ToUpper();
                    }
                    else
                    {
                        t_passwd += passwd[i].ToString().ToLower();
                    }
                }
                passwd = t_passwd;
            }
            return passwd;
        }
    }
}
