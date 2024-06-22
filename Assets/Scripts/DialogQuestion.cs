using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogQuestion
{
    public string question; // 问题文本
    public string[] options; // 选项数组
    public int correctOptionIndex; // 正确选项的索引
    public string hint; // 错误提示信息
    public Sprite image; // 提问者图片
}

