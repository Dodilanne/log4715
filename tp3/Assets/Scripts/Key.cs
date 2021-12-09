using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
  [SerializeField] DoorController doorController;

  private void _pickup() {
    doorController.Unlock();
    this.gameObject.SetActive(false);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {
      _pickup();
    }
  }
}
