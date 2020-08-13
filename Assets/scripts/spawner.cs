using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;

public class spawner : MonoBehaviour
{
    public Transform[] spawnpoints;
    public GameObject[] collectables;
    float a=1f;
    float b=3f;
    int l = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startspawn());
    }
    
    public void updatelevel(int score)
    {
        if(score<10)
        {
            a = 1f;
            b = 3f;
        }
        else if (score<30)
        {
            a = 0.5f;
            b = 2f;
        }
        else if(score<65)
        {
            a = 0.5f;
            b = 1.5f;
        }
        else
        {
            a = 0.1f;
            b = 1f;
            l = 4;
        }
    }

   IEnumerator startspawn()
    {
        yield return new WaitForSeconds(Random.Range(a,b));
        Instantiate(collectables[Random.Range(0,l)], spawnpoints[Random.Range(0, spawnpoints.Length)].position,Quaternion.identity);
        StartCoroutine(startspawn());
    }
    
}
