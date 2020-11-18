using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace NovemberProjekt.LoL2
{
	public class Character
	{
		Vector2 position;
		int damage;
		int hp;
		Vector2 spawnPos;
		int moveSpeed;
		static List<Character> list = new List<Character>();

		public Character()
		{
			list.Add(this);
		}

		public void Draw(int type)
		{
			
		}

		public int HP { get; set; } 

		public Vector2 Position { get; set; }
	}
}
