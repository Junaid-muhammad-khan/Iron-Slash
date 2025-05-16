using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;
    bool  crouch = false;

    void Start()
{   
    //Debug.Log("Subscribed to OnLandEvent");
    controller.OnLandEvent.AddListener(OnLanding);
}
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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

        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            animator.SetTrigger("Attack");
        }
        
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
}
