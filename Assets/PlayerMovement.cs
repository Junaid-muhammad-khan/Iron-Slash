using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;
    bool  crouch = false;

    private int uiMoveDirection = 0; // -1 for left, 1 for right, 0 for idle


    void Start()
{   
    //Debug.Log("Subscribed to OnLandEvent");
    controller.OnLandEvent.AddListener(OnLanding);
}
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal") + uiMoveDirection;
        horizontalMove = Mathf.Clamp(input, -1f, 1f) * runSpeed;


        animator.SetFloat("Speed" , Mathf.Abs(horizontalMove));

        if (Input.GetButton("Jump")) 
        {
         jump = true;
         animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch")) 
        {
         crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

        //if (Input.GetMouseButtonDown(0)) // Left-click
        //{
           // animator.SetTrigger("Attack");
        //}
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime,crouch ,jump);
        jump = false;
    }

    public void OnLanding() {
        
        //Debug.Log("Landed — resetting IsJumping");
        animator.SetBool("IsJumping", false);
    }

    public void OnMoveLeftButtonDown() {
    uiMoveDirection = -1;

    }

    public void OnMoveRightButtonDown() {
    uiMoveDirection = 1;
    }

    public void OnMoveButtonUp() {
    uiMoveDirection = 0;
    }
    public void JumpButtonPressed() {
    if (!jump) {
        jump = true;
        animator.SetBool("IsJumping", true);
    }
}
    public void AttackButtonPressed() {
    animator.SetTrigger("Attack");
}
}


