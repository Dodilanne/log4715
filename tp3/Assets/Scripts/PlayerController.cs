using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  // Déclaration des constantes
  private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
  private static readonly Vector3 CameraPosition = new Vector3(10, 1, 0);
  private static readonly Vector3 InverseCameraPosition = new Vector3(-10, 1, 0);
  private AudioSource source;
  public AudioClip footstepsClip;
  public AudioClip jumpClip;

  // Déclaration des variables
  bool _Grounded { get; set; }
  bool _Flipped { get; set; }
  bool _TouchingWall { get; set; }
  Animator _Anim { get; set; }
  Rigidbody _Rb { get; set; }
  Camera _MainCamera { get; set; }

  // Valeurs exposées
  [SerializeField]
  float MoveSpeed = 5.0f;

  [SerializeField]
  float DashDistance = 3.0f;
  [SerializeField]
  bool canDash = true;
  bool dashing = false;
  int dashDirection = 0;

  [SerializeField]
  string dashInputAction = "Dash";

  [SerializeField]
  float JumpForce = 10f;
  [SerializeField]
  float WallJumpVerticalFactor = 10f;

  private ParticleSystem _dustParticles;

  [SerializeField]
  LayerMask WhatIsGround;

  [SerializeField]
  LayerMask WhatIsWall;

  // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
  void Awake() {
    _Anim = GetComponent<Animator>();
    _Rb = GetComponent<Rigidbody>();
    _MainCamera = Camera.main;
    source = gameObject.AddComponent<AudioSource>();
    _dustParticles = this.transform.Find("Dash Dust").gameObject.GetComponent<ParticleSystem>();
  }

  // Utile pour régler des valeurs aux objets
  void Start() {
    _Grounded = false;
    _Flipped = false;
    _TouchingWall = false;
  }

  // Vérifie les entrées de commandes du joueur
  void Update() {
    var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
    if (dashing) {
      dashing = false;
      horizontal = dashDirection * DashDistance;

      _dustParticles.Play();

      RaycastHit hit;
      Ray ray = new Ray(this.transform.position, new Vector3(0, 0, dashDirection));
      bool hasHit = Physics.Raycast(ray, out hit, DashDistance);

      this.transform.position = hasHit ? hit.point : this.transform.position + new Vector3(0, 0, dashDirection * DashDistance);
    } else {
      HorizontalMove(horizontal);
    }

    FlipCharacter(horizontal);
    CheckInput();
  }

  // Gère le mouvement horizontal 
  void HorizontalMove(float horizontal) {
    _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
    _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
  }

  // Gère le saut du personnage, ainsi que son animation de saut
  void CheckInput() {
    if (_Grounded) {
      if (Input.GetButtonDown("Jump")) {
        _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        _Grounded = false;
        _Anim.SetBool("Grounded", false);
        _Anim.SetBool("Jump", true);
        if (jumpClip != null) {
          source.PlayOneShot(jumpClip, 0.5f);
        } else Debug.Log("missing jump clip");
      }
      // else if(_Rb.velocity.magnitude > 1)
      // {
      //   if (footstepsClip!=null) {
      //     delay();
      //     source.PlayOneShot(footstepsClip, 0.5f);
      //   }
      //   else Debug.Log("missing footsteps clip");
      // }
    } else if (_TouchingWall) {
      if (Input.GetButtonDown("Jump")) {

        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        var direction = (horizontal < 0) ? 1 : -1;
        _Rb.velocity = new Vector3(0, 0, 0);
        _Rb.AddForce(new Vector3(0, JumpForce * 0.5f * WallJumpVerticalFactor, 0), ForceMode.Impulse);
        _Rb.AddForce(new Vector3(0, 0, direction * JumpForce * MoveSpeed * 2f), ForceMode.Impulse);
        _Anim.SetBool("Jump", true);

        FlipCharacter(direction);
      }
    }

    if (Input.GetButtonDown(dashInputAction) && CanDash()) {
      dashing = true;
      dashDirection = Input.GetAxis("Horizontal") < 0 ? -1 : 1;
      Physics.IgnoreLayerCollision(7, 8);
    }
  }

  bool CanDash() {
    return canDash && !dashing && Input.GetAxis("Horizontal") != 0;
  }

  // Gère l'orientation du joueur et les ajustements de la camera
  void FlipCharacter(float horizontal) {
    if (horizontal < 0 && !_Flipped) {
      _Flipped = true;
      transform.Rotate(FlipRotation);
      _MainCamera.transform.Rotate(-FlipRotation);
      _MainCamera.transform.localPosition = InverseCameraPosition;
    } else if (horizontal > 0 && _Flipped) {
      _Flipped = false;
      transform.Rotate(-FlipRotation);
      _MainCamera.transform.Rotate(FlipRotation);
      _MainCamera.transform.localPosition = CameraPosition;
    }
  }

  // Collision avec le sol
  void OnCollisionEnter(Collision coll) {
    // On s'assure de bien être en contact avec le sol
    if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
      return;

    // Évite une collision avec le plafond
    if (coll.relativeVelocity.y > 0) {
      _Grounded = true;
      _Anim.SetBool("Grounded", _Grounded);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Wall") {
      _TouchingWall = true;
    }
  }

  void OnTriggerExit(Collider other) {
    if (other.tag == "Wall") {
      _TouchingWall = false;
    }
  }
}