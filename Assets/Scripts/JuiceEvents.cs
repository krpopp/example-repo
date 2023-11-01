using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JuiceEvents : MonoBehaviour
{

    float lastDir = 0f;

    float fallDeathCounter;
    bool fallDead = false;

    float enemyDeathCounter;
    bool enemyDead = false;

    PlayerControl playerControl;
    StudentJuiceEvents studentJuice;

    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        studentJuice = GetComponent<StudentJuiceEvents>();
        fallDeathCounter = studentJuice.fallTimeToWait;
        enemyDeathCounter = studentJuice.enemyTimeToWait;
    }

    private void Update()
    {
        if(fallDead)
        {
            FallDieJuice();
        }
        if(enemyDead)
        {
            EnemyDieJuice();
        }
    }

    //called whent the player presses the jump button
    public void StartJumpJuice()
    {
        studentJuice.StartJumpAnim();
    }

    //called when jump addforce is called
    public void JumpJuice(float dir)
    {
        studentJuice.trailRenderer.emitting = true;
    }

    //called when the player first touches the ground
    public void LandJuice(float dir)
    {
        studentJuice.StartLandAnim();
        studentJuice.trailRenderer.emitting = false;
        studentJuice.landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
        studentJuice.landDust.Play();
    }

    //calls whenever the player is moving
    public void HMoveJuice(float dir)
    {
        if(lastDir != Mathf.Sign(dir))
        {
            studentJuice.landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
            studentJuice.changeDirDust.Play();
        }
        lastDir = Mathf.Sign(dir);
    }   
    
    //called when the player is reset by falling out of the level
    //starts a counter to see when the player should reset their positon
    public void FallDieJuiceStart(CinemachineImpulseSource source)
    {
        studentJuice.DoCamShake(source);
        fallDead = true;
    }

    //counts down after the player has died
    //when the counter hits 0, reset the player
    void FallDieJuice()
    {
        fallDeathCounter -= Time.deltaTime;
        if(fallDeathCounter <= 0)
        {
            studentJuice.EndFallDieAnim();
            playerControl.ResetPos();
            fallDeathCounter = studentJuice.fallTimeToWait;
            fallDead = false;
        }
    }

    //called when the player is reset by the enemy
    public void EnemyDieJuiceStart(CinemachineImpulseSource source)
    {
        studentJuice.StartEnemyDeathAnim();
        studentJuice.DoCamShake(source);
        enemyDead = true;
    }

    //counts down after the player has died
    //when the counter hits 0, reset the player
    void EnemyDieJuice()
    {
        enemyDeathCounter -= Time.deltaTime;
        if(enemyDeathCounter <= 0)
        {
            studentJuice.EndEnemyDieAnim();
            playerControl.ResetPos();
            enemyDeathCounter = studentJuice.enemyTimeToWait;
            enemyDead = false;
        }
    }

}
