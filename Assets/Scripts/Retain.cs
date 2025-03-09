using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retain : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
