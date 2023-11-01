using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCollide : MonoBehaviour
{
    JuiceEvents juiceEvents;
    PlayerControl playerControl;

    [SerializeField]
    GameObject canvas;

    private void Start()
    {
        juiceEvents = GetComponent<JuiceEvents>();
        playerControl = GetComponent<PlayerControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !PlayerControl.dead)
        {
            PlayerControl.dead = true;
            playerControl.StopPhysics();
            juiceEvents.EnemyDieJuiceStart(collision.gameObject.GetComponent<CinemachineImpulseSource>());
        }
        if(collision.gameObject.name == "Goal")
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bottom Bounds")
        {
            playerControl.StartReset(collision.gameObject.GetComponent<CinemachineImpulseSource>());
        }
    }
}
