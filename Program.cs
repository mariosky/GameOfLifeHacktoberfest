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
			estado_siguiente = inicial;
			this.tablero = tablero;
			this.renglon = renglon;
			this.columna = columna;
		}
		public void actualiza_estado(){
			estado_actual = estado_siguiente;
		}

		public void actualiza_estado_siguiente(){
			short vecinas = num_vecinas();
			if(estado_actual == Estado.viva && (vecinas < 2 || vecinas > 3)) {
				estado_siguiente = Estado.vacia;
			}
			if(estado_actual == Estado.vacia && vecinas == 3) {
				estado_siguiente = Estado.viva;
			}
		}

		public short num_vecinas()
		{
			short cuenta = 0;
			// Verificar las celulas vecinas en un orden de las 8 alrededor de ELLA:
			// XXX
			// XOX
			// XXX
			// Renglon anterior
			if(renglon > 0) {
				if(columna > 0 && tablero.cell_in_pos(renglon-1, columna-1).estado_actual == Estado.viva)
					cuenta++;
				if(tablero.cell_in_pos(renglon-1, columna).estado_actual == Estado.viva)
					cuenta++;
				if(columna < tablero.num_columnas-1 && tablero.cell_in_pos(renglon-1, columna+1).estado_actual == Estado.viva)
					cuenta++;
			}
			// Renglon actual
			if(columna > 0 && tablero.cell_in_pos(renglon, columna-1).estado_actual == Estado.viva)
				cuenta++;
			if(columna < tablero.num_columnas-1 && tablero.cell_in_pos(renglon, columna+1).estado_actual == Estado.viva)
				cuenta++;

			// Renglon siguiente
			if(renglon < tablero.num_renglones-1)
			{
				if(columna > 0 && tablero.cell_in_pos(renglon+1, columna-1).estado_actual == Estado.viva)
					cuenta++;
				if(tablero.cell_in_pos(renglon+1, columna).estado_actual == Estado.viva)
					cuenta++;
				if(columna < tablero.num_columnas-1 && tablero.cell_in_pos(renglon+1, columna+1).estado_actual == Estado.viva)
					cuenta++;
			}
			return cuenta;
		} 

		public char symbol(){
		   if (this.estado_actual == Estado.vacia){
				return '▒';
		   } 
		   else 
		   {
				return '█';
		   }
			
		}
	}

	class Tablero {
		private List<List<Celula >> grid;
		private short _num_renglones;
		private short _num_columnas;
		public short num_renglones {
			get {
				return _num_renglones;
			}
		}
		public short num_columnas {
			get {
				return _num_columnas;
			}
		}
		public Tablero(short num_renglones, short num_columnas){
			_num_renglones = num_renglones;
			_num_columnas = num_columnas;
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

		public void actualizar_tablero(){
			foreach(List<Celula> renglon in grid)
			{
				foreach(Celula c in renglon)
				{
					c.actualiza_estado_siguiente();
				}
			}       
		}
		public void siguiente_turno(){
			foreach(List<Celula> renglon in grid)
			{
				foreach(Celula c in renglon)
				{
					c.actualiza_estado();
				}
			}
		}
		public void avanzar_turnos(int turnos, int ticks) {
			for(int i = 0; i < turnos; i++) {
				Console.Clear();
				print();
				actualizar_tablero();
				siguiente_turno();
				System.Threading.Thread.Sleep(ticks);
			}
		}
		//Cambia el estado de todas las celdas


		public void inserta(Celula c){
			if((c.renglon >= 0 && c.renglon <= num_renglones) && (c.columna >= 0 && c.columna <= num_columnas))
				grid[c.renglon][c.columna] = c;
		}
		public void inserta_mapa(short [,] pos){
			for(int i = 0; i < pos.GetLength(0); i++) {
				inserta(new Celula(Estado.viva, this, pos[i, 0], pos[i, 1]));
			}
		}
		public Celula cell_in_pos(int renglon, int columna) {
			if(!(renglon >= 0 && renglon < num_renglones))
				throw new ArgumentException("El renglon especificado esta fuera del rango ("+renglon+", MAX "+num_renglones+") Params: ("+renglon+", "+columna+")", "renglon");
			else if(!(columna >= 0 && columna < num_columnas))
				throw new ArgumentException("La columna especificada esta fuera del rango ("+columna+", MAX "+num_columnas+") Params: ("+renglon+", "+columna+")", "columna");
			else
				return grid[renglon][columna];
		}
		public void print(bool show_directions = false){
			string buffer = "";
			if(show_directions)
				buffer+="   R\n   _\nC |";
			foreach(List<Celula> renglon in grid)
			{
				if(show_directions && grid.IndexOf(renglon) != 0)
					buffer+="   ";
				foreach(Celula c in renglon)
				{
					buffer += c.symbol();
				}      
				buffer+="\n";
			}
			Console.WriteLine(buffer);
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
/*
			// Glider Gun, hacer tablero de 15, 40
			short[,] glider_gun = new short[,] {
				{0, 24},
				{1, 22}, {1, 24},
				{2, 12}, {2, 13}, {2, 20}, {2, 21}, {2, 34}, {2, 35},
				{3,11}, {3,15}, {3,20}, {3,21}, {3,34}, {3,35},
				{4,0}, {4,1}, {4,10}, {4,16}, {4,20}, {4,21},
				{5,0}, {5,1}, {5,10}, {5,14}, {5,16}, {5,17}, {5,22}, {5, 24},
				{6,10}, {6,16}, {6,24},
				{7,11}, {7,15},
				{8,12}, {8,13}
			};
*/
			Tablero GoL = new Tablero(10,5);
			GoL.inserta_mapa(new short[,] {
				{3, 3},
				{3, 2},
				{3, 1},
				{0, 0}
			});
			int input = 1;
			while(input == 1)
			{
				Console.Clear();
				Console.WriteLine("Tablero de juego:\n");
				GoL.print();
				Console.WriteLine("Menu de opciones\n 1.- Siguiente Turno\n 2.- Modificar tablero\n 3.- Adeltantar 50 turnos");
				input = int.Parse(Console.ReadLine());
				if(input == 1) {
					GoL.actualizar_tablero();
					GoL.siguiente_turno();
				} else if(input == 2) {
					Console.Clear();
					GoL.print(true);
					Console.WriteLine("(Recuerda que los renglones y columnas empiezan de 0)");
					InvalidPos:
					Console.Write("Ingrese las posiciones de la celula en formato '(R)englon,(C)olumna,viva': ");
					var pos = Console.ReadLine().Split(",");
					if(pos.Length != 3)
					{
						Console.WriteLine("Deben ser 3 parametros en formato '(R)englon,(C)olumna,viva', ejemplo: 0,1,1");
						goto InvalidPos;
					} else {
						short x = short.Parse(pos[0]);
						short y = short.Parse(pos[1]);
						string estado = pos[2];
						if(!(x >= 0 && x < GoL.num_renglones)) {
							Console.WriteLine("El renglon especificado esta fuera de los alcances del tablero.\n");
							goto InvalidPos;
						} else if(!(y >= 0 && y < GoL.num_columnas)) {
							Console.WriteLine("El renglon especificado esta fuera de los alcances del tablero.\n");
							goto InvalidPos;
						} else if(estado != "1" && estado != "0" && estado != "false" && estado != "true") {
							Console.WriteLine("El parametro 'viva' tiene que ser true/false, o 0/1.\n");
							goto InvalidPos;
						} else {
							input = 1;
							bool viva = false;
							if(estado == "1" || estado == "true")
								viva = true;
							GoL.inserta(new Celula((viva ? Estado.viva : Estado.vacia), GoL, x, y));
						}
					}
				} else if(input == 3) {
					// 50 Turnos cada 150 ticks (0.150 segundos o 150 milisegundos)
					GoL.avanzar_turnos(50, 150);
					input = 1;
				}
			}
			Console.WriteLine("Gracias por jugar el 'Juego de la Vida'");
		}
	}
}
