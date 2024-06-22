using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public float fadeDuration = 2f; // 淡入持续时间
    public float delayBeforeFadeOut = 7f; // 淡入完成后延迟多久开始淡出
    private Image image;
    private float elapsedTime = 0f;
    
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Fade());
    }

    // 淡入
    IEnumerator Fade()
    {
        // 等待一段时间
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

        // 淡出完成，销毁对象
        elapsedTime = 0f;
    }
}
