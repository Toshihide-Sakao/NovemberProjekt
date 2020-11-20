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
        double xMoveAmount;
        double yMoveAmount;

        Vector2 mousePos = new Vector2(0, 0);

        int counter = 1000000;



        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 3;
        }

        public override void Inputs()
        {
            Move();
            Cast();



        }

        public void Move()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                counter = 0;
                mousePos = Raylib.GetMousePosition();
            }
            Moving(counter, mousePos);
        }

        public void Cast()
        {
            if (Raylib.IsKeyPressed(q))
                qAbillity();
        }

        public void qAbillity()
        {
            Raylib.DrawCircle((int)Position.X + 30, (int)Position.Y, 10, Color.YELLOW);
        }

        public void Moving(int counter, Vector2 mousePos)
        {
            xMoveAmount = (int)(mousePos.X - Position.X);
            yMoveAmount = (int)(mousePos.Y - Position.Y);
            int totalMoveAmount = (int)Math.Sqrt((Math.Pow(xMoveAmount, 2) + Math.Pow(yMoveAmount, 2)));
            int forMoveAmount = totalMoveAmount / moveSpeed;
            float mSpeedX = 0;
            float mSpeedY = 0;

            if ((int)xMoveAmount != 0 && (int)forMoveAmount != 0)
            {
                mSpeedX = (float)xMoveAmount / forMoveAmount;
            }
            if ((int)yMoveAmount != 0 && (int)forMoveAmount != 0)
            {
                mSpeedY = (float)yMoveAmount / forMoveAmount;
            }

            if (counter <= forMoveAmount)
            {
                Position.X += mSpeedX;
                Position.Y += mSpeedY;
                counter++;
            }
        }
    }
}
