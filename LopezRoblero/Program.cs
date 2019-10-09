using System;
using System.Collections.Generic;

//Reglas
//1.-Una célula muerta con exactamente 3 células vecinas vivas "nace" 
//(es decir, al turno siguiente estará viva).
//2.-Una célula viva con 2 o 3 células vecinas vivas sigue viva, en otro caso muere 
//(por "soledad" o "superpoblación").

namespace LopezRoblero
{
    enum Estado { viva, vacia}
    class Celula 
    {
        public Estado estado_actual;
        public Estado estado_siguiente;
        public Tablero tablero;
        public short renglon;
        public short columna;
        
        public Celula(Estado inicial, Tablero tablero ,   short renglon, short columna)
        {
            estado_actual = inicial;
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
                if(estado_actual==Estado.viva && (num_vecinas()<2 || num_vecinas()>3 ))
             {
                 estado_siguiente=Estado.viva;
             }
        }

        public short num_vecinas()
        {   short cuenta = 0;

          /*1,2,3
            4,x,5
            6,7,8 */
            
            //Vecina 1 
            if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //vecina 2
                if(columna>0)
                {
                     if(  tablero.grid[renglon][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //Vecina 3
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //vecina 4
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //Vecina 5
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //Vecina 6
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //Vecina 7
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                //Vecina 8
                if (renglon > 0  && columna > 0)
                {
                  if(  tablero.grid[renglon-1][columna-1].estado_actual == Estado.viva)
                      cuenta++;
                }
                
              

            // Falta hacer lo mismo para las otras vecinas

            return cuenta;
        } 

        public void print()
        {
           if (this.estado_actual == Estado.vacia){
                Console.Write("▒");
           } 
           else 
           {
                Console.Write("█");
           }
            
        }
    }

    class Tablero 
    {
        public List<List<Celula >> grid;
        public Tablero(short num_renglones, short num_columnas){
              grid = new List<List<Celula>>(); 
              for (short i=0; i<= num_renglones-1; i++)
              {
                 grid.Add(new List<Celula>()); 
                 for (short j = 0; j <= num_columnas-1; j++)
                 {
                    grid[i].Add(new Celula(Estado.vacia, this, i,j));
                 }
              }

        }

        public void actualiza_estado_todas()
        {
            foreach(List<Celula> renglon in grid)
            {
               foreach(Celula c in renglon)
               {
                    c.actualiza_estado();
                }         
            }                  
        } 
        

        //Cambia el estado de todas las celdas


        public void inserta(Celula c)
        {
                grid[c.renglon][c.columna] = c;
        }

        public void print()
        {
            foreach(List<Celula> renglon in grid)
            {
               foreach(Celula c in renglon)
               {
                    c.print();
                }         
                Console.WriteLine("");
            }                  
        } 
    }


    class Program
    {
        static void Main(string[] args)
        {
             Tablero GoL = new Tablero(10,5);
             GoL.inserta( new Celula(Estado.viva,GoL, 3,3  ) );
             GoL.inserta( new Celula(Estado.viva,GoL, 3,2  ) );
             GoL.inserta( new Celula(Estado.viva,GoL, 3,1  ) );
             GoL.inserta( new Celula(Estado.viva,GoL, 0,0  ) );
             
             GoL.print(); 
             
             // Actualizar el estado_siguiente de todas las celulas
             // Actualizar el estado actual con el siguiente
             // Volver a imprimir
             // Repetir haciendo una pausa  
        }
    }
}
