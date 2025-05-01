using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    private Vector2 Point1;
    public Vector2 Point2;
    public float MovementTime;
    float CurrentMovementTime;
    public float RestTime;
    bool XToY = true;
    bool Resting = false;
    private Rigidbody2D rb2d;

    void Start()
    {
        Point1 = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine("Transition");
        CurrentMovementTime = MovementTime;
    }

    void FixedUpdate()
    {
        if(!Resting)
        {
            StartCoroutine("Transition");
        }
    }

    IEnumerator Transition()
    {
        if(XToY)
        {
            if(CurrentMovementTime >= 0)
            {
                CurrentMovementTime -= Time.deltaTime;
                transform.position = Vector2.Lerp(Point1, Point1 + Point2, CurrentMovementTime / MovementTime);
            }else{
                XToY = false;
                CurrentMovementTime = MovementTime;
                Resting = true;
                yield return new WaitForSeconds(RestTime);
                Resting = false;
            }
        }else
        {
            if(CurrentMovementTime >= 0)
            {
                CurrentMovementTime -= Time.deltaTime;
                transform.position = Vector2.Lerp(Point1 + Point2, Point1, CurrentMovementTime / MovementTime);
            }else{
                XToY = true;
                CurrentMovementTime = MovementTime;
                Resting = true;
                yield return new WaitForSeconds(RestTime);
                Resting = false;
            }
        }
        yield break;
    }
}
