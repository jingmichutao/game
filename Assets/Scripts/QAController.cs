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
        "身体变化：青春期开始后，女童的身体会发生显著变化，例如_______。",
        "个人卫生：为了保持个人卫生，女童在月经期间应定期更换_______，并保持外阴部的清洁。",
        "当有人未经允许触碰你的身体时，你有权利_______并离开现场，寻求帮助。",
        "在网络空间中，与陌生人聊天时，应避免泄露_______。",
        "如果在公共场所感觉到不安全，可以向_______寻求帮助。",
        "在家中独处时，应确保_______是锁好的。",
        "定期进行_______可以帮助早期发现健康问题。"
    };

    private readonly string[][] answers =
    {
        new string[] { "乳房发育", "月经初潮", "身体比例变化", "身型变化", "皮肤变油", "青春痘" },
        new string[] { "卫生巾", "卫生棉条", "月经杯" },
        new string[] { "说“不”", "拒绝", "抵抗" ,"说不" },
        new string[] { "个人信息", "地址", "电话", "学校名称", "信息", "隐私", "个人隐私"},
        new string[] { "警察", "保安", "成年人", "店员" },
        new string[] { "门", "窗户", "安全锁", "门窗" },
        new string[] { "体检", "健康检查", "医生检查","检查" }
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
        scoreText.text = "得分: " + score;
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
