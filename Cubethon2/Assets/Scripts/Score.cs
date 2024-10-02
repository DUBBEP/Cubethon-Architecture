using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public delegate void scoreMilestone();
    public event scoreMilestone OnScoreMilestone;

    public Transform Player;
    public TextMeshProUGUI scoreText;

    public static Score instance;

    bool milestoneReached = false;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Player.position.z > 800 && milestoneReached == false)
        {
            OnScoreMilestone?.Invoke();
            milestoneReached = true;
        }

        scoreText.text = Player.position.z.ToString("0");
    }
}
