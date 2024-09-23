using UnityEngine.SceneManagement;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Invoker _invoker;
    private bool _isReplaying;
    private bool _isRecording;
    private PlayerMovement _playerMovement;
    private Command _buttonA, _buttonD;


    private void Awake()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _playerMovement = GetComponent<PlayerMovement>();
    }


    void Start()
    {
        _buttonA = new TurnLeft(_playerMovement);
        _buttonD = new TurnRight(_playerMovement);
    }

    void FixedUpdate()
    {
        if (!_isReplaying && _isRecording)
        {
            if (Input.GetKey(KeyCode.A))
                _invoker.ExecuteCommand(_buttonA);
            else if (Input.GetKey(KeyCode.D))
                _invoker.ExecuteCommand(_buttonD);
        }
    }

    public void StartReplay()
    {
        _isReplaying = true;
        _isRecording = false;
        _invoker.Replay();
    }

    public void startRecording()
    {
        _isRecording = true;
        _isReplaying = false;
        _invoker.Record();
    }
}
