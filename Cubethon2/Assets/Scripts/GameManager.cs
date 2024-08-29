using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement movement;
    public GameObject completeLevelUI;
    bool gameEnded = false;

    public float restartDelay = 2f;


    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        gameEnded = true;
    }
    public void EndGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        movement.enabled = false;
        Debug.Log("Game Ended");
        Invoke("Restart", restartDelay);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
