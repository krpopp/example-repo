using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StudentJuiceEvents : MonoBehaviour
{

    [Header("Start Jump Juice")]
    public ParticleSystem jumpDust; //emits when the player first jumps
    public TrailRenderer trailRenderer; //emits when the player is falling/moving

    [Header("Land Juice")]
    public ParticleSystem landDust; //emits when the player first lands

    [Header("Grounded Movement Juice")]
    public ParticleSystem changeDirDust; //emits when the player changes H direction

    [Header("Falling Death Juice")]
    public float fallTimeToWait; //how long to reset after hitting the bottom bounds

    [Header("Enemy Death Juice")]
    public float enemyTimeToWait; //how long to wait after hitting an enemy
    public float camPower; //how much the camera should shake

    Animator juiceAnim;

    PlayerControl playerControl;

    private void Start()
    {
        juiceAnim = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
    }

    //called when the player reaches a certain y velocity (starts a powerful jump)
    public void StartJumpAnim()
    {
        juiceAnim.SetFloat("doStretch", playerControl.myBody.velocity.y);
    }

    //called when the player touches the ground the first time
    public void StartLandAnim()
    {
        juiceAnim.SetFloat("doStretch", 0);
    }

    //called when the player first touches the player
    public void StartEnemyDeathAnim()
    {
        juiceAnim.SetFloat("doStretch", 0);
        juiceAnim.SetBool("flashColor", true);
    }

    public void DoCamShake(CinemachineImpulseSource source)
    {
        source.GenerateImpulseWithForce(camPower);
    }

    //called when the player first touches an enemy
    public void EndEnemyDieAnim()
    {
        juiceAnim.SetBool("flashColor", false);
    }

    //called when a player first hits the bottom bounds
    public void EndFallDieAnim()
    {

    }

}
