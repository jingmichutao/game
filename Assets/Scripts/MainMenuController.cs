using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public Image overlayImage;  // �����ڵ�����

    public void StartGame()
    {
        StartCoroutine(LoadTransitionScene());
    }

    public void ShowInstructions()
    {
        // SceneManager.LoadScene("InstructScene"); // ������Ϸ˵������
        StartCoroutine(LoadInstructScene());
    }

    public void QuitGame()
    {
        Application.Quit(); // ֱ���˳���Ϸ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator LoadTransitionScene()
    {
        // ��ʾ�ڵ��������߼����ֲ���
        overlayImage.gameObject.SetActive(true);
        Color overlayColor = overlayImage.color;
        overlayColor.a = 1f;
        overlayImage.color = overlayColor;

        // �ȴ�һ֡����ȷ���ڵ�������ʾ����
        yield return null;

        // �л������ɳ���
        SceneManager.LoadScene("TransitionLevel1");
    }

    IEnumerator LoadInstructScene()
    {
        // ��ʾ�ڵ��������߼����ֲ���
        overlayImage.gameObject.SetActive(true);
        Color overlayColor = overlayImage.color;
        overlayColor.a = 1f;
        overlayImage.color = overlayColor;

        // �ȴ�һ֡����ȷ���ڵ�������ʾ����
        yield return null;

        // �л������ɳ���
        SceneManager.LoadScene("InstructScene");
    }
}