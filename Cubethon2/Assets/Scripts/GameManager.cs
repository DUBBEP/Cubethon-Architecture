using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isReplaying;

    public PlayerMovement movement;
    public InputHandler inputHandler;
    public GameObject completeLevelUI;
    public GameObject ReplayText;
    bool gameEnded = false;

    public float restartDelay = 2f;


    private void Start()
    {
        if (CommandLog.recordedCommands.Count > 0)
        {
            isReplaying = true;
            ReplayText.SetActive(true);
            inputHandler.StartReplay();
        }
        else if(CommandLog.recordedCommands.Count <= 0)
        {
            isReplaying = false;
            inputHandler.startRecording();
        }

    }

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

        if (isReplaying)
            CommandLog.recordedCommands.Clear();

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
