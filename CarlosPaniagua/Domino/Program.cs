using System;

namespace Domino
{
    class domino
    {
        private int arriva;
        private int abajo;

        public domino(int up, int down)
        {
            arriva = up;
            abajo = down;
        }

        public static int operator +(domino a, domino b)
        { 
            return a.arriva+b.arriva+a.abajo+b.abajo;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            domino a = new domino(2,0);
            domino b = new domino(4,1);
            
            Console.WriteLine(a+b);
        }
    }
}
