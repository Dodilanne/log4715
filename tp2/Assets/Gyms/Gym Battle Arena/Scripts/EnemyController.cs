using UnityEngine;

public class EnemyController : MonoBehaviour {
  // Serialized attributes
  [SerializeField] RocketManager RocketManager;
  [SerializeField] LayerMask WhatIsGround;
  [SerializeField] float ShootSpeed = 2f;

  // Private attributes
  bool _Grounded { get; set; }
  Animator _Anim { get; set; }

  void Awake() {
    _Anim = GetComponent<Animator>();
  }

  void Start() {
    _Grounded = false;
    InvokeRepeating("Shoot", 2f, ShootSpeed);
  }

  private void Die() {
    Destroy(this.gameObject);
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

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "PlayerRocket") {
      Die();
    }
  }
}
