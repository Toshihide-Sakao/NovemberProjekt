﻿using System;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    class Program
    {
        static void Main(string[] args)
        {
            Map aram = new Map();
            Champion ezreal = new Champion();
            
            
            Raylib.InitWindow(aram.xMax, aram.yMax, "Bruh of Bruhhhhhhing");
            Raylib.SetTargetFPS(60);
            
            UserInterface ezUI = new UserInterface(200, 700, ezreal);
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                
                aram.Draw(Champion.list);
                ezUI.update();


                Raylib.EndDrawing();
            }
        }
    }
}
