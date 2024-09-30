using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void Crash();
    public event Crash OnCrash;

    public GameManager gameManager;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Obstacle struck");

            OnCrash?.Invoke();
            gameManager.EndGame();
        }
    }
}
