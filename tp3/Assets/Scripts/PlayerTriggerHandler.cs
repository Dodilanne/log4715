using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerHandler : MonoBehaviour {
  [SerializeField] UnityEvent EventHandler;

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {
      EventHandler.Invoke();
    }
  }
}
