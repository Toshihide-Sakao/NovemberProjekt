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
        int xMoveAmount;
        int yMoveAmount;



        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 3;
        }

        public override void Inputs()
        {
            int xCounter = 0;
            int yCounter = 0;

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                xCounter = 0;
                yCounter = 0;
            }
            Move(xCounter, yCounter);

            if (Raylib.IsKeyPressed(q))
                Raylib.DrawCircle((int)Position.X + 30, (int)Position.Y, 10, Color.YELLOW);

        }

        public void Move(int xCounter, int yCounter)
        {
            int mouseX = Raylib.GetMouseX();
            int mouseY = Raylib.GetMouseY();
            xMoveAmount = (mouseX - (int)Position.X) / moveSpeed;
            yMoveAmount = (mouseY - (int)Position.Y) / moveSpeed;

            if (xCounter <= xMoveAmount)
            {
                Position.X += moveSpeed;
                xCounter++;
            }
            if (yCounter <= yMoveAmount)
            {
                Position.Y += moveSpeed;
                yCounter++;
            }
        }
    }
}
