using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  // Serialized attributes
  [SerializeField] LayerMask WhatIsGround;
  [SerializeField] float ShootSpeed = 2f;

  // Private attributes
  private Shooter _shooter;
  private GameController _Game;
  bool _Grounded { get; set; }
  Animator _Anim { get; set; }
  Shooter _Shooter { get; set; }
  private bool _IsAttacking = false;
  private bool _isDead = false;

  private AudioSource source;
  public AudioClip deathClip;

  private IEnumerator _enableShooting() {
    yield return new WaitForSeconds(ShootSpeed);
    _IsAttacking = false;
  }

  void Awake() {
    _shooter = GetComponent<Shooter>();
    _Anim = GetComponent<Animator>();
    _Game = GameObject.FindObjectOfType<GameController>();
    source = gameObject.AddComponent<AudioSource >();
  }

  void Start() {
    _Grounded = false;
  }

  private void Update() {
    if (!_isDead && !_IsAttacking) {
      _IsAttacking = true;
      StartCoroutine(_shooter.Shoot(() => {
        StartCoroutine(_enableShooting());
      }));
    }
  }

  public void Live() {
    _isDead = false;
    this.gameObject.layer = 7; // Active enemy
  }

  public void Die() {
    if (deathClip!=null) {
      source.PlayOneShot(deathClip, 1.0f);
    }
    else Debug.Log("missing enemy death clip");
    _isDead = true;
    this.gameObject.layer = 9; // Dead enemy
  }

  // Collision avec le sol
  void OnCollisionEnter(Collision coll) {
    // If enenmy collides with player and enemy is not dead
    if (coll.gameObject.tag == "Player" && this.gameObject.layer != 9) {
      coll.gameObject.GetComponentInChildren<HealthManager>().Die();
    }

    // On s'assure de bien être en contact avec le sol
    if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
      return;

    // Évite une collision avec le plafond
    if (coll.relativeVelocity.y > 0) {
      _Grounded = true;
      _Anim.SetBool("Grounded", _Grounded);
    }

  }
}
