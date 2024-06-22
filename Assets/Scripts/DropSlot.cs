using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag; // 获取被拖放的游戏对象
        Draggable draggableItem = dropped.GetComponent<Draggable>();  // 获取被拖放对象上的Draggable组件
        if (draggableItem != null)
        {
            // 将被拖放物体的位置设置为此DropSlot的位置
            RectTransform droppedRectTransform = dropped.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            // 将拖放物体的父物体设置为此槽位
            dropped.transform.SetParent(transform);

            // 重置拖放物体的锚点以确保其居中对齐
            droppedRectTransform.anchorMin = slotRectTransform.anchorMin;
            droppedRectTransform.anchorMax = slotRectTransform.anchorMax;
            droppedRectTransform.pivot = slotRectTransform.pivot;

            // 将拖放物体的位置设置为槽位的中心位置
            droppedRectTransform.localPosition = Vector3.zero;
        }
    }
}
