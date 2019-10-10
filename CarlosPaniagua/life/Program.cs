using System;
using System.Collections.Generic;

namespace life
{
    enum Estado {viva, vacia}

    class Celula
    {
        public Estado estado_actual;
        public Estado estado_siguiente;
        public Tablero tablero;
        public short renglon;
        public short columna;

        public Celula(Estado inicial, Tablero tablero, short renglon, short columna)
        {
            estado_actual = inicial;
            estado_siguiente = inicial;
            this.tablero = tablero;
            this.renglon = renglon;
            this.columna = columna;
        }

        public void actualiza_estado()
        {
            estado_actual = estado_siguiente;
        }
        
        public void actualiza_estado_siguiente()
        {
           //actualiza estado siguiente
           //siguiendo las reglas del juego
           //ejemplo
           short vecinas = num_vecinas();

            if(estado_actual == Estado.viva && (vecinas < 2 || vecinas > 3))
            {
                estado_siguiente = Estado.vacia;
            }

            if(estado_actual == Estado.vacia)
            {
                if(vecinas == 3) estado_siguiente = Estado.viva;
            }

        }   

        public short num_vecinas()
        {
             short cuenta = 0;
            //arriva izquierda
              if  (tablero.posval(renglon - 1, columna - 1) && tablero.estadovov(renglon - 1, columna - 1) == Estado.viva)
              {
                  cuenta++;
              }
            
            //arriva
            if  (tablero.posval(renglon - 1, columna) && tablero.estadovov(renglon - 1, columna) == Estado.viva)
              {
                  cuenta++;
              }
            

            //arriva derecha
            if  (tablero.posval(renglon - 1, columna + 1) && tablero.estadovov(renglon - 1, columna + 1) == Estado.viva)
              {
                  cuenta++;
              }
            
            //centro izquierda
            if  (tablero.posval(renglon, columna - 1) && tablero.estadovov(renglon, columna - 1) == Estado.viva)
              {
                  cuenta++;
              }
            
            //centro derecha
            if  (tablero.posval(renglon, columna + 1) && tablero.estadovov(renglon, columna + 1) == Estado.viva)
              {
                  cuenta++;
              }
            
            //abajo izquierda
            if  (tablero.posval(renglon + 1, columna - 1) && tablero.estadovov(renglon + 1, columna - 1) == Estado.viva)
              {
                  cuenta++;
              }
            
            //abajo centro
            if  (tablero.posval(renglon + 1, columna) && tablero.estadovov(renglon + 1, columna) == Estado.viva)
              {
                  cuenta++;
              }
            
            //abajo derecha
            if  (tablero.posval(renglon + 1, columna + 1) && tablero.estadovov(renglon + 1, columna + 1) == Estado.viva)
              {
                  cuenta++;
              }
             
            return cuenta;
        }

         public char type()
        {
            if(this.estado_actual == Estado.vacia)
            {
                return '▒';
            }
            else
            {
                return '█';
            }
        }

      
    }
    class Tablero
    {
        public List<List<Celula >> grid;
        public short num_renglones, num_columnas;
        public Tablero(short num_renglones, short num_columnas)
        {
            grid = new List<List<Celula>>();
            this.num_columnas = num_columnas;
            this.num_renglones = num_renglones;
            for (short i=0; i<=num_renglones; i++)
            {
                grid.Add(new List<Celula>());
                for(short j=0; j <= num_columnas; j++)
                {
                    grid[i].Add(new Celula(Estado.vacia, this, i,j));
                }
            }
        }

        public void inserta(Celula c)
        {
            grid[c.renglon][c.columna] = c;
        }

        public void print()
        {
            string buff="";
            foreach(List<Celula> renglon in grid)
            {
                
                foreach(Celula c in renglon )
                {
                    buff += c.type();
                }
                buff += "\n";
            }
            Console.WriteLine(buff);
        }
        public void actualiza_estado_todas()
        {
                foreach(List<Celula> renglon in grid)
                {
                    foreach(Celula c in renglon)
                    {
                        c.actualiza_estado_siguiente();
                    }
                }
        }
        //cambia el estado de todas las celdas
        public void turno_siguiente()
        {
            foreach(List<Celula> renglon in grid)
            {
                foreach(Celula c in renglon)
                {
                    c.actualiza_estado();
                }
            }
        }

        //si una celula no exoste en el tablero es nula
        public bool posval(int renglon, int columna)
        {
            if((renglon < 0 || renglon >= num_renglones) || (columna < 0 || columna >= num_columnas))
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        //se sabe si esta viva o vacia
        public Estado estadovov(int renglon, int columna)
        {
            return grid[renglon][columna].estado_actual;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int opcion = 1, velocidad = 150, tres;
            short columna = 8, renglon = 8;
            Tablero GoL = new Tablero(renglon, columna);
            GoL.inserta(new Celula(Estado.viva, GoL, 2, 1));
            GoL.inserta(new Celula(Estado.viva, GoL, 2, 2));
            GoL.inserta(new Celula(Estado.viva, GoL, 2, 3));
           
            while(opcion >= 1)
            {
                
                Console.Clear();
                Console.WriteLine($"Tablero actual {renglon} * {columna}\n");
                GoL.print();
                Console.WriteLine("¿Que deseas hacer?\n1.- Incertar 1 celula.\n2.- siguiente turno.\n3.- Avanzar 15 Turnos.\n4.- Cambiar tamaño de tablero\n5.- Salir");
                opcion = int.Parse(Console.ReadLine());
                if(opcion == 1)
                {
                    Console.Clear();
                    GoL.print();
                    Console.WriteLine($"recuerda que debe ser mayor o igual a 0 y no mayor a los renglones y columas actuales: \n{renglon} * {columna}");
                    Console.WriteLine("Inserta la columna de la celula");
                    columna = short.Parse(Console.ReadLine());
                    Console.WriteLine("Inserta el renglon de la celula");
                    renglon = short.Parse(Console.ReadLine());
                    GoL.inserta(new Celula(Estado.viva, GoL, renglon, columna));
                }
                
                else if(opcion == 2)
                {
                    GoL.actualiza_estado_todas();
                    GoL.turno_siguiente();
                }

                else if(opcion == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Escoje:\n1.- Cambiar velocidad {0}ms\n2.- Continuar",velocidad);
                    tres = int.Parse(Console.ReadLine());
                    if(tres == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"Introduce la velocidad en milisegundos\nActual: {velocidad}");
                        velocidad = int.Parse(Console.ReadLine());
                        for(int i = 0; i < 15; i++) 
                        {
                        
                            Console.Clear();
                            GoL.actualiza_estado_todas();
                            GoL.turno_siguiente();
                            GoL.print();

                            //se detiene por unos milisegundos y cambia si gusta el usuario
                            System.Threading.Thread.Sleep(velocidad);
               
                        }
                    }
                    else if(tres == 2)
                    {
                        for(int i = 0; i < 15; i++) 
                        {
                        
                            Console.Clear();
                            GoL.actualiza_estado_todas();
                            GoL.turno_siguiente();
                            GoL.print();

                            //se detiene por unos milisegundos predeterminados
                            System.Threading.Thread.Sleep(velocidad);
               
                        }
                    }
                }
 
                else if(opcion == 4)
                {
                    Console.Clear();
                    Console.WriteLine($"recuerda que el tablero va de 0 a #X\nActual: R: {renglon} * C: {columna}");
                    Console.WriteLine("Introduce el renglon del tablero:");
                    renglon = short.Parse(Console.ReadLine());
                    Console.WriteLine("Introduce la columna de el tablero:");
                    columna = short.Parse(Console.ReadLine());
                    GoL = new Tablero(renglon, columna);
                    GoL.inserta(new Celula(Estado.viva, GoL, 2, 1));
                    GoL.inserta(new Celula(Estado.viva, GoL, 2, 2));
                    GoL.inserta(new Celula(Estado.viva, GoL, 2, 3));
                }
           
                else if(opcion == 5)
                {
                    Console.Clear();
                    opcion = 0;
                }

            }   

            //GoL.print();
            //actualizar el estado de todas las celulas
            //actualizar el estado actual con el siguiente
            //volver a imprimir
            //repetir haciendo pausas

           
        
        }
    }
}
