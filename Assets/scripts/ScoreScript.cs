using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Obsolete]

public class ScoreScript : NetworkBehaviour
{

    int team0Score = 0;
    int team1Score = 0;
    public TextMeshProUGUI team0Text;
    public TextMeshProUGUI team1Text;

    [ClientRpc]
    public void RpcIncreaseTeam0Score()
    {
        team0Score++;
        team0Text.text = team0Score.ToString();
    }
    [ClientRpc]
    public void RpcIncreaseTeam1Score()
    {
        team1Score++;
        team1Text.text = team1Score.ToString();
    }

}
