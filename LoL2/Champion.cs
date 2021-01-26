using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class Champion : Character
    {
        // Key inputs
        public KeyboardKey q = KeyboardKey.KEY_Q;
        public KeyboardKey w = KeyboardKey.KEY_W;
        public KeyboardKey e = KeyboardKey.KEY_E;
        public KeyboardKey r = KeyboardKey.KEY_R;
        public KeyboardKey d = KeyboardKey.KEY_D;
        public KeyboardKey f = KeyboardKey.KEY_F;

        // Mouse position variables
        Vector2 moveMousePos = new Vector2(0, 0);
        Vector2 qMousePos = new Vector2();
        Vector2 wMousePos = new Vector2();
        Vector2 eMousePos = new Vector2();
        Vector2 rMousePOs = new Vector2();

        // Counters for projectile skills
        int qLengthCounter = 1000000;
        int wLengthCounter = 1000000;

        // Bool for if action is in progress
        bool moveInProgress = false;
        // Position for abillity
        Vector2 qPos;
        bool qInProgress = false;
        Vector2 wPos;
        bool wInProgress = false;
        Vector2 ePos;
        bool eInProgress = false;

        // Cooldowns for abillities
        float qCooldown = 3.3f;
        float wCooldown = 5.0f;
        float eCooldown = 9.0f;

        // Time that resets every timeframe for each usage
        DateTime moveStart;
        DateTime qTime;
        DateTime wTime;

        // Time for when the abillity was casted
        DateTime qStart;
        DateTime wStart;
        DateTime eStart;

        // REcord maxHP and max mana
        float maxHP;
        float maxMana;

        // mana
        public float mana = 300f;

        // REcovery which occurs by time
        public float hpRecoverByTime = 2f;
        public float manaRecoverByTime = 0.1f;
        DateTime recoverTime;

        // CD for abillities
        public double qCoolDownPassedTime;
        public double wCoolDownPassedTime;
        public double eCoolDownPassedTime;

        // Mana cost for abillities
        float qManaCost = 30f;
        float wManaCost = 50f;
        float eManaCost = 70f;


        // Const
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

        // This is the function that is placed in the game loop
        public override void Inputs()
        {
            Move();
            Cast();
            Recover();
        }

        // Move func
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

        // Cast Func
        public void Cast()
        {
            // Making the cooldowns go down to 0
            qCoolDownPassedTime = qCooldown - (DateTime.Now - qStart).TotalSeconds;
            wCoolDownPassedTime = wCooldown - (DateTime.Now - wStart).TotalSeconds;
            eCoolDownPassedTime = eCooldown - (DateTime.Now - eStart).TotalSeconds;

            // if q is pressed
            if (Raylib.IsKeyPressed(q) && qInProgress == false && qCoolDownPassedTime < 0)
            {
                qStart = DateTime.Now; // start time 
                qTime = DateTime.Now; // time which will reset every timeframe
                qLengthCounter = 0; // Counter for how many times it should move
                qMousePos = Raylib.GetMousePosition(); // Record mousepos when q was pressed
                qInProgress = true; 
                qPos = Position; // Start position
                
                mana -= qManaCost; // reduce mana
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

        // Recover func
        public void Recover()
        {
            double deltaTime = (DateTime.Now - recoverTime).TotalSeconds; // deltatime
            if (deltaTime >= 1f)
            {
                // if so HP doesnt go over max
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
            int qLength = 200; // length of q
            int qSpeed = 5; // speed of q
            float ratio = qLength / CalcTotalMoveAmount(qMousePos); // calcing ratio between mousepos and targetpos

            int forMoveAmount = qLength / qSpeed; // amount to move every frame.
            Vector2 targetLocal = new Vector2((qMousePos.X - Position.X) * ratio, (qMousePos.Y - Position.Y) * ratio); // Target position but in local coordinates

            Vector2 mSpeed = GetMoveSpeed(targetLocal, forMoveAmount); // amount to move x and y for every timeframe

            // if q is in progress
            if (qInProgress)
            {
                // timer for checking every timeframe
                double qTimer = (DateTime.Now - qTime).TotalSeconds;
                if (qTimer >= 0.008f && qLengthCounter <= forMoveAmount)
                {
                    // move position for one timeframe
                    qPos.X += mSpeed.X;
                    qPos.Y += mSpeed.Y;
                    
                    qTime = DateTime.Now; // recording new datetime
                    qLengthCounter++; // adding to counter
                }
                if (qLengthCounter > forMoveAmount)
                {
                    qInProgress = false; // stop when counter has reached formoveamount
                }

                // draw projectile while q is in progress
                Raylib.DrawCircle((int)qPos.X, (int)qPos.Y, 10, Color.RED);
            }
        }

        // W func
        public void wAbillity()
        {
            int wLength = 230;
            int wSpeed = 5;
            float ratio = wLength / CalcTotalMoveAmount(wMousePos); // calcing ratio between mousepos and targetpos

            int forMoveAmount = wLength / wSpeed; // amount to move every frame.
            Vector2 targetLocal = new Vector2((wMousePos.X - Position.X) * ratio, (wMousePos.Y - Position.Y) * ratio);

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

        // e func
        public void eAbillity()
        {
            int eLength = 170;
            float ratio = eLength / CalcTotalMoveAmount(eMousePos); // calcing ratio between mousepos and targetpos

            Vector2 localLimit = new Vector2((eMousePos.X - Position.X) * ratio, (eMousePos.Y - Position.Y) * ratio); // local limit pos when position is 0,0

            if (eInProgress)
            {
                // Limit position for global
                Vector2 LimitPos = new Vector2(Position.X + localLimit.X, Position.Y + localLimit.Y); 

                // when mousepos is smaller than limit
                if (CalcTotalMoveAmount(eMousePos) <= eLength)
                {
                    // move to position
                    Position = eMousePos;
                }
                else // move to limit
                {
                    Position = LimitPos;
                }

                // stop move to progress
                moveInProgress = false;

                eInProgress = false;
            }
        }

        // move function
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

        // gets how much to move per timeframe
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

        // Getting the total amount to move
        public float CalcTotalMoveAmount(Vector2 targetPos)
        {
            Vector2 moveAmount = CalcMoveAmountXY(targetPos); // making vector2 based on Position being 0,0
            float totalMoveAmount = (float)Math.Sqrt((Math.Pow(moveAmount.X, 2) + Math.Pow(moveAmount.Y, 2)));

            return totalMoveAmount;
        }


    }
}
