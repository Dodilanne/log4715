using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
  [RequireComponent(typeof(PlatformerCharacter2D))]
  public class Platformer2DUserControl : MonoBehaviour {
    [SerializeField] float m_MinJumpMultiplier = 1f;
    [SerializeField] float m_MaxJumpMultiplier = 3f;

    private PlatformerCharacter2D m_Character;

    private bool m_WasJumpPressed;
    private bool m_wasJumpReleased;

    private bool m_IsJumpPressed;
    private bool m_IsCrouchPressed;

    private float m_ChargeStartTime = -1f;

    private void Awake() {
      m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update() {
      bool isJumpPressed = Input.GetButton("Jump");
      bool isCrouchPressed = Input.GetKey(KeyCode.LeftControl);

      if (!m_WasJumpPressed) {
        m_WasJumpPressed = Input.GetButtonDown("Jump");
      }

      if (!m_wasJumpReleased && m_IsJumpPressed) {
        m_wasJumpReleased = !isJumpPressed || (m_IsCrouchPressed && !isCrouchPressed);
      }

      m_IsJumpPressed = isJumpPressed;
      m_IsCrouchPressed = isCrouchPressed;
    }

    private void FixedUpdate() {
      // Read the inputs.
      bool isCharging = m_IsCrouchPressed && m_IsJumpPressed;
      float movement = isCharging ? 0f : CrossPlatformInputManager.GetAxis("Horizontal");
      float jumpMultiplier = GetJumpMultiplier(isCharging);

      // Pass all parameters to the character control script.
      m_Character.UpdateMovemement(movement, m_IsCrouchPressed, m_WasJumpPressed, m_wasJumpReleased, Math.Min(m_MaxJumpMultiplier, jumpMultiplier));
      m_WasJumpPressed = false;
      m_wasJumpReleased = false;
    }

    private float GetJumpMultiplier(bool isCharging) {
      float jumpMultiplier = m_MinJumpMultiplier;

      if (isCharging && m_ChargeStartTime < 0) {
        m_ChargeStartTime = Time.time;
      } else if (m_ChargeStartTime >= 0 && m_wasJumpReleased) {
        float timeSinceCharge = Time.time - m_ChargeStartTime;
        if (timeSinceCharge >= Math.Max(1f, m_MinJumpMultiplier)) {
          jumpMultiplier = timeSinceCharge;
        }

        m_ChargeStartTime = -1;
      }

      return jumpMultiplier;
    }
  }
}
