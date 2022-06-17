using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{

    public static AudioSource audioSource;
    
    public static AudioClip pickCoin;

    public static AudioClip throwCoin;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
        throwCoin = Resources.Load<AudioClip>("ThrowCoin");
    }

    public static void PlayerPickCoin()
    {
        audioSource.PlayOneShot(pickCoin);
    }
    
    public static void PlayerThrowCoin()
    {
        audioSource.PlayOneShot(throwCoin);
    }
}
