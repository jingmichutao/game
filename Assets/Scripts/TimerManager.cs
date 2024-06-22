using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeLimit = 30f; // �涨ʱ��
    private float timeRemaining; // ʣ��ʱ��
    public GameObject hintPanel; // ��ʾ���
    public TMP_Text hintText; // ��ʾ��Ϣ
    public Button replayButton; // ���水ť
    public Button returnButton; // �������˵���ť
    public Button nextButton; // ��һ�ذ�ť
    public float feedbackDisplayTime = 2f;
    private bool isTiming = true; // ��Ǽ�ʱ�Ƿ����ڽ���

    private void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText();
        hintPanel.SetActive(false); // ��ʼʱ������ʾ���

        // �󶨰�ť����¼�
        replayButton.onClick.AddListener(OnReplayButtonClicked);
        returnButton.onClick.AddListener(OnReturnButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void Update()
    {
        if (isTiming)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTiming = false;
                ShowTimeUpMessage(); // ��ʾʱ�䵽����Ϣ
            }
            UpdateTimerText(); // ���¼�ʱ����ʾ
        }
    }

    // ���¼�ʱ���ı�
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ShowTimeUpMessage()
    {
        hintPanel.SetActive(true); // ��ʾ��ʾ���
        hintText.text = "ʱ��ľ������һ�����ǻ���һ��...����һ�ΰɣ�";
        replayButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    private void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���¼��ص�ǰ����
    }

    private void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // �������˵�����
    }

    private void OnNextButtonClicked()
    {
        SceneManager.LoadScene("Level3_1"); // ������һ�ؿ�
    }

    // ֹͣ��ʱ��
    public void StopTimer()
    {
        isTiming = false;
    }
}
