using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    public Canvas sureexit;
    public void yes()
    {
        Application.Quit();
    }

    public void no()
    {
        sureexit.enabled = false;
    }
}
