using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPointGizimos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;//设置Gizmos颜色为绿色
        Gizmos.DrawWireCube(transform.position, new Vector2(0.3f, 0.6f));//绘制一个宽0.3，高0.6的绿色空心矩形，表示检测范围
    }
}
