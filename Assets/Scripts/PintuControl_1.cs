using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PintuControl_1 : MonoBehaviour
{
    public GameObject picbtnPrefab;//��ť������Ԥ����
    public GameObject btnPrefab;//ƴͼ��Ƭ�İ�ťԤ����
    public GameObject canvas;//����
    public Texture2D img;//����Ҫƴ��ͼƬ
    public List<Vector3> posList = new List<Vector3>();//������ű�����ťλ������ļ���
    public List<GameObject> btnList = new List<GameObject>();//���ƴͼ��Ƭ��ť
    public static bool puzzleCompleted = false;
    public AudioSource successAudio; // ƴͼ�ɹ���Ч
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject ins = Instantiate(picbtnPrefab, canvas.transform);
            ins.transform.localPosition = new Vector3(-250 + 250 * (i % 3) + 450, -250 + 250 * (i / 3), 0);
            posList.Add(ins.transform.localPosition);
            //ins.layer = 5;
        }

        for (int i = 0; i < 9; i++)
        {
            GameObject ins = Instantiate(btnPrefab, canvas.transform);
            ins.transform.localPosition = new Vector3(-180, 200, 0);
            Texture2D smallImg = new Texture2D(img.width / 3, img.height / 3);

            Color[] colors = img.GetPixels((i % 3) * smallImg.width, (i / 3) * smallImg.height, smallImg.width, smallImg.height);
            smallImg.SetPixels(0, 0, smallImg.width, smallImg.height, colors);
            smallImg.Apply();
            ins.GetComponent<RawImage>().texture = smallImg;

            btnList.Add(ins);
            //ins.layer = 5;
        }
        //����˳��
        for (int i = 0; i < 100; i++)
        {
            btnList[Random.Range(0, btnList.Count)].transform.SetAsLastSibling();
        }
    }

    // ����Э�̷����������ӳټ��س���
    IEnumerator DelayedSceneLoad(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // �ȴ�ָ����ʱ��
        SceneManager.LoadScene(sceneName); // ���س���
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleCompleted) return; // ���ƴͼ����ɣ���ֱ�ӷ��أ���ִ�к����߼�
        bool flag = true;//trueƴ��
        for (int i = 0; i < btnList.Count; i++)
        {
            if (btnList[i].transform.localPosition != posList[i])
            {
                flag = false;
                break;
            }
        }
        if (flag)
        {
            Debug.Log("Success!");
            successAudio.Play(); // ���ųɹ���Ч
            StartCoroutine(DelayedSceneLoad("Level1_2", 1f)); // �ȴ�2�������³���
            puzzleCompleted = true; // ������ɱ�־

        }
    }


}
