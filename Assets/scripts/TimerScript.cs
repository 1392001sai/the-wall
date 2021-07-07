using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

[System.Obsolete]
public class TimerScript : NetworkBehaviour
{
    public TextMeshProUGUI curtext;
    public int TimerTime;
    bool Started = false;
    public TextMeshProUGUI score0, score1;

    private void Update()
    {
        if (TimerTime != -1 && Started == false)
        {
            StartCoroutine(changeText());
            Started = true;
        }
    }

    IEnumerator changeText()
    {
        do
        {
            curtext.text = TimerTime.ToString();
            yield return new WaitForSeconds(1);
            TimerTime--;
        }
        while (TimerTime != 0);
        PlayerPrefs.SetString("score0", score0.text);
        PlayerPrefs.SetString("score1", score1.text);
        if (FindObjectOfType<BgmScript>() != null)
        {
            FindObjectOfType<BgmScript>().VolInc();
        }
        NetworkManager.singleton.StopHost();
        Destroy(FindObjectOfType<NetworkManager>().gameObject);
        Destroy(FindObjectOfType<FindHost>().gameObject);

        GameObject.FindWithTag("LevelPanel").GetComponent<Animator>().SetTrigger("ShiftRight");
    }
}
