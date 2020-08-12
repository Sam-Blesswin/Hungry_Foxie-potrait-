using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinput : MonoBehaviour
{
    private bool tap, swipeleft, swiperight;
    private Vector2 starttouch, swipedelta;
    private bool isdragging = false;
    void Update()
    {
        tap = swipeleft = swiperight = false;
        #region mobileinputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isdragging = true;
                starttouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isdragging = false;
                reset();
            }
        }
        #endregion

        //calculate distance swipped
        swipedelta = Vector2.zero;
        if (isdragging)
        {
            if (Input.touches.Length > 0)
            {
                swipedelta = Input.touches[0].position - starttouch;
            }
        }

        //is swipe long enough
        if (swipedelta.magnitude >= 125)
        {
            //direction detection
            float x = swipedelta.x;
            float y = swipedelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                    swiperight = true;
                else
                    swipeleft = true;
            }
            reset();
        }
    }
    public void reset()
    {
        isdragging = false;
        swipedelta = starttouch = Vector2.zero;
    }

    public bool SwipeLeft { get { return swipeleft; } }
    public bool SwipeRight { get { return swiperight; } }
    public bool Tap { get { return tap; } }
    public Vector2 StartTouch { get { return starttouch; } }
    public Vector2 SwipeDelta { get { return swipedelta; } }
}
