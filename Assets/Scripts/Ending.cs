using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Ending : MonoBehaviour
{
    public Image overlayImage;  // �����ڵ�����

    public void StartGame()
    {
        StartCoroutine(LoadTransitionScene());
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

        // �л�����ʼ����
        SceneManager.LoadScene("MainMenu");
    }
}
