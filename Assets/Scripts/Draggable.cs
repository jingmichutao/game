using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; // 用于定位和调整大小
    private CanvasGroup canvasGroup; // 用于控制透明度和交互
    private Vector2 originalPosition; // 存储拖动前的位置
    private Transform originalParent; // 存储拖动前的父物体

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent; // 记录拖动前的父物体
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale; // 根据鼠标移动的距离更新位置
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // 恢复完全不透明
        canvasGroup.blocksRaycasts = true; // 恢复交互

        // 检查拖动结束时鼠标位置是否在有效槽位内
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropSlot>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
            transform.SetParent(originalParent);
        }
    }
}