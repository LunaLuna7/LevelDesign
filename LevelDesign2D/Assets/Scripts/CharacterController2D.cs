using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float m_AirJumps;                                  //How many Air jumps the player has.
    [SerializeField] private bool m_DynamicJump;
    [SerializeField] private float m_FallGravity = 7f;                          //Falling speed
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private float m_AirJumpsLeft;   //Keep track of airJumps

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_AirJumpsLeft = m_AirJumps;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump)
    {
      
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            // Reset AirJumps

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        JumpGravity(jump);

        if (jump)
        {

            // Ground Jump
            if (m_Grounded)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            //Air Jump
            else if (!m_Grounded && m_AirJumpsLeft > 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                m_AirJumpsLeft--;
            }
        }
        
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void JumpGravity(bool jump)
    {
        if (jump && m_AirJumpsLeft >= 1)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
        }

        if (m_Rigidbody2D.velocity.y < 0) //we are falling
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;



        else if (m_DynamicJump && (m_Rigidbody2D.velocity.y > 0) && !Input.GetButton("Jump"))
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        
    }

    public void ResetJumps()
    {
        m_AirJumpsLeft = m_AirJumps;
    }
}