using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class Champion : Character
    {
        // Mouse position variable
        Vector2 moveMousePos = new Vector2();

        // Counters for projectile skills
        int[] ProjectileCounter = new int[] { 1000000, 0, 1000000 };
        // int qLengthCounter = 1000000;
        // int wLengthCounter = 1000000;

        // Bool for if action is in progress
        bool moveInProgress = false;

        // Position for abillity
        public Vector2[] abillityPosses = new Vector2[3];
        // public Vector2 qPos;
        // public Vector2 wPos;
        // public Vector2 ePos;

        // Cooldowns for abillities
        float[] acutallCooldowns = new float[] { 3.3f, 5.0f, 9.0f };
        // float qCooldown = 3.3f;
        // float wCooldown = 5.0f;
        // float eCooldown = 9.0f;

        // Time that resets every timeframe for each usage
        DateTime moveStart;

        float[] abillityTimers = new float[3];
        // float qTimer;
        // DateTime wTime;

        // Time for when the abillity was casted (NOT NEEDED ANYMORE)
        // DateTime qStart;
        // DateTime wStart;
        // DateTime eStart;

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
        public float[] currentCooldowns = new float[3];
        // public float qCoolDownPassedTime;
        // public double wCoolDownPassedTime;
        // public double eCoolDownPassedTime;

        // Mana cost for abillities
        float[] manaCosts = new float[] { 30f, 50f, 70f };
        // float qManaCost = 30f;
        // float wManaCost = 50f;
        // float eManaCost = 70f;

        // Progress
        bool[] inProgresses = new bool[3];
        // bool qInProgress = false;
        // bool wInProgress = false;
        // bool eInProgress = false;

        // mouse position variables
        Vector2[] mousePosses = new Vector2[3];
        // Vector2 qMousePos = new Vector2();
        // Vector2 wMousePos = new Vector2();
        // Vector2 eMousePos = new Vector2();
        // Vector2 rMousePOs = new Vector2();

        // Key inputs
        KeyboardKey[] abillityKeys = new KeyboardKey[] {KeyboardKey.KEY_Q, KeyboardKey.KEY_W, KeyboardKey.KEY_E, KeyboardKey.KEY_R, KeyboardKey.KEY_D, KeyboardKey.KEY_F};
        // KeyboardKey q = KeyboardKey.KEY_Q;
        // KeyboardKey w = KeyboardKey.KEY_W;
        // KeyboardKey e = KeyboardKey.KEY_E;
        // KeyboardKey r = KeyboardKey.KEY_R;
        // KeyboardKey d = KeyboardKey.KEY_D;
        // KeyboardKey f = KeyboardKey.KEY_F;


        // Const
        public Champion()
        {
            spawnPos = new Vector2(100, 400);
            Position = spawnPos;

            moveSpeed = 2;

            moveStart = DateTime.Now;
            // qStart = DateTime.Now;
            // wStart = DateTime.Now;
            // eStart = DateTime.Now;
            recoverTime = DateTime.Now;

            HP = 1000f;

            maxHP = HP;
            maxMana = mana;

            for (int i = 0; i < acutallCooldowns.Length; i++)
            {
                currentCooldowns[i] = acutallCooldowns[i];
            }
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
            for (int i = 0; i < acutallCooldowns.Length; i++)
            {
                currentCooldowns[i] -= Raylib.GetFrameTime();
            }

            for (int i = 0; i < 3; i++)
            {
                if (Raylib.IsKeyPressed(abillityKeys[i]) && inProgresses[i] == false && currentCooldowns[i] < 0)
                {
                    abillityTimers[i] = 0; // time which will reset every timeframe
                    ProjectileCounter[i] = 0; // Counter for how many times it should move
                    mousePosses[i] = Raylib.GetMousePosition(); // Record mousepos when q was pressed
                    inProgresses[i] = true;
                    abillityPosses[i] = Position; // Start position

                    mana -= manaCosts[i]; // reduce mana
                }
            }
            // if q is pressed

            // else if (Raylib.IsKeyPressed(w) && wInProgress == false && wCoolDownPassedTime < 0)
            // {
            //     wTime = DateTime.Now;
            //     wLengthCounter = 0;
            //     wMousePos = Raylib.GetMousePosition();
            //     wInProgress = true;
            //     wPos = Position;

            //     mana -= wManaCost;
            // }
            // else if (Raylib.IsKeyPressed(e) && eCoolDownPassedTime < 0)
            // {
            //     eStart = DateTime.Now;
            //     eMousePos = Raylib.GetMousePosition();
            //     eInProgress = true;

            //     mana -= eManaCost;
            // }
            // else if (Raylib.IsKeyPressed(r))
            // {

            // }
            
            
            wAbillity(mousePosses[1]);
            qAbillity(mousePosses[0]);
            eAbillity(mousePosses[2]);
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

        public void qAbillity(Vector2 qMousePos)
        {
            int qLength = 200; // length of q
            int qSpeed = 5; // speed of q

            ProjectileAbillity(qLength, qSpeed, 0, Color.RED);
        }

        // W func
        public void wAbillity(Vector2 wMousePos)
        {
            int wLength = 230;
            int wSpeed = 5;

            ProjectileAbillity(wLength, wSpeed, 1, Color.GRAY);
        }

        // // e func
        public void eAbillity(Vector2 eMousePos)
        {
            int eLength = 170;
            
            BlinkAbillity(eLength, 2);
        }

        void ProjectileAbillity(int length, int speed, int abillity, Color color)
        {
            float ratio = length / CalcTotalMoveAmount(mousePosses[abillity]); // calcing ratio between mousepos and targetpos

            int forMoveAmount = length / speed; // amount to move every frame.
            Vector2 targetLocal = new Vector2((mousePosses[abillity].X - Position.X) * ratio, (mousePosses[abillity].Y - Position.Y) * ratio); // Target position but in local coordinates

            Vector2 mSpeed = GetMoveSpeed(targetLocal, forMoveAmount); // amount to move x and y for every timeframe

            // if q is in progress
            if (inProgresses[abillity])
            {
                System.Console.WriteLine(abillityTimers[abillity]);
                // timer for checking every timeframe
                abillityTimers[abillity] += Raylib.GetFrameTime();
                if (abillityTimers[abillity] >= 0.01f && ProjectileCounter[abillity] <= forMoveAmount)
                {
                    // move position for one timeframe
                    abillityPosses[abillity].X += mSpeed.X;
                    abillityPosses[abillity].Y += mSpeed.Y;

                    abillityTimers[abillity] = 0; // recording new datetime
                    ProjectileCounter[abillity]++; // adding to counter
                }
                // draw projectile while q is in progress
                Raylib.DrawCircle((int)abillityPosses[abillity].X, (int)abillityPosses[abillity].Y, 10, color);

                if (ProjectileCounter[abillity] > forMoveAmount)
                {
                    inProgresses[abillity] = false; // stop when counter has reached formoveamount
                    currentCooldowns[abillity] = acutallCooldowns[abillity];
                }
            }
        }

        void BlinkAbillity(int length, int abillity)
        {
            float ratio = length / CalcTotalMoveAmount(mousePosses[abillity]); // calcing ratio between mousepos and targetpos

            Vector2 localLimit = new Vector2((mousePosses[abillity].X - Position.X) * ratio, (mousePosses[abillity].Y - Position.Y) * ratio); // local limit pos when position is 0,0

            if (inProgresses[abillity])
            {
                // Limit position for global
                Vector2 LimitPos = new Vector2(Position.X + localLimit.X, Position.Y + localLimit.Y);

                // when mousepos is smaller than limit
                if (CalcTotalMoveAmount(mousePosses[abillity]) <= length)
                {
                    // move to position
                    Position = mousePosses[abillity];
                }
                else // move to limit
                {
                    Position = LimitPos;
                }

                // stop move to progress
                moveInProgress = false;

                inProgresses[abillity] = false;
                currentCooldowns[abillity] = acutallCooldowns[abillity];
            }
        }

        // void ResetCoolDown(int abillity)
        // {

        // }

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
