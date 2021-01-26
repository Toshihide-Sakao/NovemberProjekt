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


        int qLengthCounter = 1000000;
        int wLengthCounter = 1000000;

        bool moveInProgress = false;
        Vector2 qPos;
        bool qInProgress = false;
        Vector2 wPos;
        bool wInProgress = false;
        Vector2 ePos;
        bool eInProgress = false;

        float qCooldown = 3.3f;
        float wCooldown = 5.0f;
        float eCooldown = 9.0f;

        DateTime moveStart;
        DateTime qTime;
        DateTime wTime;

        DateTime qStart;
        DateTime wStart;
        DateTime eStart;

        float maxHP;
        float maxMana;
        public float mana = 300f;
        public float hpRecoverByTime = 2f;
        public float manaRecoverByTime = 0.1f;
        DateTime recoverTime;
        public double qCoolDownPassedTime;
        public double wCoolDownPassedTime;
        public double eCoolDownPassedTime;

        float qManaCost = 30f;
        float wManaCost = 50f;
        float eManaCost = 70f;



        public Champion()
        {
            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 2;

            moveStart = DateTime.Now;
            qStart = DateTime.Now;
            wStart = DateTime.Now;
            eStart = DateTime.Now;
            recoverTime = DateTime.Now;

            HP = 1000f;

            maxHP = HP;
            maxMana = mana;
        }

        public override void Inputs()
        {
            Move();
            Cast();
            Recover();
        }

        public void Move()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                moveStart = DateTime.Now;
                moveMousePos = Raylib.GetMousePosition();
                moveInProgress = true;
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
                qTime = DateTime.Now;
                qLengthCounter = 0;
                qMousePos = Raylib.GetMousePosition();
                qInProgress = true;
                qPos = Position;

                mana -= qManaCost;
            }
            else if (Raylib.IsKeyPressed(w) && wInProgress == false && wCoolDownPassedTime < 0)
            {
                wStart = DateTime.Now;
                wTime = DateTime.Now;
                wLengthCounter = 0;
                wMousePos = Raylib.GetMousePosition();
                wInProgress = true;
                wPos = Position;

                mana -= wManaCost;
            }
            else if (Raylib.IsKeyPressed(e) && eCoolDownPassedTime < 0)
            {
                eStart = DateTime.Now;
                eMousePos = Raylib.GetMousePosition();
                eInProgress = true;

                mana -= eManaCost;
            }
            else if (Raylib.IsKeyPressed(r))
            {

            }

            wAbillity();
            qAbillity();
            eAbillity();
        }

        public void Recover()
        {
            double deltaTime = (DateTime.Now - recoverTime).TotalSeconds;
            if (deltaTime >= 1f)
            {
                if (HP + hpRecoverByTime > maxHP)
                {
                    HP = maxHP;
                }
                else
                {
                    HP += hpRecoverByTime;
                }

                if (mana + manaRecoverByTime > maxMana)
                {
                    mana = maxMana;
                }
                else
                {
                    mana += manaRecoverByTime;
                }
            }
        }

        public void qAbillity()
        {
            int qLength = 200;
            int qSpeed = 5;
            float ratio = qLength / CalcTotalMoveAmount(qMousePos); // calcing ratio between mousepos and targetpos

            int forMoveAmount = qLength / qSpeed; // amount to move every frame.
            Vector2 targetLocal = new Vector2((qMousePos.X - Position.X) * ratio, (qMousePos.Y - Position.Y) * ratio); // Target position but in local coordinates

            Vector2 mSpeed = GetMoveSpeed(targetLocal, forMoveAmount);

            if (qInProgress)
            {
                double qTimer = (DateTime.Now - qTime).TotalSeconds;
                if (qTimer >= 0.008f && qLengthCounter <= forMoveAmount)
                {
                    qPos.X += mSpeed.X;
                    qPos.Y += mSpeed.Y;
                    qTime = DateTime.Now;

                    qLengthCounter++;
                }
                if (qLengthCounter > forMoveAmount)
                {
                    qInProgress = false;
                }

                Raylib.DrawCircle((int)qPos.X, (int)qPos.Y, 10, Color.RED);
            }
        }

        public void wAbillity()
        {
            int wLength = 230;
            int wSpeed = 5;
            float ratio = wLength / CalcTotalMoveAmount(wMousePos); // calcing ratio between mousepos and targetpos
            // int totalMoveAmount = (int)(CalcTotalMoveAmount(mousePos) * ratio); // calculating total amount to move to reach target. (diagonal)

            int forMoveAmount = wLength / wSpeed; // amount to move every frame.
            Vector2 targetLocal = new Vector2((wMousePos.X - Position.X) * ratio, (wMousePos.Y - Position.Y) * ratio);
            // Vector2 targetPos = new Vector2(bruh.X * ratio, bruh.Y * ratio);

            Vector2 mSpeed = GetMoveSpeed(targetLocal, forMoveAmount);

            if (wInProgress)
            {
                double wTimer = (DateTime.Now - wTime).TotalSeconds;
                if (wTimer >= 0.008f && wLengthCounter <= forMoveAmount)
                {
                    wPos.X += mSpeed.X;
                    wPos.Y += mSpeed.Y;
                    wTime = DateTime.Now;

                    wLengthCounter++;
                }

                if (wLengthCounter >= forMoveAmount)
                {
                    wPos = Position;
                    wInProgress = false;
                }

                Raylib.DrawCircle((int)wPos.X, (int)wPos.Y, 15, Color.GRAY);
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
                moveInProgress = false;
                eInProgress = false;
            }
        }

        public void Moving(Vector2 mousePos)
        {
            int forMoveAmount = (int)CalcTotalMoveAmount(mousePos) / moveSpeed; // How many pixels it should move
            Vector2 mSpeed = GetMoveSpeed(CalcMoveAmountXY(mousePos), forMoveAmount); // gets actuall value to move every frame for x and y

            if (moveInProgress)
            {
                double moveTimer = (DateTime.Now - moveStart).TotalSeconds;
                if (moveTimer >= 0.01f)
                {
                    Position.X += mSpeed.X;
                    Position.Y += mSpeed.Y;

                    moveStart = DateTime.Now;
                }
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

        // Getting local vector2 based on Position being 0,0
        public Vector2 CalcMoveAmountXY(Vector2 pos)
        {
            return new Vector2(pos.X - Position.X, pos.Y - Position.Y);
        }

        public float CalcTotalMoveAmount(Vector2 targetPos)
        {
            Vector2 moveAmount = CalcMoveAmountXY(targetPos); // making vector2 based on Position being 0,0
            float totalMoveAmount = (float)Math.Sqrt((Math.Pow(moveAmount.X, 2) + Math.Pow(moveAmount.Y, 2)));

            return totalMoveAmount;
        }

        bool CompVector2s(Vector2 pos1, Vector2 pos2)
        {
            bool x = false;
            bool y = false;
            if (pos1.X >= pos2.X - 2.5f && pos1.X <= pos2.X + 2.5f)
            {
                x = true;
            }
            if (pos1.Y >= pos2.Y - 2.5f && pos1.Y <= pos2.Y + 2.5f)
            {
                y = true;
            }

            if (x && y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
