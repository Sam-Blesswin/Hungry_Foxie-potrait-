using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitmenu : MonoBehaviour
{
    public Canvas exit;
    public Canvas sureexit;
    
    private void Start()
    {
       exit.enabled = false;
       sureexit.enabled = false;
    }

    public void playagain()
    {
        SceneManager.LoadScene(1);
    }

    public void home()
    {
        SceneManager.LoadScene(0);
    }

   public void exitgame()
    {
        sureexit.enabled = true;
    }  
}
