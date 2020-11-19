using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class Champion : Character
    {
        public KeyboardKey q = KeyboardKey.KEY_Q;
        public KeyboardKey w = KeyboardKey.KEY_W;
        public KeyboardKey e = KeyboardKey.KEY_E;
        public KeyboardKey r = KeyboardKey.KEY_R;
        public KeyboardKey d = KeyboardKey.KEY_D;
        public KeyboardKey f = KeyboardKey.KEY_F;


        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;
        }

        public override void Inputs()
        {
            if (Raylib.IsKeyPressed(q))
                Raylib.DrawCircle((int)Position.X + 20, (int)Position.Y + 20, 10, Color.YELLOW);

        }

        public void Move()
        {
            bool moveX = true;
            bool moveY = true;
            int mouseX = 0;
            int mouseY = 0;


            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                mouseX = Raylib.GetMouseX();
                mouseY = Raylib.GetMouseY();

                int xMoveAmount = mouseX - (int)Position.X;
                int yMoveAmount = mouseY - (int)Position.Y;
            }
            if (moveX)
            {
                if (Position.X != mouseX)
                {
                    Position.X += 1;
                }
                else
                {
                    moveX = false;
                }
            }

            if (moveY)
            {
                if (Position.Y != mouseY)
                {
                    Position.Y += 1;
                }
                else
                {
                    moveY = false;
                }
            }


        }
    }
}
