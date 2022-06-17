using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGameAlive = true;
    
    public static PlayerHealth playerHealth =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
}
