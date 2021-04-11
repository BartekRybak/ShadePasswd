using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadePasswd
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                string passwd = Password.Generate(new Password.CombinationPart[] {
                Password.CombinationPart.Letters,
                Password.CombinationPart.Numbers,
                Password.CombinationPart.Words,
                Password.CombinationPart.SpecialChars
            }, 16,true);

                Console.WriteLine(passwd);
                Console.ReadLine();
            }

            
        }
    }
}
