using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class Map
    {
        Vector2 maxLengths;
        int[,] map;
        Color backgroundColor = new Color(207, 167, 89, 1);

        public void Draw()
        {
            Raylib.ClearBackground(backgroundColor);

            for (int y = 0; y < maxLengths.Y; y++)
            {
                for (int x = 0; x < maxLengths.X; x++)
                {

                }
            }
        }
        public Map()
        {

        }

        public Map(List<Character> charList)
        {
            setMap(charList);
        }

        private void setMap(List<Character> charList)
        {
            map = new int[(int)maxLengths.X, (int)maxLengths.Y];

            for (int y = 0; y < maxLengths.Y; y++)
            {
                for (int x = 0; x < maxLengths.X; x++)
                {
                    foreach (var item in charList)
                    {
                        if (item.Position == new Vector2((float)x, (float)y) )
                        {
							//if character exits here
                        }
                    }
                }
            }
        }
    }
}
