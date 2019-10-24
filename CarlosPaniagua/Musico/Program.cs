using System;
using System.Collections.Generic;

namespace Musico
{
    class Musico 
    {

        protected string nombre;
        
        public Musico(string n)
        {
            nombre = n;
        }

        public virtual void Saluda()
        {
            Console.WriteLine($"hola soy {nombre}");
        }

        public virtual void Afina()
        {
            Console.WriteLine($"{nombre}, Afina su instrumento ");
        }

        public virtual void toca()
        {
            Console.WriteLine($"toca {nombre}\n");
        }

    }

    class Bajista:Musico
    {
        private string bajo;
        
        public Bajista(string no,string bajo):base(no)
        {
            this.bajo = bajo;
        } 

       public override void Afina()
        {
           Console.WriteLine($"{nombre} esta afinando su bajo {bajo}");
        }

       public override void Saluda()
        {
           Console.WriteLine($"hola soy {nombre} y soy bajista");
        }

       public override void toca()
        {
           Console.WriteLine($"toca {nombre} su bajo {bajo}\n");
        }
    }

    class Guitarrista:Musico
    {
       private string Guitarra;

        public Guitarrista(string no,string Guitarra):base(no)
        {
            this.Guitarra = Guitarra;
        } 

        public override void Afina()
        {
            Console.WriteLine($"{nombre} esta afinando su guitarra {Guitarra}");
        }

        public override void Saluda()
        {
            Console.WriteLine($"hola soy {nombre} y soy guitarrista");
        }

        public override void toca()
        {
            Console.WriteLine($"toca {nombre} su bajo {Guitarra}\n");
        }
    }

    class Baterista:Musico
    {
        private string bateria;
        public Baterista(string no, string bateria):base(no)
        {
            this.bateria = bateria;
        }

        public override void Afina()
        {
            Console.WriteLine($"{nombre} esta afinando su bateria {bateria}");
        }
        
        public override void Saluda()
        {
            Console.WriteLine($"hola soy {nombre} y soy baterista");
        }

        public override void toca()
        {
            Console.WriteLine($"toca {nombre} su bateria {bateria}");
        }
    }


    class Program
    { 

        static void Main()
        {   
            List<Musico> grupo = new List<Musico>();
            grupo.Add(new Musico("Tom"));
            grupo.Add(new Bajista("Flea", "Gibson"));
            grupo.Add(new Guitarrista("Alfred", "Red Special"));
            grupo.Add(new Baterista("Charly", "Yamaha"));
            foreach(Musico m in grupo)
            {
                m.Saluda();
                m.Afina();
                m.toca();
            }

        }
    }
}