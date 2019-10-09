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
            short vecinas = num_vecinas();
            if(estado_actual==Estado.viva && (vecinas < 2 || vecinas >3 ))
            {
                estado_siguiente=Estado.vacia;
            }
            if(estado_actual==Estado.vacia && vecinas == 3)
            {
                estado_siguiente=Estado.viva;
            }
        }

        public short num_vecinas()
        {   short cuenta = 0;

          /*1,2,3
            4,x,5
            6,7,8 */
            // Renglon anterior
            if(tablero.posicion_valida(renglon-1, columna-1) && tablero.celula_posicion_estado(renglon-1, columna-1) == Estado.viva) {
                cuenta++;
            }
            if(tablero.posicion_valida(renglon-1, columna) && tablero.celula_posicion_estado(renglon-1, columna) == Estado.viva) {
                cuenta++;
            }
            if(tablero.posicion_valida(renglon-1, columna+1) && tablero.celula_posicion_estado(renglon-1, columna+1) == Estado.viva) {
                cuenta++;
            }
            // Renglon actual
            if(tablero.posicion_valida(renglon, columna-1) && tablero.celula_posicion_estado(renglon, columna-1) == Estado.viva) {
                cuenta++;
            }
            if(tablero.posicion_valida(renglon, columna+1) && tablero.celula_posicion_estado(renglon, columna+1) == Estado.viva) {
                cuenta++;
            }
            // Renglon siguiente
            if(tablero.posicion_valida(renglon+1, columna-1) && tablero.celula_posicion_estado(renglon+1, columna-1) == Estado.viva) {
                cuenta++;
            }
            if(tablero.posicion_valida(renglon+1, columna) && tablero.celula_posicion_estado(renglon+1, columna) == Estado.viva) {
                cuenta++;
            }
            if(tablero.posicion_valida(renglon+1, columna+1) && tablero.celula_posicion_estado(renglon+1, columna+1) == Estado.viva) {
                cuenta++;
            }

            return cuenta;
        } 

        public string print()
        {
           if (this.estado_actual == Estado.vacia){
                return "▒";
           } 
           else 
           {
                return "█";
           }
            
        }
    }

    class Tablero 
    {
        public List<List<Celula >> grid;
        //
        public short num_renglones, num_columnas;
        public Tablero(short num_renglones, short num_columnas){
              grid = new List<List<Celula>>(); 
              this.num_renglones = num_renglones;
              this.num_columnas = num_columnas;
              for (short i=0; i< num_renglones; i++)
              {
                 grid.Add(new List<Celula>()); 
                 for (short j = 0; j < num_columnas; j++)
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
                    c.actualiza_estado_siguiente();
                }         
            }                  
        }
        public void siguiente_turno()
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

        // Verificar celula en posicion, devuelve una celula nula si no existe en el tablero
        public bool posicion_valida(int renglon, int columna) {
            if((renglon < 0 || renglon >= num_renglones) || (columna < 0 || columna >= num_columnas)) {
                return false;
            } else {
                return true;
            }
        }

        //Devuelve el estado de si esta viva o muerta
        public Estado celula_posicion_estado(int renglon, int columna) {
            return grid[renglon][columna].estado_actual;
        }
        //
        public void inserta(Celula c)
        {
                grid[c.renglon][c.columna] = c;
        }

        public void print()
        {
            string grafica = "";
            foreach(List<Celula> renglon in grid)
            {
               foreach(Celula c in renglon)
               {
                    grafica += c.print();
                }
                grafica += "\n";
            }
            Console.WriteLine(grafica);
        } 
    }


    class Program
    {
        static void Main(string[] args)
        {
             Tablero GoL = new Tablero(10,5);
             GoL.inserta( new Celula(Estado.viva,GoL, 3, 1  ) );
             GoL.inserta( new Celula(Estado.viva,GoL, 3, 2  ) );
             GoL.inserta( new Celula(Estado.viva,GoL, 3, 3  ) );
             GoL.print();
             GoL.actualiza_estado_todas();
             GoL.siguiente_turno();
             GoL.print(); 
           

             // 10 Turnos
            /*for(int i = 0; i < 10; i++) {
                Console.Clear();
                GoL.print();
                GoL.actualiza_estado_todas();
                GoL.siguiente_turno();
                System.Threading.Thread.Sleep(150);
            }*/

             // Actualizar el estado_siguiente de todas las celulas
             // Actualizar el estado actual con el siguiente
             // Volver a imprimir
             // Repetir haciendo una pausa  
        }
    }
}
