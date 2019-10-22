using System;
using System.Collections.Generic;

namespace gol
{
    enum Estado { viva, vacia}
    class Celula {
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
            estado_actual=estado_siguiente;
        }

        public void actualiza_estado_siguiente(){
        //Actualiza estado_siguiente
        //Siguiendo las reglas del juego
        //Ejemplo
        /*if(num_vecinas() == 0)
        {
            estado_siguiente=Estado.viva;
        }*/
        short vecina=num_vecinas();

        //Regla 1
        if(estado_actual == Estado.viva &&(vecina < 2))
        {
          estado_siguiente=Estado.vacia;
        }
        //Regla 2
         if(estado_actual == Estado.viva && (vecina <=3 || vecina >= 2) )
        {
          estado_siguiente=Estado.viva;
        }

        //Regla 3
        if(estado_actual == Estado.viva && (vecina <3 ))
        {
          estado_siguiente=Estado.vacia;
        }
        //Regla 4
        if(estado_actual == Estado.vacia &&(vecina == 3 ))
        {
          estado_siguiente=Estado.viva;
        }
        

        }

        public short num_vecinas()
        {   
            short cuenta = 0;

           
            if (renglon > 0  && columna > 0)
                {
                    //Columna de atras
                      if(  tablero.condicion(renglon-1,columna-1) && tablero.Ubicacion_Celula (renglon-1,columna-1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                      if(  tablero.condicion(renglon+1,columna-1) && tablero.Ubicacion_Celula (renglon+1,columna-1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                      if(  tablero.condicion(renglon,columna-1) && tablero.Ubicacion_Celula (renglon,columna-1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                  //Columna adelante
                      if(  tablero.condicion(renglon-1,columna+1) && tablero.Ubicacion_Celula (renglon-1,columna+1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                      if(  tablero.condicion(renglon+1,columna+1) && tablero.Ubicacion_Celula (renglon+1,columna+1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                     if(  tablero.condicion(renglon,columna+1) && tablero.Ubicacion_Celula (renglon,columna+1) == Estado.viva)
                      {
                          cuenta++; 
                      }
                //Columna de adelante
                     if(  tablero.condicion(renglon-1,columna) && tablero.Ubicacion_Celula (renglon-1,columna) == Estado.viva)
                     {
                          cuenta++; 
                      }
                     if(  tablero.condicion(renglon+1,columna) && tablero.Ubicacion_Celula (renglon+1,columna) == Estado.viva)
                     {
                          cuenta++; 
                      }                 
                }
              
            //falta hacer lo mismo en las otras vecinas

            return cuenta;
        }  

           public void print(){
           if (this.estado_actual == Estado.vacia){
                Console.Write("▒");
           } 
           else 
           {
                Console.Write("█");
           }
            
        }
        
    }

    class Tablero {
        public List<List<Celula >> grid;
        public short num_columnas;
        public short num_renglones;


        public Tablero(short num_renglones, short num_columnas)
        {     
            this.num_columnas=num_columnas;
            this.num_renglones=num_renglones;
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
       public void Avance_turno(){
			foreach(List<Celula> renglon in grid)
			{
				foreach(Celula c in renglon)
				{
					c.actualiza_estado_siguiente();
                    
				}
			}
            
            
		}
        

        public bool condicion(int renglon, int columna) 
        {
            if((renglon < 0 || renglon >= num_renglones) || (columna < 0 || columna >= num_columnas)) 
            {
                return false;
            } else 
            {
                return true;
            }
        }


        public Estado Ubicacion_Celula(int renglon, int columna) 
        {
            return grid[renglon][columna].estado_actual;
        }


       //Cambia el estado de todas las celdas
       
        public void inserta(Celula c){
            if((c.renglon >= 0 ) && (c.columna >= 0 ))
            {
            grid[c.renglon][c.columna] = c;
            }
               
        }
        
        
        public void print(){
            foreach(List<Celula> renglon in grid)
            {
               foreach(Celula c in renglon)
               {
                    c.print();
                }         
                Console.WriteLine("\n");   
            }          
             Console.WriteLine("");        
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
            

             int CaseSwitch;
             do{
             Console.WriteLine("1.Pocision inicial \n2.Inicializar etapas\n3.Finalizar");
            CaseSwitch=Convert.ToInt16(Console.ReadLine());
             switch(CaseSwitch)
             {
               
                case 1:
                 GoL.print();
                 GoL.actualiza_estado_todas();
                 Console.WriteLine(GoL.grid[1][1].num_vecinas()); 
                 break;
                 case 2:
                 for(int i = 0; i < 5; i++) 
                {
                 GoL.print();
                 GoL.actualiza_estado_todas();
                 GoL.Avance_turno(); 
                 System.Threading.Thread.Sleep(350);
                 } 
                 break;
                 case 3:
                 Console.WriteLine("Finalizado");
                 break ;

             }
             }
             while(CaseSwitch!=3);  
                            
           
             
             //Actualizar el estado de todas las celdas
             //Cambiar el estado actual
             //Volver a imprimir
             //Repetir haciendo una pausa 
            
            
					
				
             
        }
    }
}
