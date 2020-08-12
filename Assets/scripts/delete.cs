using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{

    private void Update()
    {
        if(transform.position.y<-2.5f)
        {
            Destroy(gameObject);
        }
    }
}
