using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
	public class Melee : Minion
	{
		public Melee(Color graphic, int size, int team)
		{
			this.graphic = graphic;
			this.size = size;
			this.team = team;

			HP = 100;

			spawnPos = new Vector2(600, 400);
			Position = spawnPos;
		}
	}
}
