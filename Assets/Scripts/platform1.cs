using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform1 : MonoBehaviour
{
    PlatformEffector2D pe2d;
    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pe2d.rotationalOffset = 90;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            pe2d.rotationalOffset = -90f;
        }
    }
}
