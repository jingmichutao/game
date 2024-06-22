using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchFly : MonoBehaviour
{
    public float speed = 5f; // 飞行速度
    public Vector2 startPos, endPos; // 起始与结束位置

    private void Update()
    {
        // 计算当前位置，使其沿固定方向移动
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, endPos, step);

        // 当达到或超过终点时，隐藏对象
        if (new Vector2(transform.position.x, transform.position.y) == endPos ||
    transform.position.x > endPos.x)
        {
            HandleEndOfFlight(); // 处理飞行结束
        }
    }

    private void Start()
    {
        // 初始化位置
        transform.position = startPos;
    }

    private void HandleEndOfFlight()
    {
        // 隐藏对象
        this.gameObject.SetActive(false);
    }
}
