using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class TextGlow : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color HDRColor;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().color = HDRColor;
    }
}
