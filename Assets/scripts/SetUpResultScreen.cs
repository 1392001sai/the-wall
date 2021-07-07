using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Obsolete]

public class SetUpResultScreen : MonoBehaviour
{
    public GameObject RedWinText, BlueWinText, DrawText, RedWinImage, BlueWinImage;
    public TextMeshProUGUI score0, score1;

    private void Start()
    {
        AdManager adManager = FindObjectOfType<AdManager>();
        adManager.ShowInterstitalAd();
        //adManager.ShowBannerAd();
        score0.text = PlayerPrefs.GetString("score0");
        score1.text = PlayerPrefs.GetString("score1");
        if (int.Parse(score0.text) < int.Parse(score1.text))
        {
            RedWinImage.SetActive(true);
            RedWinText.SetActive(true);
        }
        else if(int.Parse(score0.text) == int.Parse(score1.text))
        {
            RedWinImage.SetActive(true);
            BlueWinImage.SetActive(true);
            DrawText.SetActive(true);
        }
        else
        {
            BlueWinImage.SetActive(true);
            BlueWinText.SetActive(true);
        }
    }
}
