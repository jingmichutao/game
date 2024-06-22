using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Image questionImage; // ��ʾ����ͼƬ��UI���
    public TMP_Text questionText; // ��ʾ�����UI�ı�
    public TMP_Text hintText; // ��ʾ��ʾ��Ϣ��UI�ı�
    public Button[] optionButtons; // ѡ�ť����
    public GameObject QAPanel; // �Ի������
    public GameObject hintPanel; // ��ʾ�����
    public Button closeHintButton; // �رհ�ť����
    public Button nextQuestionButton; // ��һ����ť����

    public GameObject endDialogPanel; // �����Ի����
    public TMP_Text endDialogText; // �����Ի����ı�
    public Image badgeImage; // ����ͼ��
    public Button closeButton; // �رհ�ť

    public DialogQuestion[] dialogQuestions; // �Ի���������

    private int currentQuestionIndex = 0; // ��ǰ��������

    //public AudioSource rightAudio; // �����Ч
    //public AudioSource wrongAudio; // �����Ч
    public AudioSource buttonAudio; // ��ť��Ч

    void Start()
    {
        SetupQuestion(); // ���õ�һ������
        SetupButtonListeners();

        endDialogPanel.SetActive(false); // ȷ�������Ի��������
    }

    // ���ð�ť������
    void SetupButtonListeners()
    {
        closeHintButton.onClick.AddListener(() => {
            PlaySound(buttonAudio);
            OnHintPanelClose();
        }); // �ر���ʾ��ť����¼�
        //nextButton.onClick.AddListener(() => ShowNextQuestion()); // ��һ�����ⰴť����¼�
    }

    void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // ���õ�ǰ����
    void SetupQuestion()
    {
        hintPanel.SetActive(false); // ȷ����ʾ������
        QAPanel.SetActive(true); // ȷ���Ի���ɼ�

        if (currentQuestionIndex < dialogQuestions.Length)
        {
            DialogQuestion question = dialogQuestions[currentQuestionIndex];
            questionText.text = question.question; // ���������ı�
            questionImage.sprite = question.image; // ��������ͼƬ

            // ��������ͼƬ�Ĵ�С��λ��
            RectTransform imageRectTransform = questionImage.GetComponent<RectTransform>();
            imageRectTransform.anchoredPosition = new Vector2(-830, 380); // ����λ��
            imageRectTransform.sizeDelta = new Vector2(100, 100); // ���ÿ�Ⱥ͸߶�
            imageRectTransform.localScale = new Vector3(3, 3, 3); // ��������

            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < question.options.Length)
                {
                    optionButtons[i].gameObject.SetActive(true); // ��ʾѡ�ť
                    optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i]; // ����ѡ���ı�
                    int optionIndex = i;
                    optionButtons[i].onClick.RemoveAllListeners(); // �Ƴ�֮ǰ�ļ�����
                    optionButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex)); // ���ѡ�����¼�
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false); // ���ض����ѡ�ť
                }
            }
        }
        else
        {
            EndDialog(); // �����Ի�
        }
    }

    // ��ѡ�ѡ��ʱ����
    void OnOptionSelected(int index)
    {
        if (index == dialogQuestions[currentQuestionIndex].correctOptionIndex)
        {
            //PlaySound(rightAudio);
            currentQuestionIndex++; // ���ѡ������ȷ�𰸣�������������
            SetupQuestion(); // ������һ������
        }
        else
        {
            //PlaySound(wrongAudio);
            hintText.text = dialogQuestions[currentQuestionIndex].hint; // ������ʾ�ı�
            hintPanel.SetActive(true); // ��ʾ��ʾ��
        }
    }

    // ��ʾ��һ������
    void ShowNextQuestion()
    {
        hintPanel.SetActive(false); // ȷ����ʾ������
        currentQuestionIndex++; // ������ǰ��������
        SetupQuestion(); // ������һ������
    }

    // �����Ի�
    void EndDialog()
    {
        QAPanel.SetActive(false); // ���ضԻ���
        ShowEndDialogPanel();
        // ������һ������
        //SceneManager.LoadScene("TransitionLevel3");
    }

    void ShowEndDialogPanel()
    {
        endDialogPanel.SetActive(true); // ��ʾ�����Ի����
        endDialogText.text = "�ջ�һö�ǻۻ��£�"; // ���ý����Ի��ı�
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnEndDialogClose);
    }

    void OnEndDialogClose()
    {
        endDialogPanel.SetActive(false); // ���ؽ����Ի����
        SceneManager.LoadScene("TransitionLevel3"); // ������һ������
    }

    // ����ʾ��رհ�ť�����ʱ����
    public void OnHintPanelClose()
    {
        hintPanel.SetActive(false); // ������ʾ��
    }
}
