using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingForServerAnimScript : MonoBehaviour
{
    public TextMeshProUGUI curtext;
    public string BaseText = "Waiting For Server";
    public float TimeGap;
    int NoOfDots;

    private void Start()
    {
        NoOfDots = 0;
        StartCoroutine(changeText());
    }

    IEnumerator changeText()
    {
        do
        {
            yield return new WaitForSeconds(TimeGap);
            if (NoOfDots == 3)
            {
                NoOfDots = 0;
                curtext.text = BaseText;
            }
            curtext.text += " . ";
            NoOfDots++;
        }
        while (true);
    }
}
