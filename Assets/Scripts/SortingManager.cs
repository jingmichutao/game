using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SortingManager : MonoBehaviour
{
    public GameObject[] slots; // ���ڴ洢��Ϸ�����λ��������ȷ����ڷ�
    public TimerManager timerManager; // ���ڿ�����Ϸʱ��
    public float feedbackDisplayTime = 1.5f;

    public GameObject hintPanel; // ��ʾ���
    public TMP_Text hintText; // ��ʾ��Ϣ�ı�
    public Button replayButton; // ���水ť
    public Button returnButton; // �������˵���ť
    public Button nextButton; // ��һ�ذ�ť
    public Button checkButton; // �������ť

    private void Start()
    {
        hintPanel.SetActive(false); // ��ʼʱ������ʾ���

        // �󶨰�ť����¼�
        replayButton.onClick.AddListener(OnReplayButtonClicked);
        returnButton.onClick.AddListener(OnReturnButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        checkButton.onClick.AddListener(OnCheckButtonClicked); // �󶨼������ť�¼�
    }

    private void OnCheckButtonClicked()
    {
        timerManager.StopTimer(); // ֹͣ��ʱ
        CheckSorting(); // �������
    }

    // ����Ƿ���ȷ����
    public void CheckSorting()
    {
        string[] correctOrder = { "Item3", "Item1", "Item4", "Item5", "Item2" }; // ��ȷ����Ʒ������������
        bool isCorrect = true;
        // �������в�λ
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                GameObject item = slots[i].transform.GetChild(0).gameObject;
                if (item.name != correctOrder[i]) // �ж���Ʒ�����Ƿ�����ȷ�����Ӧ��λ������һ��
                {
                    isCorrect = false; // ��һ������Ϊ������󣬲�����ѭ��
                    break;
                }
            }
            else
            {
                isCorrect = false;
                break;
            }
        }

        hintPanel.SetActive(true); // ��ʾ��ʾ���

        if (isCorrect)
        {
            hintText.text = "�ɹ�����֪ʶ���ޣ����һ���ǻ�֮ʯ��";
            replayButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            hintText.text = "���ź���δ����ȷ���У�����һ�ΰɣ�";
            replayButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���¼��ص�ǰ����
    }

    private void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // �������˵�����
    }

    private void OnNextButtonClicked()
    {
        SceneManager.LoadScene("Level3_1"); // ������һ�ؿ�
    }
}
