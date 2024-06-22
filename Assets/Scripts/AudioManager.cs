using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;  // 单例模式实例
    private AudioSource audioSource;  // AudioSource组件

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 确保音乐管理器在场景切换时不会被销毁
        }
        else
        {
            Destroy(gameObject);
        }
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource != null)
        {
            // 如果 bgm 正在播放，点击按钮暂停播放
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