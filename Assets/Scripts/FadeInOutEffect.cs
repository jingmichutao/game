using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutEffect : MonoBehaviour
{
    public float fadeDuration = 2f; // �������ʱ��
    public float delayBeforeFadeOut = 5f; // ������ɺ��ӳٶ�ÿ�ʼ����
    private Image image;
    private float elapsedTime = 0f;
    public GameObject witchPrefab; // СŮ��Prefab
    public string mainSceneName = "MainMenu"; // ����������

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeInAndOut());
    }
    
    IEnumerator FadeInAndOut()
    {
        // ����
        while (elapsedTime < fadeDuration)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ɣ��ȴ�һ��ʱ��
        elapsedTime = 0f;
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // ʵ����СŮ�ײ�����λ�ú�����
        GameObject witch = Instantiate(witchPrefab, transform.position, Quaternion.identity);
        witch.transform.localScale = new Vector3(10f, 10f, 10f); // ���轫СŮ�׷Ŵ�4��
        //witch.transform.position = new Vector3(-Screen.width / 2, 0, 0); // ���轫СŮ�׷�������Ļ����м�

        // ����
        while (elapsedTime < fadeDuration)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;

            // �ƶ�СŮ��
            witch.transform.Translate(Vector3.right * Time.deltaTime * 100f); // ÿ���ƶ�100��λ

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ɣ�����СŮ��
        Destroy(witch);

        // �л���MainScene����
        SceneManager.LoadScene(mainSceneName);
    }
}
