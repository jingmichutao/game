using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PintuControl_1 : MonoBehaviour
{
    public GameObject picbtnPrefab;//按钮背景的预制体
    public GameObject btnPrefab;//拼图碎片的按钮预制体
    public GameObject canvas;//画布
    public Texture2D img;//定义要拼的图片
    public List<Vector3> posList = new List<Vector3>();//用来存放背景按钮位置坐标的集合
    public List<GameObject> btnList = new List<GameObject>();//存放拼图碎片按钮
    public static bool puzzleCompleted = false;
    public AudioSource successAudio; // 拼图成功音效
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
        //打乱顺序
        for (int i = 0; i < 100; i++)
        {
            btnList[Random.Range(0, btnList.Count)].transform.SetAsLastSibling();
        }
    }

    // 新增协程方法，用于延迟加载场景
    IEnumerator DelayedSceneLoad(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // 等待指定的时间
        SceneManager.LoadScene(sceneName); // 加载场景
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleCompleted) return; // 如果拼图已完成，则直接返回，不执行后续逻辑
        bool flag = true;//true拼好
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
            successAudio.Play(); // 播放成功音效
            StartCoroutine(DelayedSceneLoad("Level1_2", 1f)); // 等待2秒后加载新场景
            puzzleCompleted = true; // 设置完成标志

        }
    }


}
