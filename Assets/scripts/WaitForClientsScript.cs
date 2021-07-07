using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class WaitForClientsScript : MonoBehaviour
{
    public PlayerSpawn playerSpawn;
    NetworkSetup networkSetup;

    private void Start()
    {
        networkSetup = FindObjectOfType<NetworkSetup>();
    }
    public void OnClick()
    {
        if (networkSetup.ConditionCheck() == true)
        {
            playerSpawn.RpcTransition();
        }
        
    }
    
}
