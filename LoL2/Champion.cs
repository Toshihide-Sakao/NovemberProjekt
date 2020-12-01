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

        Vector2 moveMousePos = new Vector2(0, 0);
        Vector2 qMousePos = new Vector2();
        Vector2 wMousePos = new Vector2();
        Vector2 eMousePos = new Vector2();
        Vector2 rMousePOs = new Vector2();

        int counter = 1000000;
        int qLengthCounter = 1000000;
        int wLengthCounter = 1000000;

        Vector2 qPos;



        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 2;

            qPos = Position;
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
                moveMousePos = Raylib.GetMousePosition();
            }
            Moving(moveMousePos);
        }

        public void Cast()
        {
            if (Raylib.IsKeyPressed(q))
            {
                qLengthCounter = 0;
                qMousePos = Raylib.GetMousePosition();
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
            qAbillity();
        }

        public void qAbillity()
        {
            int qLength = 100;
            int qSpeed = 5;
<<<<<<< Updated upstream
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

            
=======
            float ratio = qLength / CalcTotalMoveAmount(qMousePos); // calcing ratio between mousepos and targetpos
            // int totalMoveAmount = (int)(CalcTotalMoveAmount(mousePos) * ratio); // calculating total amount to move to reach target. (diagonal)

            int forMoveAmount = qLength / qSpeed; // amount to move every frame.

            Console.WriteLine(qMousePos);
            Vector2 bruh = new Vector2((qMousePos.X - Position.X) * ratio, (qMousePos.Y - Position.Y) * ratio);
            // Vector2 targetPos = new Vector2(bruh.X * ratio, bruh.Y * ratio);
            
            Vector2 mSpeed = GetMoveSpeed(bruh, forMoveAmount);



            if (qLengthCounter <= forMoveAmount)
            {
                qPos.X += mSpeed.X;
                qPos.Y += mSpeed.Y;
                qLengthCounter++;
            }

            Raylib.DrawCircle((int)qPos.X, (int)qPos.Y, 10, Color.RED);
>>>>>>> Stashed changes
        }

        public void Moving(Vector2 mousePos)
        {
            int forMoveAmount = (int)CalcTotalMoveAmount(mousePos) / moveSpeed;
            Vector2 mSpeed = GetMoveSpeed(CalcMoveAmountXY(mousePos), forMoveAmount);

            if (counter <= forMoveAmount)
            {
                Position.X += mSpeed.X;
                Position.Y += mSpeed.Y;
            }
        }

        public Vector2 GetMoveSpeed(Vector2 targetPos, int forMoveAmount)
        {
            float mSpeedX = 0;
            float mSpeedY = 0;

            if ((int)targetPos.X != 0 && (int)forMoveAmount != 0)
            {
                mSpeedX = (float)targetPos.X / forMoveAmount;
            }
            if ((int)targetPos.Y != 0 && (int)forMoveAmount != 0)
            {
                mSpeedY = (float)targetPos.Y / forMoveAmount;
            }

            return new Vector2(mSpeedX, mSpeedY);
        }

        public Vector2 CalcMoveAmountXY(Vector2 allaj)
        {
            return new Vector2(allaj.X - Position.X, allaj.Y - Position.Y);
        }

        public float CalcTotalMoveAmount(Vector2 targetPos)
        {
            Vector2 moveAmount = CalcMoveAmountXY(targetPos);
            float totalMoveAmount = (float)Math.Sqrt((Math.Pow(moveAmount.X, 2) + Math.Pow(moveAmount.Y, 2)));

            return totalMoveAmount;
        }

        public void bruuuuh(Vector2 targetPos, float totalMoveAmount, int speed)
        {

        }
    }
}
