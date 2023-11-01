using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Vars")]
    [SerializeField]
    float speed = 2f;

    //vars for jumping physics
    [SerializeField]
    float jumpPowerMin, jumpPowerMax, gravityScale, gravityFall, castDist;

    //ref to this object's rigidbody
    [Header("Components")]
    public Rigidbody2D myBody;

    //for checking and then reseting the player's position
    [Header("Reset Transforms")]
    [SerializeField]
    Transform resetPos, bottomBounds;

    //vars for horiztontal input and movement
    float horizontalMove;

    //to see if we touched the ground
    bool grounded = false;

    float jumpPower;

    //for jumping input
    bool jump = false;

    //vars for juice envents
    JuiceEvents juiceEvents;

    //for juice events
    bool landed = false;
    public static bool dead = false;

    void Start()
    {
        jumpPower = jumpPowerMin;
        juiceEvents = GetComponent<JuiceEvents>();
    }

    void Update()
    {
        if(!dead)
        {
            CheckHInput();
            CheckVInput();
        }
    }

    //checks if we're pressing L/R arrow keys and sets our movement var
    void CheckHInput()
    {
        horizontalMove = Input.GetAxis("Horizontal");
    }

    //check if we're pressing the jump button, using press length to determine jumping power
    void CheckVInput()
    {
        if (Input.GetButton("Jump") && grounded)
        {
            juiceEvents.StartJumpJuice();
            if (jumpPower <= jumpPowerMax)
            {
                jumpPower += 80 * Time.deltaTime;
            }
            else
            {
                jump = true;
            }
        }
        else if (Input.GetButtonUp("Jump") && grounded)
        {
            jump = true;
        }
    }

    //check if we've fallen out of bounds
    public void StartReset(CinemachineImpulseSource source)
    {
        StopPhysics();
        dead = true;
        juiceEvents.FallDieJuiceStart(source);
    }

    //stops our velocity
    public void StopPhysics()
    {
        myBody.gravityScale = 1f;
        myBody.velocity = new Vector3(0f, 0f, 0f);
    }

    //resets our position
    public void ResetPos()
    {
        dead = false;
        SceneManager.LoadScene("Game");
        //transform.position = resetPos.position;
    }

    //fixed update runs at a fixed rate
    //it is the event where Unity runs physics calculations
    void FixedUpdate()
    {
        if(!dead)
        {
            float moveSpeed = HMove();

            StartJump();

            VMove();

            GroundCheck();

            //actually move my players
            myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        }
    }

    //set how fast we should move on the x axis this frame
    float HMove()
    {
        juiceEvents.HMoveJuice(horizontalMove);
        return horizontalMove * speed;
    }

    //sets gravity based on which direction we're moving
    void VMove()
    {
        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
           // CheckReset();
        }
    }

    //starts jump physics
    void StartJump()
    {
        if (jump)
        {
            juiceEvents.JumpJuice(horizontalMove);
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
            landed = false;
            jumpPower = jumpPowerMin;
        }
    }

    //checks if we're touching the ground
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if (hit.collider != null && hit.transform.tag == "Ground")
        {
            if (!landed && !grounded)
            {

                juiceEvents.LandJuice(horizontalMove);
                landed = true;
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
