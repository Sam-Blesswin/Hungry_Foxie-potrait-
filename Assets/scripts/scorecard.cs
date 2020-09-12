using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scorecard : MonoBehaviour
{
    private spawner spawn;
    private PlayFabStats PFS;

    private int score = 0;

  
    public TextMeshProUGUI scoreboard;
    public TextMeshProUGUI finalscoretxt;

    private void Awake()
    {
        spawn = GameObject.FindObjectOfType<spawner>();
        PFS = GameObject.FindObjectOfType<PlayFabStats>();
    }

    private void Update()
    {
        scoreboard.text = score.ToString();
        finalscoretxt.text = score.ToString();
        spawn.updatelevel(score);
        PFS.SetScore(score);
        if(score<0)
        {
            score = 0;
        }
    }

    public void scoreupdate()
    {
        score += 1;
    }

    public void scoredegrade()
    {
        if (score > 0)
        {
            score -= 2;
        }
    }
}
