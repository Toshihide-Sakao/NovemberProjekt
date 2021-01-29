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
            Champion ezreal = new Champion(new Color(222, 224, 92, 255), 20, 0);

            // spawn one melee minion
            Melee test1 = new Melee(new Color(201, 27, 14, 255), 15, 1);
            
            // initialize window
            Raylib.InitWindow(aram.xMax, aram.yMax, "Bruh of Bruhhhhhhing");

            // set fps
            Raylib.SetTargetFPS(144);
            
            // gets UI
            UserInterface ezUI = new UserInterface(200, 650, ezreal);

            // game loop
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                
                // draw characters
                aram.Draw(Character.list);

                // Dectects damage and stuff
                Detector.Detect();

                // update UI
                ezUI.update();

                Raylib.EndDrawing();
            }
        }
    }
}
