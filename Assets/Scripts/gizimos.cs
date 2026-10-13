using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizimos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//设置Gizmos颜色为红色
        Gizmos.DrawWireSphere(transform.position, 0.22f);//绘制一个半径为0.22的红色空心圆，表示检测范围
    }
}
