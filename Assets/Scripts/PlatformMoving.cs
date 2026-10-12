using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] points; // 路径点数组
    [SerializeField] private float speed = 2f; // 移动速度

    private int currentPointIndex = 1; // 当前目标点的索引
    void Start()
    {

    }

    void FixedUpdate()
    {
        MovePlatform();
    }
    private void MovePlatform()
    {
        // 移动平台到当前目标点
        transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].transform.position, speed * Time.deltaTime);
        // 检查是否到达目标点，如果到达则切换到下一个目标点
        if (Vector2.Distance(transform.position, points[currentPointIndex].transform.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex == 1) ? 0 : 1; // 循环移动到下一个点
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家设置为平台的子对象，使其随平台移动
            collision.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 取消玩家的父对象设置，使其不再随平台移动
            collision.transform.SetParent(null);
        }
    }
}
