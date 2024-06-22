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
    public Image overlayImage;  // �����ڵ�����
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        // ȷ���ڵ�������ʼʱ��͸��
        SetOverlayAlpha(1f);

        // ����
        yield return StartCoroutine(FadeIn());

        // ��ʼ������һ������
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level3");
        operation.allowSceneActivation = false;

        float elapsedTime = 0f; // ��¼��ʱ��
        const float StartDelay = 1f; // ��ʼ�ȴ�ʱ��
        const float RampUpTime = 2f; // ���ȼ���ʱ���
        const float TotalDuration = StartDelay + RampUpTime; // �ܹ���ʱ��

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // �ڿ�ʼ�׶Σ����������ֲ���
            if (elapsedTime <= StartDelay)
            {
                progressBar.value = 0f; // ��������ʼλ��
            }
            else
            {
                // ����ʱ�����ӣ����������ӽ��ȣ�ģ����ؼ���
                float progressRatio = Mathf.Clamp01((elapsedTime - StartDelay) / RampUpTime);
                // ��ѡ��������΢���������ʹ�����Եø���Ȼ
                float fluctuation = Random.Range(-0.01f, 0.01f);
                float adjustedProgress = Mathf.SmoothStep(0f, 1f, progressRatio) + fluctuation;

                // ȷ�����Ȳ�����1����Ӧ�õ�������
                progressBar.value = Mathf.Clamp01(operation.progress * adjustedProgress);

                // ���ӽ�������㹻ʱ���ȥ��ֱ����ɼ��ز�����
                if (operation.progress >= 0.9f && elapsedTime >= TotalDuration - 0.5f)
                {
                    progressBar.value = 1f;
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(FadeOut());
                    operation.allowSceneActivation = true;
                    break; // ȷ������ѭ��
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
            overlayColor.a = 1f; // ȷ���ڵ��������ֲ�͸��
            backgroundImage.color = backgroundColor;
            loadingText.color = textColor;
            overlayImage.color = overlayColor;
            yield return null;
        }

        // ȷ���ڵ������ڵ���������Ҳ���ֲ�͸����ֱ���³�����ȫ����
        SetOverlayAlpha(1f);
    }

    private void SetOverlayAlpha(float alpha)
    {
        Color overlayColor = overlayImage.color;
        overlayColor.a = alpha;
        overlayImage.color = overlayColor;
    }
}
