using UnityEngine;

public class EnemyController : MonoBehaviour {
  // Serialized attributes
  [SerializeField] LayerMask WhatIsGround;

  // Private attributes
  bool _Grounded { get; set; }
  Animator _Anim { get; set; }

  void Awake() {
    _Anim = GetComponent<Animator>();
  }

  void Start() {
    _Grounded = false;
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
