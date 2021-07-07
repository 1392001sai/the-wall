using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class TeamSelectScript : MonoBehaviour
{
   
    public int team;
    public ButtonAnimation TeamBlueSelector;
    public ButtonAnimation TeamRedSelector;
    public void TeamBlue()
    {
        team = 0;
        if (TeamBlueSelector.Enlarged == false)
        {
            StartCoroutine(TeamBlueSelector.ChangeSize(1.2f, 0.05f));
            if (FindObjectOfType<NetworkSetup>().localPlayer != null)
            {
                FindObjectOfType<NetworkSetup>().localPlayer.GetComponent<PlayerSpawn>().UpdateTeam(team);
            }
        }
        if (TeamRedSelector.Enlarged == true)
        {
            StartCoroutine(TeamRedSelector.ChangeSize((1f/1.2f), 0.05f));
        }

    }
    public void TeamRed()
    {
        team = 1;
        if (TeamBlueSelector.Enlarged == true)
        {
            StartCoroutine(TeamBlueSelector.ChangeSize((1f / 1.2f), 0.05f));
        }
        if (TeamRedSelector.Enlarged == false)
        {
            StartCoroutine(TeamRedSelector.ChangeSize(1.2f, 0.05f));
            if (FindObjectOfType<NetworkSetup>().localPlayer != null)
            {
                FindObjectOfType<NetworkSetup>().localPlayer.GetComponent<PlayerSpawn>().UpdateTeam(team);
            }
        }

    }
}
