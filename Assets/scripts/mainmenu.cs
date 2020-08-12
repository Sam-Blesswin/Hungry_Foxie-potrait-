using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public Canvas sureexit;
    private void Start()
    {
        sureexit.enabled = false;
    }
    public void loadgame()
    {
        SceneManager.LoadScene(1);
    }

    public void exit()
    {
        sureexit.enabled = true;
    }
}
