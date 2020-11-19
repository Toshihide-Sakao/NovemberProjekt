using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class Map
    {
        Vector2 max;
        Color backgroundColor = new Color(76, 151, 217, 1);

        public void Draw()
        {
            
        }

        public Map()
        {
            max.X = 800;
            max.Y = 800;
        }

        public void Draw(List<Character> charList) 
        {
            drawMain();

            for (int i = 0; i < charList.Count; i++)
            {
                drawCharacter(charList[i]);
            }
        }

        void drawMain()
        {
            Raylib.ClearBackground(backgroundColor);
            Raylib.DrawRectangle(0, 0, 800, 250, Color.BROWN);
            Raylib.DrawRectangle(0, 550, 800, 250, Color.BROWN);
        }

        void drawCharacter(Character guy)
        {
            Raylib.DrawCircle((int)guy.Position.X, (int)guy.Position.Y, 20, Color.YELLOW);

            guy.Inputs();
        }

        private void setMap(List<Character> charList)
        {
            
        }
        public int xMax
        {
            get { return (int)max.X; }
            set { max.X = value; }
        }

        public int yMax
        {
            get { return (int)max.Y; }
            set { max.Y = value; }
        }
        
    }
}
