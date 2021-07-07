using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class DeathScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Blood;
    //public GameObject BloodSpill;
    public float Groundy;
    public Transform BloodSpawnPoint;

    void BloodSpatter()
    {
        GameObject b = Instantiate(Blood, BloodSpawnPoint.position, Quaternion.identity);
        Player.GetComponent<shooting>().Death();
    }

    void BloodSpillSpawn()
    {
        //Instantiate(BloodSpill, new Vector3(BloodSpawnPoint.position.x, Groundy, 0), Quaternion.identity);
    }
}
