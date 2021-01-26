using System;
using Raylib_cs;

namespace NovemberProjekt.LoL2
{
    public class UserInterface
    {
        int x;
        int y;
        
        Color backColor = new Color(56, 51, 36, 255);
        Color boxColor = new Color(191, 225, 227, 255);
        Color boxReadyColor = new Color(245, 136, 86, 255);
        Champion champ;

        public UserInterface(int xPos, int yPos, Champion champion)
        {
            x = xPos;
            y = yPos;
            champ = champion;

            // Raylib.DrawRectangle(x, y, 300, 100, backColor);
        }

        public void update()
        {
            Raylib.DrawRectangle(x, y, 400, 150, backColor);
            int xBox = x + 20;
            int yBox = y + 20;

            double[] abillityCDs = new double[] {champ.qCoolDownPassedTime, champ.wCoolDownPassedTime, champ.eCoolDownPassedTime};
            for (int i = 0; i < 3; i++)
            {
                if (abillityCDs[i] <= 0)
                {
                    Raylib.DrawRectangle(xBox + 70 * i, yBox, 60, 60, boxReadyColor);
                }
                else
                {
                    Raylib.DrawRectangle(xBox + 70 * i, yBox, 60, 60, boxColor);
                }
                if (abillityCDs[i] >= 1)
                {
                    Raylib.DrawText(((int)(Math.Max(0, abillityCDs[i]))).ToString(), (xBox + 10) + 70 * i, yBox + 20, 30, Color.BLACK);
                }
                else if (abillityCDs[i] > 0)
                {
                    float cdOneDecimal =  (int)(10 * (Math.Max(0, abillityCDs[i]))) / 10.0f;
                    Raylib.DrawText(cdOneDecimal.ToString(), (xBox + 10) + 70 * i, yBox + 20, 30, Color.BLACK);
                }
                
            }
            float hp = (int)(champ.HP * 100) / 100f;
            float mana = (int)(champ.mana * 100) / 100f;
            Raylib.DrawText(hp.ToString(), xBox + 170, yBox + 75, 18, Color.WHITE);
            Raylib.DrawText(mana.ToString(), xBox + 170, yBox + 100, 18, Color.WHITE);
        }
    }
}
