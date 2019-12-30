using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    public int coinWorth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.coinsAmount = Player.coinsAmount + coinWorth;
            FindObjectOfType<AudioManager>().Play("CoinPickup");
            Destroy(gameObject);
        }
    }
}
