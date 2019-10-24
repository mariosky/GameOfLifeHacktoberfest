using System;
using System.Collections.Generic;

namespace figura
{
    abstract class Figura
    {
        protected int px, py;
        protected string color;

        public Figura(int px, int py, string color)
        {
            this.px = px;
            this.py = py;
            this.color = color;

        }

        public abstract void dibuja();
    }


    class Rectangulo: Figura
    {
        public Rectangulo(int px, int py, string color):base(px, py, color)
        {
        }
        public override void dibuja()
        {
            
            Console.WriteLine($"se dibuja un rectangulo color {color} en x = {px} y y = {py}");
        }
    }

     class Circulo : Figura {
        public Circulo(int px, int py, string color):base(px , py, color)
        {
        }

        public override void dibuja()
        {
            Console.WriteLine($"se dibuja un circulo color {color} en x = {px} y y = {py}");
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            
            //Figura = figura = new Figura(10,10);
            List<Figura> figuras = new List<Figura>();
            figuras.Add(new Rectangulo(10, 10,"verde"));
            figuras.Add(new Rectangulo(12,13,"rojo"));
            figuras.Add(new Rectangulo(12,25,"azul"));
            figuras.Add(new Circulo(13,13,"verde"));
            figuras.Add(new Circulo(12,13,"rojo"));
            figuras.Add(new Circulo(11,14,"azul"));

            foreach (var item in figuras)
            {
                item.dibuja();
            }
            Circulo r = new Circulo(10,10,"rojo");   
            r.dibuja();
        }
    }
}

