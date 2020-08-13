using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour
{
    public int health = 3;

    public int numofhearts=3;
    public Image[] hearts;
    public GameObject explosion;

    private Charactermovement controller;

    private exitmenu end;
    public Canvas ongame;
    private bool isdead;

    private void Start()
    {
        isdead = false;
        Time.timeScale = 1f;
        controller = GameObject.FindObjectOfType<Charactermovement>();
        end = GameObject.FindObjectOfType<exitmenu>();
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i<numofhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        dead();
    }
    private void dead()
    {
        if(health<0)
        {
            isdead = true;
            Destroy(gameObject,2f);
            Time.timeScale = 0f;
            controller.enabled = false;   //for swipe input
            ongame.enabled = false;
            end.exit.enabled = true;
        }
    }

    public bool getdeadstatus()
    {
        return isdead;
    }

    private void OnCollisionEnter(Collision enemy)
    {
        if(enemy.gameObject.tag=="enemy")
        {
            Destroy(enemy.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            health -= 1;
            numofhearts -= 1;
        }
    }
}
