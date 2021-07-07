using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class PlayerSpawn : NetworkBehaviour
{

    public GameObject[] Player1;
    public GameObject[] Player2;
    public GameObject[] PosTeam1;
    public GameObject[] PosTeam2;
    public GameObject hudr;
    public GameObject hudl;
    public GameObject parachuteDeployer;
    public GameObject ScoreScreen;
    public int team = 0;
    public float RespawnTime;
    GameObject go;
    ScoreScript scoreScript;
    TeamSelectScript teamSelectScript;
    [SyncVar]
    int Team0PlayerType = 0;
    [SyncVar]
    int Team1PlayerType = 0;

    
    public override void OnStartLocalPlayer()
    {
        FindObjectOfType<NetworkSetup>().localPlayer = this;
        teamSelectScript = FindObjectOfType<TeamSelectScript>();
        team = teamSelectScript.team;
        //Debug.Log(1);
        CmdUpdateNumberOfPlayers(team, isServer);
        FindObjectOfType<StartGame>().IsServer = isServer;
        
        if (isServer == true)
        {

            FindObjectOfType<StartGame>().playerSpawn = this;
            FindObjectOfType<WaitForClientsScript>().playerSpawn = this;
        }
        else
        {
            FindObjectOfType<NetworkSetup>().SetUpSuccess();
        }
    }

    [Command]
    void CmdUpdateNumberOfPlayers(int PlayerTeam, bool server)
    {
        NetworkSetup ServerSetup = FindObjectOfType<NetworkSetup>();
        if (PlayerTeam == 0)
        {
            //Debug.Log(7);
            ServerSetup.Team0No++;
            
           
        }
        else
        {
            ServerSetup.Team1No++;
            
        }
        
        //RpcSuccess();
    }

    [Command]

    private void CmdTeamUpdated(int PlayerTeam, int team)
    {
        NetworkSetup networkSetup = FindObjectOfType<NetworkSetup>();
        if (team != PlayerTeam)
        {
            team = PlayerTeam;
            if (PlayerTeam == 0)
            {
                networkSetup.Team0No++;
                networkSetup.Team1No--;
            }
            else
            {
                networkSetup.Team0No--;
                networkSetup.Team1No++;
            }
        }
    }

    //[ClientRpc]

    /*void RpcError()
    {
        Debug.Log("rpc");
        if (hasAuthority == true)
        {
            FindObjectOfType<NetworkSetup>().SetUpFailed();
        }
    }*/
    /*[ClientRpc]

    void RpcSuccess()
    {
        if (hasAuthority == true)
        {
            FindObjectOfType<NetworkSetup>().SetUpSuccess();
        }
    }*/

    public void UpdateTeam(int PlayerTeam)
    {
        CmdTeamUpdated(PlayerTeam, team);
        team = PlayerTeam;
    }
    public void setUpSpawn()
    {
        //Debug.Log(3);
        if (isLocalPlayer == true)
        {
            
            
            //Debug.Log(2);
            //FindObjectOfType<WaitForClientsScript>().playerSpawn = this;
            
            if (isServer == true)
            {
                //Debug.Log("TEAM0=" + FindObjectOfType<NetworkSetup>().Team0No);
                //Debug.Log("TEAM1=" + FindObjectOfType<NetworkSetup>().Team1No);
                GameObject s = Instantiate(ScoreScreen);
                scoreScript = s.GetComponent<ScoreScript>();
                NetworkServer.Spawn(s);
                RpcSetGameTime(s.GetComponent<NetworkIdentity>().netId, FindObjectOfType<NetworkSetup>().GameTime);
                GameObject pd = Instantiate(parachuteDeployer);
                NetworkServer.Spawn(pd);
                         
                
            }
            teamSelectScript.gameObject.SetActive(false);

            CmdSpawn(team);
            if (team == 0)
            {

                Instantiate(hudl);
            }
            if (team == 1)
            {

                Instantiate(hudr);
            }

        }
    }

    [Command]
    void CmdSpawn(int ClientTeam)
    {

        if (ClientTeam == 1)
        {
            go = Instantiate(Player2[Team1PlayerType], new Vector3(Random.Range(PosTeam2[0].transform.position.x, PosTeam2[1].transform.position.x), PosTeam2[0].transform.position.y, 0), Quaternion.identity);
            go.GetComponent<PlayerData>().PlayerType = Team1PlayerType;
            IncPlayerType1();
        }
        else
        {
            go = Instantiate(Player1[Team0PlayerType], new Vector3(Random.Range(PosTeam1[0].transform.position.x, PosTeam1[1].transform.position.x), PosTeam1[0].transform.position.y, 0), Quaternion.identity);
            go.GetComponent<PlayerData>().PlayerType = Team1PlayerType;
            IncPlayerType0();
        }
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);    
        go.GetComponent<PlayerData>().team = ClientTeam;
        go.GetComponent<shooting>().Spawnner = this;


    }

    [Command]
    void CmdReSpawn(int ClientTeam, int respawnType)
    {
        if (ClientTeam == 1)
        {
            go = Instantiate(Player2[respawnType], new Vector3(Random.Range(PosTeam2[0].transform.position.x, PosTeam2[1].transform.position.x), PosTeam2[0].transform.position.y, 0), Quaternion.identity);
            go.GetComponent<PlayerData>().PlayerType = respawnType;
        }
        else
        {
            go = Instantiate(Player1[respawnType], new Vector3(Random.Range(PosTeam1[0].transform.position.x, PosTeam1[1].transform.position.x), PosTeam1[0].transform.position.y, 0), Quaternion.identity);
            go.GetComponent<PlayerData>().PlayerType = respawnType;
        }
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        go.GetComponent<PlayerData>().team = ClientTeam;
        go.GetComponent<shooting>().Spawnner = this;


    }


    public void RespawnCaller(int ClientTeam, int respawnType)
    {
        StartCoroutine(Respawn(ClientTeam, respawnType));
    }
    IEnumerator Respawn(int ClientTeam, int respawnType)
    {
        yield return new WaitForSeconds(RespawnTime);
        CmdReSpawn(ClientTeam, respawnType);
    }

    public void ClientsReady()
    {
        if (isServer == true)
        {
            RpcServerStarted();
        }
    }

    [ClientRpc]
    void RpcServerStarted()
    {
        GameObject.FindWithTag("TransitionPanel").GetComponent<Animator>().SetTrigger("ShiftLeft");
        GameObject.FindWithTag("SetUpScreen").GetComponent<NetworkSetup>().localPlayer.setUpSpawn();

    }

    void IncPlayerType0()
    {
        foreach(PlayerSpawn playerSpawn in GameObject.FindObjectsOfType<PlayerSpawn>())
        {

            //Debug.Log(0);
            playerSpawn.Team0PlayerType++;
        }
    }
    void IncPlayerType1()
    {
        foreach (PlayerSpawn playerSpawn in GameObject.FindObjectsOfType<PlayerSpawn>())
        {
            Debug.Log(1);
            playerSpawn.Team1PlayerType++;
        }
    }



    [ClientRpc]
    public void RpcTransition()
    {
        GameObject.FindWithTag("TransitionPanel").GetComponent<Animator>().SetTrigger("ShiftRight");
    }

    [ClientRpc]
    void RpcSetGameTime(NetworkInstanceId ScoreScreenId, int GameTime)
    {
        //Debug.Log(GameTime);
        GameObject s = ClientScene.FindLocalObject(ScoreScreenId);
        s.GetComponent<TimerScript>().TimerTime = GameTime;
    }



}
