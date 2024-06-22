using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnDrag : MonoBehaviour,IDragHandler
{
    Vector2 canvasSize;//画布大小
    PintuControl ptctrl;//在pintuControl中使用
    public AudioSource snapAudioSource; // 引用音效的 AudioSource
    public void OnDrag(PointerEventData eventData) 
    {
       
        //Debug.Log(Input.mousePosition);//屏幕坐标 （鼠标 左下角原点
        transform.localPosition = ScreenPosToUIPos(Input.mousePosition);//按钮 UI坐标（正中间原点
        //按钮吸附
        for(int i = 0;i<ptctrl.posList.Count;i++)
        {
            if (Vector3.Distance(transform.localPosition, ptctrl.posList[i]) < 150)
            {
                transform.localPosition= ptctrl.posList[i];
                snapAudioSource.Play(); // 播放吸附音效
            }
        }
    }

    Vector3 ScreenPosToUIPos(Vector3 screenPos)//屏幕坐标-》UI坐标 两者尺寸也不一样
    {
        float x = screenPos.x - Screen.width / 2;
        float y = screenPos.y - Screen.height / 2;
        x = x * canvasSize.x / Screen.width;
        y = y * canvasSize.y / Screen.height;
        return new Vector3(x, y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        canvasSize = canvasRect.sizeDelta;
        ptctrl = GameObject.Find("Main Camera").GetComponent<PintuControl>();
        //Debug.Log(canvasSize);
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
