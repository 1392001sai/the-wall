using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]

public class ParachuteDeployer : NetworkBehaviour
{
    public GameObject[] Team1Position;
    public GameObject[] Team2Position;
    public GameObject Parachute;
    float startTime;
    public float DeployTime;
    //int team = 1;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (isServer == true)
        {
            if (Time.time - startTime > DeployTime)
            {
                Deploy();
                startTime = Time.time;
                /* if (team == 1)
                 {
                     team = 2;
                 }
                 if (team == 2)
                 {
                     team = 1;
                 }*/
            }
        }
    }
    void Deploy()
    {
        GameObject p1, p2; 
        p1 = Instantiate(Parachute, new Vector3(Random.Range(Team1Position[0].transform.position.x, Team1Position[1].transform.position.x), Team1Position[0].transform.position.y, 0), Quaternion.identity);
        p2 = Instantiate(Parachute, new Vector3(Random.Range(Team2Position[0].transform.position.x, Team2Position[1].transform.position.x), Team2Position[0].transform.position.y, 0), Quaternion.identity);
        NetworkServer.Spawn(p1);
        NetworkServer.Spawn(p2);
        int index1 = Random.Range(0, 2);
        int index2 = Random.Range(0, 2);
        RpcSetPowerUpIndex(p1.GetComponent<NetworkIdentity>().netId, p2.GetComponent<NetworkIdentity>().netId, index1, index2);
    }

    [ClientRpc]
    void RpcSetPowerUpIndex(NetworkInstanceId p1id, NetworkInstanceId p2id, int index1, int index2)
    {
        GameObject p1 = ClientScene.FindLocalObject(p1id);
        GameObject p2 = ClientScene.FindLocalObject(p2id);
        p1.GetComponent<Parachute>().powerUp.GetComponent<PowerUp>().SetIndex(index1);
        p2.GetComponent<Parachute>().powerUp.GetComponent<PowerUp>().SetIndex(index2);
    }
}
