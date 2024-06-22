using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag; // ��ȡ���Ϸŵ���Ϸ����
        Draggable draggableItem = dropped.GetComponent<Draggable>();  // ��ȡ���ϷŶ����ϵ�Draggable���
        if (draggableItem != null)
        {
            // �����Ϸ������λ������Ϊ��DropSlot��λ��
            RectTransform droppedRectTransform = dropped.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            // ���Ϸ�����ĸ���������Ϊ�˲�λ
            dropped.transform.SetParent(transform);

            // �����Ϸ������ê����ȷ������ж���
            droppedRectTransform.anchorMin = slotRectTransform.anchorMin;
            droppedRectTransform.anchorMax = slotRectTransform.anchorMax;
            droppedRectTransform.pivot = slotRectTransform.pivot;

            // ���Ϸ������λ������Ϊ��λ������λ��
            droppedRectTransform.localPosition = Vector3.zero;
        }
    }
}
