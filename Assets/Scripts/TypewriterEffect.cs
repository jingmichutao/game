using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public string fullText;
    public float typingSpeed = 0.15f; // ´ò×ÖËÙ¶È
    private TMP_Text textComponent;
    private int index = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        typingCoroutine = StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        while (index < fullText.Length)
        {
            textComponent.text = fullText.Substring(0, index + 1);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}