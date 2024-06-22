using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogQuestion
{
    public string question; // �����ı�
    public string[] options; // ѡ������
    public int correctOptionIndex; // ��ȷѡ�������
    public string hint; // ������ʾ��Ϣ
    public Sprite image; // ������ͼƬ
}

