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

        Vector2 mousePos = new Vector2(0, 0);

        int counter = 1000000;
        int qLengthCounter = 1000000;
        int wLengthCounter = 1000000;



        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 2;
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
            Moving(mousePos);
        }

        public void Cast()
        {
            
            if (Raylib.IsKeyPressed(q))
            {
                qLengthCounter = 0;
            }
            else if (Raylib.IsKeyPressed(w))
            {
                wLengthCounter = 0;
            }
            else if (Raylib.IsKeyPressed(e))
            {
                
            }
            else if (Raylib.IsKeyPressed(r))
            {
                
            }
            qAbillity(mousePos);
        }

        public void qAbillity(Vector2 mousePos)
        {
            int qLength = 10;
            int qSpeed = 5;
            float ratio = CalcTotalMoveAmount(mousePos) / qLength;
            int forMoveAmount = (int)(CalcTotalMoveAmount(mousePos) * ratio) / qSpeed;
            Vector2 mSpeed = GetMoveSpeed(mousePos, forMoveAmount);
            Vector2 moveAmount = CalcMoveAmountXY(mousePos);
            Vector2 qMoveAmount = new Vector2(moveAmount.X * ratio, moveAmount.Y * ratio);

            if (qLengthCounter <= forMoveAmount)
            {
                // Position.X += mSpeed.X;
                // Position.Y += mSpeed.Y;
                //Vector2 qPos = new Vector2(Position.X + , Position.Y + qMoveAmount.Y);
                Raylib.DrawCircle((int)(Position.X + qMoveAmount.X * (float)qLengthCounter), (int)(Position.Y + qMoveAmount.Y * (float)qLengthCounter), 10, Color.YELLOW);
            }

            
        }

        public void Moving(Vector2 mousePos)
        {
            int forMoveAmount = (int)CalcTotalMoveAmount(mousePos) / moveSpeed;
            Vector2 mSpeed = GetMoveSpeed(mousePos, forMoveAmount);

            if (counter <= forMoveAmount)
            {
                Position.X += mSpeed.X;
                Position.Y += mSpeed.Y;
            }
        }

        public Vector2 GetMoveSpeed(Vector2 mousePos, int forMoveAmount) 
        {
            Vector2 moveAmount = CalcMoveAmountXY(mousePos);
            float mSpeedX = 0;
            float mSpeedY = 0;

            if ((int)moveAmount.X != 0 && (int)forMoveAmount != 0)
            {
                mSpeedX = (float)moveAmount.X / forMoveAmount;
            }
            if ((int)moveAmount.Y != 0 && (int)forMoveAmount != 0)
            {
                mSpeedY = (float)moveAmount.Y / forMoveAmount;
            }

            return new Vector2(mSpeedX, mSpeedY);
        }

        public Vector2 CalcMoveAmountXY(Vector2 mousePos)
        {
            return new Vector2(mousePos.X - Position.X, mousePos.Y - Position.Y);
        }

        public float CalcTotalMoveAmount(Vector2 mousePos)
        {
            Vector2 moveAmount = CalcMoveAmountXY(mousePos);
            float totalMoveAmount = (float)Math.Sqrt((Math.Pow(moveAmount.X, 2) + Math.Pow(moveAmount.Y, 2)));
            
            return totalMoveAmount;
        }
    }
}
