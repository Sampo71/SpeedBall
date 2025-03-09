using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{
    public int WarpToLevel;
    public SceneSwap SceneCall;

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        SceneCall.NextLevel();
    }
}
