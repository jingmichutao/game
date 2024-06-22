using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public float fadeDuration = 2f; // �������ʱ��
    public float delayBeforeFadeOut = 7f; // ������ɺ��ӳٶ�ÿ�ʼ����
    private Image image;
    private float elapsedTime = 0f;
    
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Fade());
    }

    // ����
    IEnumerator Fade()
    {
        // �ȴ�һ��ʱ��
        elapsedTime = 0f;
        yield return new WaitForSeconds(delayBeforeFadeOut);

        while (elapsedTime < fadeDuration)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ɣ����ٶ���
        elapsedTime = 0f;
    }
}
