using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnDrag : MonoBehaviour,IDragHandler
{
    Vector2 canvasSize;//������С
    PintuControl ptctrl;//��pintuControl��ʹ��
    public AudioSource snapAudioSource; // ������Ч�� AudioSource
    public void OnDrag(PointerEventData eventData) 
    {
       
        //Debug.Log(Input.mousePosition);//��Ļ���� ����� ���½�ԭ��
        transform.localPosition = ScreenPosToUIPos(Input.mousePosition);//��ť UI���꣨���м�ԭ��
        //��ť����
        for(int i = 0;i<ptctrl.posList.Count;i++)
        {
            if (Vector3.Distance(transform.localPosition, ptctrl.posList[i]) < 150)
            {
                transform.localPosition= ptctrl.posList[i];
                snapAudioSource.Play(); // ����������Ч
            }
        }
    }

    Vector3 ScreenPosToUIPos(Vector3 screenPos)//��Ļ����-��UI���� ���߳ߴ�Ҳ��һ��
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
