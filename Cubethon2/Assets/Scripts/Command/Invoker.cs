using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Invoker : MonoBehaviour
{
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;

    public void ExecuteCommand(Command command)
    {
        command.Execute();

        if (_isRecording)
            CommandLog.recordedCommands.Add(_recordingTime, command);

        Debug.Log("Recorded Time: " + _recordingTime);
        Debug.Log("Recorded Command: " + command);

    }

    public void Record()
    {
        _recordingTime = 0.0f;
        _isRecording = true;
    }

    public void Replay()
    {
        _replayTime = 0.0f;
        _isReplaying = true;

        if (CommandLog.recordedCommands.Count <= 0)
            Debug.LogError("No commands to replay!");

        CommandLog.recordedCommands.Reverse();
    }

    private void FixedUpdate()
    {
        if (_isRecording)
            _recordingTime += Time.deltaTime;

        if (_isReplaying)
        {
            _replayTime += Time.fixedDeltaTime;

            if (CommandLog.recordedCommands.Any())
            {
                if (Mathf.Approximately(
                    _replayTime, CommandLog.recordedCommands.Keys[0]))
                {
                    Debug.Log("Replay Time: " + _replayTime);
                    Debug.Log("Replay Command: " + CommandLog.recordedCommands.Values[0]);

                    CommandLog.recordedCommands.Values[0].controller = GetComponent<PlayerMovement>();
                    CommandLog.recordedCommands.Values[0].Execute();
                    CommandLog.recordedCommands.RemoveAt(0);
                }
            }
            else
            {
                _isReplaying = false;
            }
        }
    }
}
