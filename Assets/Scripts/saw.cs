using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2f; // 锯齿移动速度
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime); // 绕Z轴旋转
    }
}
