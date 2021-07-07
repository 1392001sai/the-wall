using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class movement : NetworkBehaviour
{
    public float speed;
    movementButtons right;
    movementButtons left;
    public Animator PlayerAnim;
    public PlayerData playerData;
    public GameObject dust;
    public Transform dustPoint2;
    public Transform dustPoint1;
    public int dir;
    public Animator DamageIndicator;

    public override void OnStartAuthority()
    {
        DamageIndicator.SetTrigger("IsStart");
        dir = 0;
        right = GameObject.Find("Right").GetComponent<movementButtons>();
        left = GameObject.Find("Left").GetComponent<movementButtons>();
        playerData.IsMoving = false;
        CmdUpdateVar(playerData.IsMoving);
    }

    void Update()
    {
        if (hasAuthority == true)
        {
            if (playerData.IsStunned == false)
            {
                if (right.Pressed == true && left.Pressed == false)
                {
                    playerData.IsMoving = true;
                    CmdUpdateVar(playerData.IsMoving);
                    transform.Translate(speed * Time.deltaTime * transform.right);
                    if (dir != 1)
                    {
                        //GameObject d = Instantiate(dust, dustPoint2.position, dustPoint2.rotation);
                        //NetworkServer.Spawn(d);
                        dir = 1;
                    }
                    //transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y, transform.localScale.z);
                }
                else if (right.Pressed == false && left.Pressed == true)
                {
                    playerData.IsMoving = true;
                    CmdUpdateVar(playerData.IsMoving);
                    transform.Translate(-speed * Time.deltaTime * transform.right);
                    if (dir != -1)
                    {
                        //GameObject d = Instantiate(dust, dustPoint1.position, dustPoint1.rotation);
                        //NetworkServer.Spawn(d);
                        dir = -1;
                    }
                }
                else
                {
                    playerData.IsMoving = false;
                    CmdUpdateVar(playerData.IsMoving);
                    dir = 0;
                }
            }
            else
            {
                playerData.IsMoving = false;
                CmdUpdateVar(playerData.IsMoving);
            }
            

        }
        if (PlayerAnim.GetBool("IsMoving") != playerData.IsMoving)
        {
            PlayerAnim.SetBool("IsMoving", playerData.IsMoving);
        }
        //PlayerAnim.SetBool("IsStunned", playerData.IsStunned);


    }
    [Command]
    void CmdUpdateVar(bool IsMoving)
    {
        playerData.IsMoving = IsMoving;
    }


}
