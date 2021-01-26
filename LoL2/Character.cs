using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
	public class Character
	{
		int damage;
		protected Vector2 spawnPos;
		protected int moveSpeed;

		public float HP { get; set; } 
		public Vector2 Position;
		public Color graphic;
		
		public static List<Character> list = new List<Character>();

		public Character()
		{
			graphic = new Color(222, 224, 92, 255);
			list.Add(this);
		}
		
		public virtual void Inputs()
		{

		}
	}
}
