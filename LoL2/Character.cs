using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
	public class Character
	{
		int damage; // Not Used yet
		protected Vector2 spawnPos; // spawn position
		protected int moveSpeed; // Character Movement speed

		public float HP { get; set; } // Character HP
		public Vector2 Position; // Character position
		public Color graphic; // Color for character
		public int size;
		public int team;
		public int[] abillityDamages = new int[0];
		
		public static List<Character> list = new List<Character>(); // List for all characters

		// Constructor for Charachter
		public Character()
		{
			list.Add(this); // adding char to list
		}

		public virtual void Inputs()
		{

		}

		public virtual string CheckDerived()
		{
			return "Character";
		}
	}
}
