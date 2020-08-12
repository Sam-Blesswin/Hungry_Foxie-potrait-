using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scorecard : MonoBehaviour
{
    private spawner spawn;
    private playerhealth playerstatus;

    private int score = 0;
    private bool playerisdead;
  
    public TextMeshProUGUI scoreboard;
    public TextMeshProUGUI finalscoretxt;

    private void Awake()
    {
        spawn = GameObject.FindObjectOfType<spawner>();
        playerstatus = GameObject.FindObjectOfType<playerhealth>();
    }

    private void Update()
    {
        scoreboard.text = score.ToString();
        finalscoretxt.text = score.ToString();

        playerisdead =playerstatus.getdeadstatus();
        checkstatus(playerisdead);

        spawn.updatelevel(score);
        if(score<0)
        {
            score = 0;
        }
    }

    private void checkstatus(bool playerisdead)
    {
        if(playerisdead)
        {
           
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
