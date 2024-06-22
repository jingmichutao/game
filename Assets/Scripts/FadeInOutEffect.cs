using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutEffect : MonoBehaviour
{
    public float fadeDuration = 2f; // 淡入持续时间
    public float delayBeforeFadeOut = 5f; // 淡入完成后延迟多久开始淡出
    private Image image;
    private float elapsedTime = 0f;
    public GameObject witchPrefab; // 小女巫Prefab
    public string mainSceneName = "MainMenu"; // 主场景名称

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeInAndOut());
    }
    
    IEnumerator FadeInAndOut()
    {
        // 淡入
        while (elapsedTime < fadeDuration)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 淡入完成，等待一段时间
        elapsedTime = 0f;
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // 实例化小女巫并设置位置和缩放
        GameObject witch = Instantiate(witchPrefab, transform.position, Quaternion.identity);
        witch.transform.localScale = new Vector3(10f, 10f, 10f); // 假设将小女巫放大4倍
        //witch.transform.position = new Vector3(-Screen.width / 2, 0, 0); // 假设将小女巫放置在屏幕左侧中间

        // 淡出
        while (elapsedTime < fadeDuration)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;

            // 移动小女巫
            witch.transform.Translate(Vector3.right * Time.deltaTime * 100f); // 每秒移动100单位

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 淡出完成，销毁小女巫
        Destroy(witch);

        // 切换到MainScene场景
        SceneManager.LoadScene(mainSceneName);
    }
}
