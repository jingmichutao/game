using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeLimit = 30f; // 规定时间
    private float timeRemaining; // 剩余时间
    public GameObject hintPanel; // 提示面板
    public TMP_Text hintText; // 提示信息
    public Button replayButton; // 重玩按钮
    public Button returnButton; // 返回主菜单按钮
    public Button nextButton; // 下一关按钮
    public float feedbackDisplayTime = 2f;
    private bool isTiming = true; // 标记计时是否正在进行

    private void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText();
        hintPanel.SetActive(false); // 初始时隐藏提示面板

        // 绑定按钮点击事件
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
                ShowTimeUpMessage(); // 显示时间到的信息
            }
            UpdateTimerText(); // 更新计时器显示
        }
    }

    // 更新计时器文本
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ShowTimeUpMessage()
    {
        hintPanel.SetActive(true); // 显示提示面板
        hintText.text = "时间耗尽，最后一关总是会难一点...再试一次吧！";
        replayButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    private void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载当前场景
    }

    private void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // 加载主菜单场景
    }

    private void OnNextButtonClicked()
    {
        SceneManager.LoadScene("Level3_1"); // 加载下一关卡
    }

    // 停止计时器
    public void StopTimer()
    {
        isTiming = false;
    }
}
