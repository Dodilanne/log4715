using UnityEngine;

public class RobotBase : MonoBehaviour {
  [SerializeField] LayerMask WhatIsGround;

  Animator _anim;

  private void Awake() {
    _anim = GetComponent<Animator>();
  }

  private void OnCollisionEnter(Collision coll) {
    // On s'assure de bien être en contact avec le sol
    if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
      return;

    // Évite une collision avec le plafond
    if (coll.relativeVelocity.y > 0) {
      _anim.SetBool("Grounded", true);
    }
  }
}
