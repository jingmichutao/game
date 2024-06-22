using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Instructions : MonoBehaviour
{
    public Image overlayImage;  // 用于遮挡背景
    public Button nextButton;
    public Button laterButton;
    public Button returnButton;

    private enum PageState { First, Second, Third, Fourth }
    private PageState currentPageState = PageState.First;

    public TextMeshProUGUI[] pageTexts; // 新增：存储每个页面的TextMeshPro文本组件

    private void Start()
    {
        // 初始化按钮显示状态
        UpdateButtonsVisibility();
    }

    public void OnNextButtonClicked()
    {
        if (currentPageState < PageState.Fourth)
        {
            currentPageState++;
            UpdateButtonsVisibility();
        }
        else
        {
            StartCoroutine(LoadTransitionScene());
        }
    }

    public void OnLaterButtonClicked()
    {
        if (currentPageState > PageState.First)
        {
            currentPageState--;
            UpdateButtonsVisibility();
        }
    }

    public void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadTransitionScene()
    {
        // 显示遮挡背景的逻辑保持不变
        overlayImage.gameObject.SetActive(true);
        Color overlayColor = overlayImage.color;
        overlayColor.a = 1f;
        overlayImage.color = overlayColor;

        // 等待一帧，以确保遮挡背景显示出来
        yield return null;

        // 切换到开始场景
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateButtonsVisibility()
    {
        // 更新按钮显示状态
        switch (currentPageState)
        {
            case PageState.First:
                nextButton.gameObject.SetActive(true);
                laterButton.gameObject.SetActive(false);
                returnButton.gameObject.SetActive(false);
                break;
            case PageState.Second:
            case PageState.Third:
                nextButton.gameObject.SetActive(true);
                laterButton.gameObject.SetActive(true);
                returnButton.gameObject.SetActive(false);
                break;
            case PageState.Fourth:
                nextButton.gameObject.SetActive(false);
                laterButton.gameObject.SetActive(true);
                returnButton.gameObject.SetActive(true);
                break;
        }

        // 更新页面文本显示状态
        for (int i = 0; i < pageTexts.Length; i++)
        {
            pageTexts[i].gameObject.SetActive(i == (int)currentPageState);
        }
    }
}
