using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaPlayer : MonoBehaviour {
  // Serialized attributes
  [SerializeField] bool IsDead;

  void Start() {
    IsDead = false;
  }

  // Collision avec le sol
  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Enemy") {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}
