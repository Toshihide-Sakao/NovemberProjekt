using System;
using System.Numerics;

namespace NovemberProjekt.LoL2
{
    public class Detector
    {
        public static void Detect()
        {
            // i is the char being effected
            // j is the char doing the effedct
            for (int i = 0; i < Character.list.Count; i++)
            {
                for (int j = 0; j < Character.list.Count; j++)
                {
                    if (Character.list[j].CheckDerived() == "Champion")
                    {
                        Champion effecter = (Champion)Character.list[j];
                        for (int q = 0; q < effecter.abillityDamages.Length; q++)
                        {
                            if (CheckCollision(effecter, Character.list[i], q) && effecter.team != Character.list[i].team)
                            {
                                Character.list[i].HP -= effecter.abillityDamages[q];
                                effecter.inProgresses[q] = false;
                            }
                        }
                    }
                }
            }
        }

        static bool CheckCollision(Champion effecter, Character effected, int abillityIndex)
        {
            bool xVal = false;
            bool yVal = false;
            float offset = effected.size * 0.9f + effecter.ProjectileSize[abillityIndex] * 0.9f;
            if (effected.Position.X >= effecter.abillityPosses[abillityIndex].X - offset && effected.Position.X <= effecter.abillityPosses[abillityIndex].X + offset)
            {
                xVal = true;
            }
            if (effected.Position.Y >= effecter.abillityPosses[abillityIndex].Y - offset && effected.Position.Y <= effecter.abillityPosses[abillityIndex].Y + offset)
            {
                yVal = true;
            }

            if (xVal && yVal)
            {
                return true;
            }

            return false;
        }
    }
}
