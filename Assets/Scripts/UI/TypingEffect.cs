using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI target;
    public string[] text;
    public float waitingTime;

    private void Start()
    {
        StartCoroutine(_typing());
    }
    IEnumerator _typing()
    {
        yield return new WaitForSeconds(waitingTime);
        for (int i = 0; i < text.Length; i++)
        {
            target.text = null;
            for (int j = 0; j <= text[i].Length; j++)
            {
                target.text = text[i].Substring(0, j);
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        target.text = null;
    }

}
