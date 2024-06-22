using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SortingManager : MonoBehaviour
{
    public GameObject[] slots; // 用于存储游戏对象槽位，按照正确排序摆放
    public TimerManager timerManager; // 用于控制游戏时间
    public float feedbackDisplayTime = 1.5f;

    public GameObject hintPanel; // 提示面板
    public TMP_Text hintText; // 提示信息文本
    public Button replayButton; // 重玩按钮
    public Button returnButton; // 返回主菜单按钮
    public Button nextButton; // 下一关按钮
    public Button checkButton; // 检查排序按钮

    private void Start()
    {
        hintPanel.SetActive(false); // 初始时隐藏提示面板

        // 绑定按钮点击事件
        replayButton.onClick.AddListener(OnReplayButtonClicked);
        returnButton.onClick.AddListener(OnReturnButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        checkButton.onClick.AddListener(OnCheckButtonClicked); // 绑定检查排序按钮事件
    }

    private void OnCheckButtonClicked()
    {
        timerManager.StopTimer(); // 停止计时
        CheckSorting(); // 检查排序
    }

    // 检查是否正确排序
    public void CheckSorting()
    {
        string[] correctOrder = { "Item3", "Item1", "Item4", "Item5", "Item2" }; // 正确的物品排序名称数组
        bool isCorrect = true;
        // 遍历所有槽位
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                GameObject item = slots[i].transform.GetChild(0).gameObject;
                if (item.name != correctOrder[i]) // 判断物品名称是否与正确排序对应的位置名称一致
                {
                    isCorrect = false; // 不一致则标记为排序错误，并跳出循环
                    break;
                }
            }
            else
            {
                isCorrect = false;
                break;
            }
        }

        hintPanel.SetActive(true); // 显示提示面板

        if (isCorrect)
        {
            hintText.text = "成功击败知识怪兽！获得一颗智慧之石！";
            replayButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            hintText.text = "很遗憾，未能正确排列，再试一次吧！";
            replayButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
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
}
