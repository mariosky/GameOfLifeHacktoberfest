using System;
using System.Collections.Generic;

namespace LeonMolinaFelixEnrique
{
    enum Estado {viva, vacia}
    class Celula 
    {
        public Estado estado_actual;
        public Estado estado_siguiente;
        public Tablero tablero;
        public short renglon;
        public short columna;
        
        public Celula(Estado inicial, Tablero tablero , short renglon, short columna)
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
            /*Actualiza estado_siguiente
            Siguiendo las reglas del Juego       
            Ejemplo:
            if (num_vecinas() == 1)
            estado_siguiente = Estado.viva;*/
            
            short vecinas = num_vecinas();
			
            if(estado_actual == Estado.viva && (vecinas < 2 || vecinas > 3)) 
            {
				estado_siguiente = Estado.vacia;
            }
            
            if(estado_actual == Estado.vacia && vecinas == 3) 
            {
				estado_siguiente = Estado.viva;
			}

        }

        public short num_vecinas()
        {   
            short cuenta = 0; 
            
            if (renglon > 0)
            {
                //Vecina 1,1
                if(tablero.verificar(renglon-1, columna-1) && tablero.celula_posicion(renglon-1, columna-1) == Estado.viva)
                {
                    cuenta++;
                }
                
                //Vecina 1,2
                if(tablero.verificar(renglon-1, columna) && tablero.celula_posicion(renglon-1, columna) == Estado.viva) 
                {
                    cuenta++;
                }
                
                //Vecina 1,3
                if(tablero.verificar(renglon-1, columna+1) && tablero.celula_posicion(renglon-1, columna+1) == Estado.viva) 
                {
                 cuenta++;
                }
               
                //Vecina 2,1
                if(tablero.verificar(renglon, columna-1) && tablero.celula_posicion(renglon, columna-1) == Estado.viva) 
                {
                    cuenta++;
                }
                
                //Vecina 2,3
                if(tablero.verificar(renglon, columna+1) && tablero.celula_posicion(renglon, columna+1) == Estado.viva) 
                {
                    cuenta++;
                }
                
                // Vecina 3,1
                if(tablero.verificar(renglon+1, columna-1) && tablero.celula_posicion(renglon+1, columna-1) == Estado.viva) 
                {
                    cuenta++;
                }
                
                //Vecina 3,2
                if(tablero.verificar(renglon+1, columna) && tablero.celula_posicion(renglon+1, columna) == Estado.viva) 
                {
                    cuenta++;
                }
                
                //Vecina 3,3
                if(tablero.verificar(renglon+1, columna+1) && tablero.celula_posicion(renglon+1, columna+1) == Estado.viva) {
                {
                    cuenta++;
                }
			}   
            //Falta hacer lo mismo para las otras vecinas
            return cuenta;
        } 

        public string print()
        {
           if (this.estado_actual == Estado.vacia)
            {
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

        public short num_columna;
        public short num_renglon;

        public Tablero(short num_renglon, short num_columna)
        {
            this.num_renglon = num_renglon;
			this.num_columna = num_columna;

            grid = new List<List<Celula>>(); 
            for (short i=0; i<= num_renglon-1; i++)
            {
                grid.Add(new List<Celula>()); 
                for (short j = 0; j <= num_columna-1; j++)
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
        public void siguiente()
        {
			foreach(List<Celula> renglon in grid)
			{
				foreach(Celula c in renglon)
				{
					c.actualiza_estado();
				}
			}
		}
		public bool verificar(int renglon, int columna) 
        {
            if((renglon < 0 || renglon >= num_renglones) || (columna < 0 || columna >= num_columnas)) 
            {
                return false;
            } else 
            {
                return true;
            }
        }
        public Estado celula_posicion_estado(int renglon, int columna) 
        {
            return grid[renglon][columna].estado_actual;
        }

        //Cambia el estado de todas las celdas
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
            GoL.inserta( new Celula(Estado.viva,GoL, 3,3 ));
            GoL.inserta( new Celula(Estado.viva,GoL, 3,2 ));
            GoL.inserta( new Celula(Estado.viva,GoL, 3,1 ));
            GoL.inserta( new Celula(Estado.viva,GoL, 0,0 ));
            GoL.print(); 
            
            /*Actualizar el estado_siguiente de todas las celulas
            GoL.actualiza_estado_todas();
            
            //Actualizar el estado actual con el siguiente
            GoL.siguiente();
            
            //Volver a imprimir
            GoL.print();*/
            
            //Imprimir el programa en turnos (10)
            for(int i = 0; i < 10; i++) 
            {
                Console.Clear();
                GoL.print();
                GoL.actualiza_estado_todas();
                GoL.siguiente();

                //Repetir haciendo una pausa  
                System.Threading.Thread.Sleep(350);
               
            } 
            
            /*//Repetir haciendo una pausa
            System.Threading.Thread.Sleep(350);*/

        }
    }
    
}
}