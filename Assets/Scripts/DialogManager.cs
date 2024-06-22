using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Image questionImage; // 显示问题图片的UI组件
    public TMP_Text questionText; // 显示问题的UI文本
    public TMP_Text hintText; // 显示提示信息的UI文本
    public Button[] optionButtons; // 选项按钮数组
    public GameObject QAPanel; // 对话框面板
    public GameObject hintPanel; // 提示框面板
    public Button closeHintButton; // 关闭按钮引用
    public Button nextQuestionButton; // 下一条按钮引用

    public GameObject endDialogPanel; // 结束对话面板
    public TMP_Text endDialogText; // 结束对话的文本
    public Image badgeImage; // 徽章图像
    public Button closeButton; // 关闭按钮

    public DialogQuestion[] dialogQuestions; // 对话问题数组

    private int currentQuestionIndex = 0; // 当前问题索引

    //public AudioSource rightAudio; // 答对音效
    //public AudioSource wrongAudio; // 答错音效
    public AudioSource buttonAudio; // 按钮音效

    void Start()
    {
        SetupQuestion(); // 设置第一个问题
        SetupButtonListeners();

        endDialogPanel.SetActive(false); // 确保结束对话面板隐藏
    }

    // 设置按钮监听器
    void SetupButtonListeners()
    {
        closeHintButton.onClick.AddListener(() => {
            PlaySound(buttonAudio);
            OnHintPanelClose();
        }); // 关闭提示框按钮点击事件
        //nextButton.onClick.AddListener(() => ShowNextQuestion()); // 下一个问题按钮点击事件
    }

    void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // 设置当前问题
    void SetupQuestion()
    {
        hintPanel.SetActive(false); // 确保提示框隐藏
        QAPanel.SetActive(true); // 确保对话框可见

        if (currentQuestionIndex < dialogQuestions.Length)
        {
            DialogQuestion question = dialogQuestions[currentQuestionIndex];
            questionText.text = question.question; // 设置问题文本
            questionImage.sprite = question.image; // 设置问题图片

            // 设置问题图片的大小和位置
            RectTransform imageRectTransform = questionImage.GetComponent<RectTransform>();
            imageRectTransform.anchoredPosition = new Vector2(-830, 380); // 设置位置
            imageRectTransform.sizeDelta = new Vector2(100, 100); // 设置宽度和高度
            imageRectTransform.localScale = new Vector3(3, 3, 3); // 设置缩放

            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < question.options.Length)
                {
                    optionButtons[i].gameObject.SetActive(true); // 显示选项按钮
                    optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i]; // 设置选项文本
                    int optionIndex = i;
                    optionButtons[i].onClick.RemoveAllListeners(); // 移除之前的监听器
                    optionButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex)); // 添加选项点击事件
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false); // 隐藏多余的选项按钮
                }
            }
        }
        else
        {
            EndDialog(); // 结束对话
        }
    }

    // 当选项被选择时调用
    void OnOptionSelected(int index)
    {
        if (index == dialogQuestions[currentQuestionIndex].correctOptionIndex)
        {
            //PlaySound(rightAudio);
            currentQuestionIndex++; // 如果选择了正确答案，递增问题索引
            SetupQuestion(); // 设置下一个问题
        }
        else
        {
            //PlaySound(wrongAudio);
            hintText.text = dialogQuestions[currentQuestionIndex].hint; // 设置提示文本
            hintPanel.SetActive(true); // 显示提示框
        }
    }

    // 显示下一个问题
    void ShowNextQuestion()
    {
        hintPanel.SetActive(false); // 确保提示框隐藏
        currentQuestionIndex++; // 递增当前问题索引
        SetupQuestion(); // 设置下一个问题
    }

    // 结束对话
    void EndDialog()
    {
        QAPanel.SetActive(false); // 隐藏对话框
        ShowEndDialogPanel();
        // 加载下一个场景
        //SceneManager.LoadScene("TransitionLevel3");
    }

    void ShowEndDialogPanel()
    {
        endDialogPanel.SetActive(true); // 显示结束对话面板
        endDialogText.text = "收获一枚智慧徽章！"; // 设置结束对话文本
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnEndDialogClose);
    }

    void OnEndDialogClose()
    {
        endDialogPanel.SetActive(false); // 隐藏结束对话面板
        SceneManager.LoadScene("TransitionLevel3"); // 加载下一个场景
    }

    // 当提示框关闭按钮被点击时调用
    public void OnHintPanelClose()
    {
        hintPanel.SetActive(false); // 隐藏提示框
    }
}
