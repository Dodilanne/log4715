using UnityEngine;
using UnityEngine.Events;

public class ArenaPlayer : MonoBehaviour {
  // Serialized attributes
  [SerializeField] UnityEvent GameOverEvent;

  private void Die() {
    GameOverEvent.Invoke();
  }

  // Collision avec le sol
  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Enemy" && GameOverEvent != null) {
      Die();
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "EnemyRocket") {
      Die();
    }
  }
}
