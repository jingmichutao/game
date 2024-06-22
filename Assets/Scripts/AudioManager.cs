using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;  // ����ģʽʵ��
    private AudioSource audioSource;  // AudioSource���

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ȷ�����ֹ������ڳ����л�ʱ���ᱻ����
        }
        else
        {
            Destroy(gameObject);
        }
        // ��ȡ AudioSource ���
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource != null)
        {
            // ��� bgm ���ڲ��ţ������ť��ͣ����
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}