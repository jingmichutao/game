using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public TMP_Text dialogText; // 在Inspector中拖拽对话文本UI组件
    public string[] dialogLines; // 对话内容数组
    public GameObject dialogPanel; // 对话框UI的GameObject
    public Button closeButton; // 关闭按钮引用
    public Button nextButton; // 下一条按钮引用
    public GameObject nextCanvas; // 下一个画布的GameObject引用
    public GameObject background; // 背景图片的GameObject引用
    public AudioSource ButtonAudio; // 按钮音效

    private int currentLineIndex = 0;

    void Start()
    {
        StartDialog();
        SetupButtonListeners();
    }

    void SetupButtonListeners()
    {
        closeButton.onClick.AddListener(() => {
            PlaySound(ButtonAudio);
            EndDialog();
        });
        nextButton.onClick.AddListener(() => {
            PlaySound(ButtonAudio);
            DisplayNextLine();
        });
    }

    void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void StartDialog()
    {
        // 确保对话面板可见
        dialogPanel.SetActive(true);
        StartCoroutine(FadeInDialog());
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // 对话结束，关闭对话框
            EndDialog();
        }
    }

    IEnumerator FadeInDialog()
    {
        CanvasGroup canvasGroup = dialogPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = dialogPanel.AddComponent<CanvasGroup>();
        }

        float startTime = Time.time;
        float duration = 1.0f; // 淡入效果持续的时间，可以根据需要调整

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            canvasGroup.alpha = t;
            yield return null;
        }

        canvasGroup.alpha = 1;
        Debug.Log("FadeInDialog ended");
    }

    void EndDialog()
    {
        dialogPanel.SetActive(false);
        if (background != null)
        {
            //background.SetActive(false); // 隐藏背景图片
        }
        currentLineIndex = 0; // 重置对话索引

        // 激活下一个画布
        if (nextCanvas != null)
        {
            Debug.Log("Activating nextCanvas");
            nextCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("nextCanvas is not assigned in the inspector.");
        }
    }
}
