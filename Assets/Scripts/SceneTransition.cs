using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Button closeButton; // 关闭按钮
    public AudioSource buttonAudio; // 按钮音效

    private void Start()
    {
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => {
                buttonAudio.Play();
                TransNext();
            });
        }
        else
        {
            Debug.LogError("closeButton is not assigned in the Inspector.");
        }
    }

    public void TransNext()
    {
        SceneManager.LoadScene("TransitionLevel2");
    }
}
