using UnityEngine;

public class RobotBase : MonoBehaviour {
  Animator _anim;

  private void Awake() {
    _anim = GetComponent<Animator>();
  }

  private void OnCollisionEnter(Collision coll) {
    if (coll.gameObject.tag == "Wall" && coll.relativeVelocity.y > 0) {
      _anim.SetBool("Grounded", true);
    }
  }
}
