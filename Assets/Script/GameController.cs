using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGameAlive = true;
    
    public static PlayerHealth playerHealth =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    
    
    public static class Anim
    {
        public static float Idle = 0f;
        public static float Run = 0.111f;
        public static float Jump = 0.222f;
        public static float Fall = 0.333f;
        public static float DoubleJump = 0.4144f;
        public static float DoubleFall = 0.555f;
        public static float Attack = 0.666f;
        public static float Climb = 0.777f;
        public static float Death = 0.8888f;
        public static float Hit = 1f;
    }
}
