using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            CoinUI.CurrentCoinQuantity += 1;
            Destroy(gameObject);
            SoundManage.PlayerPickCoin();
        }
    }
}