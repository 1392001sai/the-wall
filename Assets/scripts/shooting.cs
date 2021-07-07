using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class shooting : NetworkBehaviour
{

    public float ReloadTime;
    Touch aimtouch;
    Vector3 touchpos;
    public GameObject projectile;
    public GameObject barrel;
    public GameObject pivot;
    [SyncVar]
    float speed;
    public float speedFactor;
    public Animator PlayerAnim;
    Vector3 aimVector;
    public PlayerData playerData;
    int chkflip;
    public GameObject StunShield;
    //[SyncVar(hook = "OnChangeHealth")]
    public float MaxHealth;
    int validTouch = 0;
    public HealthBarScript healthBarScript;
    public PlayerSpawn Spawnner;
    [SyncVar]
    public bool ShieldActive;
    public GameObject BazookaSmoke;
    public GameObject BazookaSmokePoint;
    public GameObject DamageIndicator;
    ScoreScript scoreScript;
    public float KnockBackAmount;
    public float KnockBackTime;
    public GameObject handl;
    Transform initHandl;
    public Transform HealParticlesSpawnPoint;
    public GameObject HealParticles;
    Transform LocalPlayer;
    public GameObject[] Sounds;
    GameObject sound;



    public override void OnStartAuthority()
    {


        //playerData = GetComponent<PlayerData>();
        playerData.IsReloading = false;
        playerData.IsAiming = false;
        LocalPlayer = gameObject.transform;
        foreach(shooting shootingObj in FindObjectsOfType<shooting>())
        {
            shootingObj.LocalPlayer = LocalPlayer;
        }
        CmdUpdateVar(speed, playerData.IsReloading, playerData.IsAiming);
    }

    private void Start()
    {
        chkflip = ChkFlip();
        healthBarScript.SetMaxHealth(MaxHealth);
        ShieldActive = false;
        scoreScript = GameObject.FindWithTag("ScoreScreen").GetComponent<ScoreScript>();
    }

    void Update()
    {
        if (hasAuthority == true)
        {
            if (playerData.IsReloading == false && playerData.IsStunned == false)
            {
                aim();
                
            }
            
        }
        PlayerAnim.SetBool("IsAiming", playerData.IsAiming);
    }

    void aim()
    {
        validTouch = 0;
        for (int i = 0; i < Input.touchCount; i++)
        {      
            if (ValidTouchChk(i) == true)
            {
                validTouch = 1;
                aimtouch = Input.GetTouch(i);
                break;
            }
        }

        if (validTouch == 1)
        {
            touchpos.x = aimtouch.position.x;
            touchpos.y = aimtouch.position.y;
            touchpos.z = Camera.main.transform.position.z;
            touchpos = Camera.main.ScreenToWorldPoint(touchpos);
            touchpos.z = 0;
            aimVector = touchpos - pivot.transform.position;
            //Debug.Log(3);
            if (Vector3.SignedAngle(transform.right * chkflip, aimVector, Vector3.forward) * chkflip < 10 || Vector3.SignedAngle(transform.right * chkflip, aimVector, Vector3.forward) * chkflip > 95)//95 for checking purposes
            {
                validTouch = 0;
            }
            else
            {
                playerData.IsAiming = true;
                
                pivot.transform.right = aimVector * chkflip;
                Debug.Log(2);
                speed = Vector3.Magnitude(aimVector);
                CmdUpdateVar(speed, playerData.IsReloading, playerData.IsAiming);
            }
        }
        if (validTouch == 0 && playerData.IsAiming == true)
        {
            shoot();

        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(touchpos, pivot.transform.position);

    }
    void shoot()
    {
        CmdUpdateVar(speed, playerData.IsReloading, playerData.IsAiming);
        CmdProjectile();
        playerData.IsAiming = false;
        playerData.IsReloading = true;
        CmdUpdateVar(speed, playerData.IsReloading, playerData.IsAiming);
        StartCoroutine(Reload());

    }
    [Command]
    void CmdProjectile()
    {
        //Debug.Log(chkflip);
        GameObject p = Instantiate(projectile, barrel.transform.position, Quaternion.FromToRotation(transform.right, barrel.transform.right * chkflip));
        GameObject bs;
        if (chkflip == 1)
        {
            bs = Instantiate(BazookaSmoke, BazookaSmokePoint.transform.position, Quaternion.Euler(barrel.transform.rotation.eulerAngles.z + 90,-90,-90));
        }
        else
        {
            bs = Instantiate(BazookaSmoke, BazookaSmokePoint.transform.position, Quaternion.Euler(180 - barrel.transform.rotation.eulerAngles.z + 90, -90, -90));
        }
        p.GetComponent<AlignToPath>().StartAlign = true;
        p.GetComponent<MissileProp>().LocalPlayer = LocalPlayer;
        p.GetComponent<MissileProp>().StartSounds();
        //p.GetComponent<AlignToPath>().homingMissileScript.MyTeam = playerData.team;
        p.GetComponent<Rigidbody2D>().velocity = barrel.transform.right * speed * speedFactor * chkflip;
        //Debug.Log("shooting " + chkflip);
        NetworkServer.Spawn(p);
        NetworkServer.Spawn(bs);
        RpcSetProjectile(p.GetComponent<NetworkIdentity>().netId, barrel.transform.right);

    }

    [ClientRpc]
    void RpcSetProjectile(NetworkInstanceId pid, Vector3 dir)
    {
        if (isServer != true)
        {
            GameObject p = ClientScene.FindLocalObject(pid);
            if (p == null)
            {
                Debug.Log("p not found");
            }
            p.GetComponent<MissileProp>().LocalPlayer = LocalPlayer;
            p.GetComponent<MissileProp>().StartSounds();
            p.GetComponent<AlignToPath>().StartAlign = true;
            //p.GetComponent<AlignToPath>().homingMissileScript.MyTeam = playerData.team;
            p.GetComponent<Rigidbody2D>().velocity = dir * speed * speedFactor * chkflip;
            Debug.Log("shooting " + chkflip);
        }
    }



    int ChkFlip()
    {

        if (playerData.team == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }

    }

    [Command]
    void CmdUpdateVar(float ShootSpeed, bool IsReloading, bool IsAiming)
    {
        speed = ShootSpeed;
        playerData.IsReloading = IsReloading;
        playerData.IsAiming = IsAiming;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(ReloadTime);
        playerData.IsReloading = false;
        CmdUpdateVar(speed, playerData.IsReloading, playerData.IsAiming);

    }
    /*public void UnstunCaller(float StunTime)
    {
        StartCoroutine(Unstun(StunTime));
    }
    IEnumerator Unstun(float StunTime)
    {
        float initrotz;
        initrotz = pivot.transform.rotation.eulerAngles.z;
        pivot.transform.rotation = Quaternion.Euler(0, 0, 19.659f);
        yield return new WaitForSeconds(StunTime);
        pivot.transform.rotation = Quaternion.Euler(0, 0, initrotz);
        playerData.IsStunned = false;
        CmdUpdateVar(speed, chkflip, playerData.IsReloading, playerData.IsAiming, playerData.IsStunned);
    }*/

    public void Stunned()
    {
        pivot.transform.rotation = Quaternion.Euler(0, 0, 19.659f);
        initHandl = handl.transform;
        playerData.IsStunned = true;
        playerData.IsMoving = false;
        GetComponent<Animator>().SetTrigger("IsStunned");
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        float curTime = 0;
        Vector3 initialPos = transform.position, finalPos;
        finalPos = initialPos;
        if (playerData.team == 0)
        {
            finalPos.x -= KnockBackAmount;
        }
        else
        {
            finalPos.x += KnockBackAmount;
        }
        while (curTime <= KnockBackTime)
        {
            transform.position = Vector3.Lerp(initialPos, finalPos, curTime/KnockBackTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        handl.transform.position = initHandl.position;
        handl.transform.rotation = Quaternion.identity;
        playerData.IsStunned = false;
    }



    bool ValidTouchChk(int i)
    {
        if (!(Input.GetTouch(i).position.x > 2 * Screen.width / 3 && Input.GetTouch(i).position.y < Screen.height / 4) && playerData.team == 0)
        {
            return true;
        }
        else if (!(Input.GetTouch(i).position.x < 1 * Screen.width / 3 && Input.GetTouch(i).position.y < Screen.height / 4) && playerData.team == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplyStunShield(float StunShieldTime)
    {
        if (ShieldActive == false)
        {
            RpcPowerUpAnim();
            ShieldActive = true;
            GameObject s = Instantiate(StunShield, transform.position, Quaternion.identity, transform);
            NetworkServer.Spawn(s);
            RpcSetShield(s.GetComponent<NetworkIdentity>().netId);
            //StartCoroutine(DestroyStunShield(StunShieldTime, s));
        }
        RpcPowerUpSound();
    }

    [ClientRpc]
    void RpcPowerUpAnim()
    {
        DamageIndicator.GetComponent<Animator>().SetTrigger("ShieldPowerUp");
    }

    [ClientRpc]

    void RpcPowerUpSound()
    {
        sound = Instantiate(Sounds[2], transform.position, Quaternion.identity);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
    }

    [ClientRpc]
    void RpcSetShield(NetworkInstanceId ShieldId)
    {
        GameObject s = ClientScene.FindLocalObject(ShieldId);
        s.GetComponent<StunShieldScipt>().IsServer = isServer;
        s.GetComponent<StunShieldScipt>().PlayerConnection = this;
        s.transform.position = transform.position;
        if (s == null)
        {
            Debug.Log("s not found");
        }
        s.transform.parent = transform;
        //Debug.Log(s.transform.parent.name);
    }

    public void ApplyHealthPowerUp(float HealAmount)
    {
        //Debug.Log(1);
        sound = Instantiate(Sounds[2], transform.position, Quaternion.identity);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
        //health += HealAmount;
        healthBarScript.Heal(HealAmount);
        Instantiate(HealParticles, HealParticlesSpawnPoint);
        DamageIndicator.GetComponent<Animator>().SetTrigger("HealPowerUp");
    }


    /*IEnumerator DestroyStunShield(float StunShieldTime, GameObject s)
    {
        
        yield return new WaitForSeconds(StunShieldTime);
        Destroy(s);
        ShieldActive = false;
    }*/

    [Command]
    public void CmdReduceHealth(float damage)
    {
        RpcUpdateHealth(damage);
    }

    /*void OnChangeHealth(float hp)
    {
        Debug.Log("health " + hp);
        healthBarScript.UpdateHealth(hp);
    }*/

    [ClientRpc]
    void RpcUpdateHealth(float damage)
    {
        sound = Instantiate(Sounds[0], transform.position, Quaternion.identity);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
        healthBarScript.CurHealth -= damage;
        if (healthBarScript.CurHealth > 0)
        {
            DamageIndicator.GetComponent<Animator>().SetTrigger("IsHit");
        }
        else
        {
            DamageIndicator.GetComponent<Animator>().SetTrigger("IsDead");
        }
        if (hasAuthority == true)
        {
            Camera.main.GetComponent<CameraShaker>().CameraShake();
        }
        Debug.Log("health " + healthBarScript.CurHealth + isServer);
        healthBarScript.UpdateHealth();
    }

    public void Death()
    {
        sound = Instantiate(Sounds[1], transform.position, Quaternion.identity);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
        if (isServer == true)
        {
            
            Debug.Log("server called");
            InreaseScore(playerData.team);
            Spawnner.RespawnCaller(playerData.team, playerData.PlayerType);
            //increase score
            Destroy(gameObject);
        }
    }

    public void InreaseScore(int team)
    {
        if (team == 1)
        {
            scoreScript.RpcIncreaseTeam0Score();
        }
        if (team == 0)
        {
            scoreScript.RpcIncreaseTeam1Score();
        }
    }







}
