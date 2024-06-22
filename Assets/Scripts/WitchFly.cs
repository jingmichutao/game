using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchFly : MonoBehaviour
{
    public float speed = 5f; // �����ٶ�
    public Vector2 startPos, endPos; // ��ʼ�����λ��

    private void Update()
    {
        // ���㵱ǰλ�ã�ʹ���ع̶������ƶ�
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, endPos, step);

        // ���ﵽ�򳬹��յ�ʱ�����ض���
        if (new Vector2(transform.position.x, transform.position.y) == endPos ||
    transform.position.x > endPos.x)
        {
            HandleEndOfFlight(); // ������н���
        }
    }

    private void Start()
    {
        // ��ʼ��λ��
        transform.position = startPos;
    }

    private void HandleEndOfFlight()
    {
        // ���ض���
        this.gameObject.SetActive(false);
    }
}
