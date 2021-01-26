using System;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate new map
            Map aram = new Map();

            // Gen new champ
            Champion ezreal = new Champion();
            
            // initialize window
            Raylib.InitWindow(aram.xMax, aram.yMax, "Bruh of Bruhhhhhhing");

            // set fps
            Raylib.SetTargetFPS(60);
            
            // gets UI
            UserInterface ezUI = new UserInterface(200, 650, ezreal);

            // game loop
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                
                // draw characters
                aram.Draw(Character.list);

                // update UI
                ezUI.update();

                Raylib.EndDrawing();
            }
        }
    }
}
