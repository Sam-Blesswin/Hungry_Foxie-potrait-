using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class applecollect : MonoBehaviour
{
    private scorecard score;
    private void Start()
    {
        score = GameObject.FindObjectOfType<scorecard>();
    }

    private void Update()
    {
        if (transform.position.y < -2.5f)
        {
            Destroy(gameObject);
            score.scoredegrade();
        }
    }

    private void OnCollisionEnter(Collision target)
    {
       if(target.gameObject.tag=="Player")
        {
            Destroy(gameObject);
            score.scoreupdate();
        }
    }

    
}
