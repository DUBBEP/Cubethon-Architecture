using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isReplaying;

    public Camera cam;
    public FollowPlayer followPlayer;
    public PlayerMovement movement;
    public InputHandler inputHandler;
    public GameObject completeLevelUI;
    public GameObject ReplayText;
    public Collectable collectable;
    public PlayerCollision collision;
    public Score score;
    bool gameEnded = false;
    bool rotateCam = false;
    public float restartDelay = 2f;

    private void OnEnable()
    {
        collectable.OnPickup += ReverseGravity;
        collision.OnCrash += EndGame;
        score.OnScoreMilestone += SetHighGravity;
    }

    private void OnDisable()
    {
        collectable.OnPickup -= ReverseGravity;
        collision.OnCrash -= EndGame;
        score.OnScoreMilestone -= SetHighGravity;
    }

    private void Start()
    {
        Physics.gravity = Vector3.down * 0.2f;
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

    void ReverseGravity()
    {
        Physics.gravity = new Vector3(0, 50, 0);

        Invoke("RotatePerspective", 0.75f);
    }

    void RotatePerspective()
    {
        Physics.gravity = new Vector3(0, 0.5f, 0);

        rotateCam = true;
    }

    void SetHighGravity()
    {
        Physics.gravity = new Vector3(0, -200, 0);
    }

    private void FixedUpdate()
    {
        if (rotateCam)
        {
            cam.gameObject.transform.rotation = Quaternion.Lerp(cam.transform.rotation,
                Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y, 180), Time.deltaTime * 2);

            followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                new Vector3(followPlayer.offset.x, -3, followPlayer.offset.z), Time.deltaTime * 2);
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
        Physics.gravity = Vector3.down;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
