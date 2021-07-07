using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public bool Enlarged;
    public bool spriteChange;
    public Sprite[] sprites;
    public float defScaleAmount;
    public float defTime;

    public void CallChangeSize()
    {
        StartCoroutine(ChangeSize(defScaleAmount, defTime));
    }
    public IEnumerator ChangeSize(float ScaleAmount, float time)
    {
        Vector3 InitialSize = transform.localScale;
        Vector3 FinalSize = InitialSize * ScaleAmount;
        float fractionComplete = 0;
        float currentTime = Time.time;
        if (spriteChange == true)
        {
            if (ScaleAmount > 1)
            {
                GetComponent<Image>().sprite = sprites[1];
            }
            else
            {
                GetComponent<Image>().sprite = sprites[0];
            }
        }
        do
        {
            transform.localScale = Vector3.Lerp(InitialSize, FinalSize, fractionComplete);
            fractionComplete = (Time.time - currentTime) / time;
            yield return null;
        }
        while (fractionComplete <= 1);
        transform.localScale = FinalSize;
        if (ScaleAmount > 1)
        {
            Enlarged = true;
        }
        else if (ScaleAmount < 1)
        {
            Enlarged = false;
        }
    }
}
