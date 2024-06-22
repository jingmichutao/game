using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TransitionLevel3 : MonoBehaviour
{
    public Image backgroundImage;
    public TMP_Text loadingText;
    public Slider progressBar;
    public Image overlayImage;  // 用于遮挡背景
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        // 确保遮挡背景开始时不透明
        SetOverlayAlpha(1f);

        // 淡入
        yield return StartCoroutine(FadeIn());

        // 开始加载下一个场景
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level3");
        operation.allowSceneActivation = false;

        float elapsedTime = 0f; // 记录总时间
        const float StartDelay = 1f; // 初始等待时间
        const float RampUpTime = 2f; // 进度加速时间段
        const float TotalDuration = StartDelay + RampUpTime; // 总过渡时间

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // 在开始阶段，进度条保持不动
            if (elapsedTime <= StartDelay)
            {
                progressBar.value = 0f; // 保持在起始位置
            }
            else
            {
                // 随着时间增加，非线性增加进度，模拟加载加速
                float progressRatio = Mathf.Clamp01((elapsedTime - StartDelay) / RampUpTime);
                // 可选：加入轻微随机波动，使加载显得更自然
                float fluctuation = Random.Range(-0.01f, 0.01f);
                float adjustedProgress = Mathf.SmoothStep(0f, 1f, progressRatio) + fluctuation;

                // 确保进度不超过1，并应用到进度条
                progressBar.value = Mathf.Clamp01(operation.progress * adjustedProgress);

                // 当接近完成且足够时间过去，直接完成加载并淡出
                if (operation.progress >= 0.9f && elapsedTime >= TotalDuration - 0.5f)
                {
                    progressBar.value = 1f;
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(FadeOut());
                    operation.allowSceneActivation = true;
                    break; // 确保跳出循环
                }
            }

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color backgroundColor = backgroundImage.color;
        Color textColor = loadingText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            backgroundColor.a = alpha;
            textColor.a = alpha;
            backgroundImage.color = backgroundColor;
            loadingText.color = textColor;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color backgroundColor = backgroundImage.color;
        Color textColor = loadingText.color;
        Color overlayColor = overlayImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            backgroundColor.a = alpha;
            textColor.a = alpha;
            overlayColor.a = 1f; // 确保遮挡背景保持不透明
            backgroundImage.color = backgroundColor;
            loadingText.color = textColor;
            overlayImage.color = overlayColor;
            yield return null;
        }

        // 确保遮挡背景在淡出结束后也保持不透明，直到新场景完全加载
        SetOverlayAlpha(1f);
    }

    private void SetOverlayAlpha(float alpha)
    {
        Color overlayColor = overlayImage.color;
        overlayColor.a = alpha;
        overlayImage.color = overlayColor;
    }
}
