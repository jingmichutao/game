using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public Image overlayImage;  // 用于遮挡背景

    public void StartGame()
    {
        StartCoroutine(LoadTransitionScene());
    }

    public void ShowInstructions()
    {
        // SceneManager.LoadScene("InstructScene"); // 加载游戏说明场景
        StartCoroutine(LoadInstructScene());
    }

    public void QuitGame()
    {
        Application.Quit(); // 直接退出游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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

        // 切换到过渡场景
        SceneManager.LoadScene("TransitionLevel1");
    }

    IEnumerator LoadInstructScene()
    {
        // 显示遮挡背景的逻辑保持不变
        overlayImage.gameObject.SetActive(true);
        Color overlayColor = overlayImage.color;
        overlayColor.a = 1f;
        overlayImage.color = overlayColor;

        // 等待一帧，以确保遮挡背景显示出来
        yield return null;

        // 切换到过渡场景
        SceneManager.LoadScene("InstructScene");
    }
}