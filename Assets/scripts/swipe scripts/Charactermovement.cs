using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour
{
    public Playerinput swipecontrols;
    public Transform player;
    public float speed = 3f;
    public float xrange = 3f;
    private Vector3 desireddir;


    private void Start()
    {
        desireddir = player.transform.position;
    }
    private void Update()
    {
        if (swipecontrols.SwipeLeft || Input.GetKey(KeyCode.LeftArrow))
        {
            desireddir += Vector3.left * 1.5f;
        }
        else if (swipecontrols.SwipeRight || Input.GetKey(KeyCode.RightArrow))
        {
            desireddir += Vector3.right *  1.5f;
        }
        player.transform.position = Vector3.MoveTowards(player.transform.position, desireddir, speed * Time.deltaTime);
        desireddir.x = Mathf.Clamp(desireddir.x, -1 * xrange, xrange);
    }
}
