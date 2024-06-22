using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Ending : MonoBehaviour
{
    public Image overlayImage;  // 用于遮挡背景

    public void StartGame()
    {
        StartCoroutine(LoadTransitionScene());
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

        // 切换到开始场景
        SceneManager.LoadScene("MainMenu");
    }
}
