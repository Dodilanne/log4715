using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  // Serialized attributes
  [SerializeField] RocketManager RocketManager;
  [SerializeField] LayerMask WhatIsGround;
  [SerializeField] float ShootSpeed = 2f;

  // Private attributes
  private GameController _Game;
  bool _Grounded { get; set; }
  Animator _Anim { get; set; }
  Shooter _Shooter { get; set; }
  private bool _IsAttacking = false;

  void Awake() {
    _Anim = GetComponent<Animator>();
    _Shooter = GetComponent<Shooter>();
    _Game = GameObject.FindObjectOfType<GameController>();
  }

  void Start() {
    _Grounded = false;
  }

  private void Update() {
    if (!_IsAttacking && _Game.HasEntered) {
      _IsAttacking = true;
      StartCoroutine(_Shooter.Shoot());
    }
  }

  private void Shoot() {
    _Anim.SetTrigger("Pickup");
    RocketManager.Spawn(this.gameObject);
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
}
