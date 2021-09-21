using System;
using System.Collections;
using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets._2D {
  public class PlatformerCharacter2D : MonoBehaviour {
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private float m_WallPushBackForce = 1000f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField] private int m_MaxJumpsInARow = 2;
    [SerializeField] private float m_JumpDelay = .3f;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Transform m_WallCheck;
    const float k_WallCheckRadius = .5f;
    private bool m_TouchesWall = false;
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private int m_JumpCount = 0;
    private bool m_IsJumping = false;

    private void Awake() {
      // Setting up references.
      m_GroundCheck = transform.Find("GroundCheck");
      m_CeilingCheck = transform.Find("CeilingCheck");
      m_WallCheck = transform.Find("WallCheck");
      m_Anim = GetComponent<Animator>();
      m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
      m_Grounded = CheckCollision(m_GroundCheck, k_GroundedRadius);
      m_TouchesWall = CheckCollision(m_WallCheck, k_WallCheckRadius);

      m_Anim.SetBool("Ground", m_Grounded);

      // Set the vertical animation
      m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    public void UpdateMovemement(float move, bool crouch, bool jump) {
      Crouch(crouch);
      Move(move, crouch);
      Jump(jump);
    }

    private bool CheckCollision(Transform check, float radius) {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(check.position, radius, m_WhatIsGround);
      for (int i = 0; i < colliders.Length; i++) {
        if (colliders[i].gameObject != gameObject) {
          return true;
        }
      }

      return false;
    }

    private void Crouch(bool crouch) {
      // If crouching, check to see if the character can stand up
      if (!crouch && m_Anim.GetBool("Crouch")) {
        // If the character has a ceiling preventing them from standing up, keep them crouching
        if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
          crouch = true;
        }
      }

      // Set whether or not the character is crouching in the animator
      m_Anim.SetBool("Crouch", crouch);
    }

    private void Move(float move, bool crouch) {
      //only control the player if grounded or airControl is turned on
      if (m_Grounded || m_AirControl) {
        // Reduce the speed if crouching by the crouchSpeed multiplier
        move = (crouch ? move * m_CrouchSpeed : move);

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        m_Anim.SetFloat("Speed", Mathf.Abs(move));

        // Move the character
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight) {
          // ... flip the player.
          Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight) {
          // ... flip the player.
          Flip();
        }
      }
    }

    private void Jump(bool jump) {
      bool hasLanded = m_Grounded && m_Rigidbody2D.velocity.y <= 0;
      bool canJump = jump && !m_IsJumping && m_JumpCount < m_MaxJumpsInARow;

      if (canJump) {
        m_Grounded = false;
        m_Anim.SetBool("Ground", false);

        // Add a vertical force to the player.
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

        m_IsJumping = true;
        StartCoroutine(ResetIsJumpingAfterDelay());


        if (m_TouchesWall) {
          m_JumpCount = 0;
          m_Rigidbody2D.velocity = Vector2.zero;
          m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
          m_Rigidbody2D.AddForce(Vector2.right * m_JumpForce * 5);
        } else {
          m_JumpCount++;
        }
      } else if (hasLanded) {
        m_JumpCount = 0;
        m_IsJumping = false;
      }

    }

    private IEnumerator ResetIsJumpingAfterDelay() {
      yield return new WaitForSeconds(m_JumpDelay);
      m_IsJumping = false;
    }

    private void Flip() {
      // Switch the way the player is labelled as facing.
      m_FacingRight = !m_FacingRight;

      // Multiply the player's x local scale by -1.
      Vector3 theScale = transform.localScale;
      theScale.x *= -1;
      transform.localScale = theScale;
    }
  }
}
