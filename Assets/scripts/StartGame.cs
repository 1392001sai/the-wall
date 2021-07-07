using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Obsolete]
public class StartGame : MonoBehaviour
{
    public PlayerSpawn playerSpawn;
    public float TimeGap = 1f;
    public bool GameStarted = false;
    public bool IsServer;




    IEnumerator StartGameTransition()
    {
        if (FindObjectOfType<BgmScript>() != null)
        {
            FindObjectOfType<BgmScript>().VolDec();
        }
        GameStarted = true;
        yield return new WaitForSeconds(TimeGap);
        if (IsServer == true)
        {
            playerSpawn.ClientsReady();
        }
        GetComponent<Animator>().SetTrigger("ShiftLeft");
        
    }

}
