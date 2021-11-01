using UnityEngine;
using UnityEngine.Events;

public class ArenaPlayer : MonoBehaviour {
  // Serialized attributes
  [SerializeField] UnityEvent GameOverEvent;

  // Collision avec le sol
  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Enemy" && GameOverEvent != null) {
      GameOverEvent.Invoke();
    }
  }
}
