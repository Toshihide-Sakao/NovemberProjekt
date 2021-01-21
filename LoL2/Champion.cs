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
        bool qInProgress = false;
        Vector2 wPos;
        bool wInProgress = false;
        Vector2 ePos;
        bool eInProgress = false;

        float qCooldown = 3.3f;
        float wCooldown = 5.0f;
        float eCooldown = 9.0f;

        DateTime qStart;
        DateTime wStart;
        DateTime eStart;

        public double qCoolDownPassedTime;
        public double wCoolDownPassedTime;
        public double eCoolDownPassedTime;



        public Champion()
        {
            graphic = new Color(222, 224, 92, 1);

            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 2;

            qStart = DateTime.Now;
            wStart = DateTime.Now;
            eStart = DateTime.Now;
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
            qCoolDownPassedTime = qCooldown - (DateTime.Now - qStart).TotalSeconds;
            wCoolDownPassedTime = wCooldown - (DateTime.Now - wStart).TotalSeconds;
            eCoolDownPassedTime = eCooldown - (DateTime.Now - eStart).TotalSeconds;

            // System.Console.WriteLine(qCoolDownPassedTime);

            if (Raylib.IsKeyPressed(q) && qInProgress == false && qCoolDownPassedTime < 0)
            {
                qStart = DateTime.Now;
                qLengthCounter = 0;
                qMousePos = Raylib.GetMousePosition();
                qInProgress = true;
            }
            else if (Raylib.IsKeyPressed(w) && wInProgress == false && wCoolDownPassedTime < 0)
            {
                wStart = DateTime.Now;
                wLengthCounter = 0;
                wMousePos = Raylib.GetMousePosition();
                wInProgress = true;
            }
            else if (Raylib.IsKeyPressed(e) && eCoolDownPassedTime < 0)
            {
                eStart = DateTime.Now;
                eMousePos = Raylib.GetMousePosition();
                eInProgress = true;
            }
            else if (Raylib.IsKeyPressed(r))
            {

            }
            
            wAbillity();
            qAbillity();
            eAbillity();
        }

        public void qAbillity()
        {
            int qLength = 150;
            int qSpeed = 5;
            float ratio = qLength / CalcTotalMoveAmount(qMousePos); // calcing ratio between mousepos and targetpos
            // int totalMoveAmount = (int)(CalcTotalMoveAmount(mousePos) * ratio); // calculating total amount to move to reach target. (diagonal)

            int forMoveAmount = qLength / qSpeed; // amount to move every frame.
            Vector2 bruh = new Vector2((qMousePos.X - Position.X) * ratio, (qMousePos.Y - Position.Y) * ratio);
            // Vector2 targetPos = new Vector2(bruh.X * ratio, bruh.Y * ratio);

            Vector2 mSpeed = GetMoveSpeed(bruh, forMoveAmount);



            if (qLengthCounter <= forMoveAmount)
            {
                qPos.X += mSpeed.X;
                qPos.Y += mSpeed.Y;

                Raylib.DrawCircle((int)qPos.X, (int)qPos.Y, 10, Color.RED);

                qLengthCounter++;
            }
            else
            {
                qPos = Position;
                qInProgress = false;
            }
        }

        public void wAbillity()
        {
            int wLength = 220;
            int wSpeed = 5;
            float ratio = wLength / CalcTotalMoveAmount(wMousePos); // calcing ratio between mousepos and targetpos
            // int totalMoveAmount = (int)(CalcTotalMoveAmount(mousePos) * ratio); // calculating total amount to move to reach target. (diagonal)

            int forMoveAmount = wLength / wSpeed; // amount to move every frame.
            Vector2 bruh = new Vector2((wMousePos.X - Position.X) * ratio, (wMousePos.Y - Position.Y) * ratio);
            // Vector2 targetPos = new Vector2(bruh.X * ratio, bruh.Y * ratio);

            Vector2 mSpeed = GetMoveSpeed(bruh, forMoveAmount);

            if (wLengthCounter <= forMoveAmount)
            {
                wPos.X += mSpeed.X;
                wPos.Y += mSpeed.Y;

                Raylib.DrawCircle((int)wPos.X, (int)wPos.Y, 15, Color.GRAY);

                wLengthCounter++;
            }
            else
            {
                wPos = Position;
                wInProgress = false;
            }
        }

        public void eAbillity()
        {
            int eLength = 170;
            float ratio = eLength / CalcTotalMoveAmount(eMousePos); // calcing ratio between mousepos and targetpos

            Vector2 bruh = new Vector2((eMousePos.X - Position.X) * ratio, (eMousePos.Y - Position.Y) * ratio);

            if (eInProgress)
            {
                Vector2 LimitPos = new Vector2(Position.X + bruh.X, Position.Y + bruh.Y);
                if (CalcTotalMoveAmount(eMousePos) <= 170)
                {
                    Position = eMousePos;
                }
                else
                {
                    Position = LimitPos;
                }
                counter = 1000000;
                eInProgress = false;
            }
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
