using System;

namespace duracion
{

    class Duracion
    {
        //private double segundos;
        private double horas, minutos , segundos;

        public Duracion(double h, double m, double s)
        {
            horas = h;
            minutos = m;
            segundos = s;
        }

        public Duracion(double seg)
        {
            segundos = seg;
        }

        /*public Duracion(double hr, double mi, double sg)
        {  
            segundos = hr * 60 * 60;
            segundos += mi * 60;
            segundos += sg;
        }*/

        public double A_horas()
        {
            return Math.Floor(segundos/(60*60));
        }
        public double A_minutos()
        {
            return Math.Floor(segundos/60);
        }
        public double A_segundos()
        {
            return Math.Floor(segundos);
        }

        public static Duracion operator +(Duracion a, Duracion b)
        {
            double h=0,m=0,s = 0;

            h = ((a.horas + b.horas)*60)/60;
            m = (a.minutos + b.minutos);
            s = (a.segundos + b.segundos);

            return new Duracion(Math.Floor(h), Math.Floor(m), Math.Floor(s));
        }

        public void print()
        {
            Console.WriteLine($"{horas}:{minutos}:{segundos}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Duracion a = new Duracion(02, 15, 12);
            Duracion b = new Duracion(00, 02, 15);
            Duracion c = new Duracion(02, 00, 10);
            Duracion e;
            a.print();
            b.print();
            c.print();
            e = a+b;
            e.print();
        }
    }
}
