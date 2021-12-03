using UnityEngine;
using UnityEngine.Events;

public class ArenaPlayer : MonoBehaviour {
  // Serialized attributes
  [SerializeField] UnityEvent GameOverEvent;
  private AudioSource source;
  public AudioClip gameOverClip;

  private void Start() {
    source = gameObject.AddComponent<AudioSource >();
  }

  private void Die() {
     if (gameOverClip!=null) {
      source.PlayOneShot(gameOverClip, 1.5f);
    }
    else Debug.Log("missing game over clip");
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
