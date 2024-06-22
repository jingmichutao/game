using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QAController : MonoBehaviour
{
    public Image monsterImage;
    public TMP_Text scoreText;
    public TMP_InputField answerInput;
    public Button submitButton;
    public TMP_Text questionText;
    public GameObject successPanel;
    public GameObject failurePanel;
    public GameObject endCanvas; // Reference to the EndCanvas GameObject
    public Button retryButton;
    public Button mainMenuButton;
    public Button nextLevelButton; // Button on EndCanvas to load next level

    private int score = 0;
    private float monsterScale = 1.0f;
    private List<int> questionOrder = new List<int>(); // List to store the random order of questions
    private readonly string[] questions =
    {
        "����仯���ഺ�ڿ�ʼ��Ůͯ������ᷢ�������仯������_______��",
        "����������Ϊ�˱��ָ���������Ůͯ���¾��ڼ�Ӧ���ڸ���_______������������������ࡣ",
        "������δ���������������ʱ������Ȩ��_______���뿪�ֳ���Ѱ�������",
        "������ռ��У���İ��������ʱ��Ӧ����й¶_______��",
        "����ڹ��������о�������ȫ��������_______Ѱ�������",
        "�ڼ��ж���ʱ��Ӧȷ��_______�����õġ�",
        "���ڽ���_______���԰������ڷ��ֽ������⡣"
    };

    private readonly string[][] answers =
    {
        new string[] { "�鷿����", "�¾�����", "��������仯", "���ͱ仯", "Ƥ������", "�ഺ��" },
        new string[] { "������", "��������", "�¾���" },
        new string[] { "˵������", "�ܾ�", "�ֿ�" ,"˵��" },
        new string[] { "������Ϣ", "��ַ", "�绰", "ѧУ����", "��Ϣ", "��˽", "������˽"},
        new string[] { "����", "����", "������", "��Ա" },
        new string[] { "��", "����", "��ȫ��", "�Ŵ�" },
        new string[] { "���", "�������", "ҽ�����","���" }
    };

    private int currentQuestionIndex = 0;

    void Start()
    {
        successPanel.SetActive(false);
        failurePanel.SetActive(false);
        endCanvas.SetActive(false); // Hide the EndCanvas initially

        // Initialize random question order
        for (int i = 0; i < questions.Length; i++)
        {
            questionOrder.Add(i);
        }
        ShuffleQuestions();

        UpdateUI();
        submitButton.onClick.AddListener(CheckAnswer);
        retryButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        // Setup nextLevelButton click event to load next level
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < questionOrder.Count; i++)
        {
            int temp = questionOrder[i];
            int randomIndex = Random.Range(i, questionOrder.Count);
            questionOrder[i] = questionOrder[randomIndex];
            questionOrder[randomIndex] = temp;
        }
    }

    void CheckAnswer()
    {
        string userAnswer = answerInput.text.Trim();
        bool isCorrect = CheckCorrectAnswer(userAnswer, questionOrder[currentQuestionIndex]);

        if (isCorrect)
        {
            score += 15;
            monsterScale -= 0.1f;
        }
        else
        {
            score -= 15;
            monsterScale += 0.1f;
        }

        monsterImage.rectTransform.localScale = new Vector3(monsterScale, monsterScale, 1);

        // Check if score is greater than or equal to 60
        if (score >= 60)
        {
            successPanel.SetActive(true);
            monsterImage.gameObject.SetActive(false);
            submitButton.interactable = false;
            endCanvas.SetActive(true);
            return;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            failurePanel.SetActive(true);
            submitButton.interactable = false;
        }
        else
        {
            UpdateUI();
        }
    }

    bool CheckCorrectAnswer(string userAnswer, int questionIndex)
    {
        string[] correctAnswers = answers[questionIndex];
        foreach (string answer in correctAnswers)
        {
            if (userAnswer.Contains(answer))
            {
                return true;
            }
        }
        return false;
    }

    void UpdateUI()
    {
        scoreText.text = "�÷�: " + score;
        questionText.text = questions[questionOrder[currentQuestionIndex]];
        answerInput.text = "";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("EndScene");
    }
}
