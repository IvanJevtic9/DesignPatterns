using Facade.FacadeExample;
using System.Collections.Generic;
using System;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            var magicSquare = new MagicSquareGenerator();

            var square = magicSquare.Generate(5);
        }
    }
}
