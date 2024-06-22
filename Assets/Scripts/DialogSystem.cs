using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public TMP_Text dialogText; // ��Inspector����ק�Ի��ı�UI���
    public string[] dialogLines; // �Ի���������
    public GameObject dialogPanel; // �Ի���UI��GameObject
    public Button closeButton; // �رհ�ť����
    public Button nextButton; // ��һ����ť����
    public GameObject nextCanvas; // ��һ��������GameObject����
    public GameObject background; // ����ͼƬ��GameObject����
    public AudioSource ButtonAudio; // ��ť��Ч

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
        // ȷ���Ի����ɼ�
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
            // �Ի��������رնԻ���
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
        float duration = 1.0f; // ����Ч��������ʱ�䣬���Ը�����Ҫ����

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
            //background.SetActive(false); // ���ر���ͼƬ
        }
        currentLineIndex = 0; // ���öԻ�����

        // ������һ������
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
