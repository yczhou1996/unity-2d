using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinItem : MonoBehaviour
{

    private bool isPlayerInTrainBin;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (isPlayerInTrainBin)
            {
                if (CoinUI.CurrentCoinQuantity > 0)
                {
                    TrashBinCoin.coinCurrent++;
                    CoinUI.CurrentCoinQuantity--;
                    SoundManage.PlayerThrowCoin();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player trigger");
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrainBin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrainBin = false;
        }
    }
}
