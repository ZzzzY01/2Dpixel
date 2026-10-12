using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLATFORM : MonoBehaviour
{
    PlatformEffector2D pe2d;
    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pe2d.rotationalOffset = 180f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pe2d.rotationalOffset = 0f;
        }
    }
}
