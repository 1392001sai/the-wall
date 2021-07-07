using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class PlayerData : NetworkBehaviour
{
    [SyncVar]
    public int team;
    [SyncVar]
    public bool IsStunned;
    [SyncVar]
    public bool IsMoving;
    [SyncVar]
    public bool IsReloading;
    [SyncVar]
    public bool IsAiming;
    [SyncVar]
    public float health;
    [SyncVar]
    public int PlayerType;
    public string PlayerId;

    private void Start()
    {
        PlayerId = "player " + GetComponent<NetworkIdentity>().netId;
        transform.name = PlayerId;
    }

}
