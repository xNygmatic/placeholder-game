using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement.hitCheckpoint = true;
            FindObjectOfType<LevelManager>().RestartCurrentLevel();
        }
    }
}
