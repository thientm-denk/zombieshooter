using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static int Difficult { get; set; }

    public static int zombieMaxHealth {
        get
        {
            switch (Difficult)
            {
                case 1:
                    {
                        return 30;
                    }
                case 2:
                    {
                        return 40;
                    }
                case 3:
                    {
                        return 60;
                    }
                default:
                    {
                        return 30;
                    }
            }
        }

    }

    public static float zombieMoveSpeed
    {
        get
        {
            switch (Difficult)
            {
                case 1:
                    {
                        return 3.5f;
                    }
                case 2:
                    {
                        return 4;
                    }
                case 3:
                    {
                        return 4.5f;
                    }
                default:
                    {
                        return 3.5f;
                    }
            }
        }

    }

    public static int scoreForEachZombie
    {
        get
        {
            switch (Difficult)
            {
                case 1:
                    {
                        return 1;
                    }
                case 2:
                    {
                        return 2;
                    }
                case 3:
                    {
                        return 3;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }

    }



}
