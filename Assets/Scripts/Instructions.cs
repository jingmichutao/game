using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Instructions : MonoBehaviour
{
    public Image overlayImage;  // �����ڵ�����
    public Button nextButton;
    public Button laterButton;
    public Button returnButton;

    private enum PageState { First, Second, Third, Fourth }
    private PageState currentPageState = PageState.First;

    public TextMeshProUGUI[] pageTexts; // �������洢ÿ��ҳ���TextMeshPro�ı����

    private void Start()
    {
        // ��ʼ����ť��ʾ״̬
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

    private void UpdateButtonsVisibility()
    {
        // ���°�ť��ʾ״̬
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

        // ����ҳ���ı���ʾ״̬
        for (int i = 0; i < pageTexts.Length; i++)
        {
            pageTexts[i].gameObject.SetActive(i == (int)currentPageState);
        }
    }
}
