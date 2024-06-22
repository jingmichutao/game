using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; // ���ڶ�λ�͵�����С
    private CanvasGroup canvasGroup; // ���ڿ���͸���Ⱥͽ���
    private Vector2 originalPosition; // �洢�϶�ǰ��λ��
    private Transform originalParent; // �洢�϶�ǰ�ĸ�����

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent; // ��¼�϶�ǰ�ĸ�����
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale; // ��������ƶ��ľ������λ��
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // �ָ���ȫ��͸��
        canvasGroup.blocksRaycasts = true; // �ָ�����

        // ����϶�����ʱ���λ���Ƿ�����Ч��λ��
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropSlot>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
            transform.SetParent(originalParent);
        }
    }
}