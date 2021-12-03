using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
  [SerializeField] DoorController doorController;

  private AudioSource source;
  public AudioClip pickupKeyClip;

    private void Awake() {
      source = gameObject.AddComponent<AudioSource >();
  }

  private void _pickup() {
    if (pickupKeyClip!=null) {
        source.PlayOneShot(pickupKeyClip, 5.0f);
    }
    else Debug.Log("missing pickup key clip");
    doorController.Unlock();
    this.gameObject.SetActive(false);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {
      _pickup();
    }
  }
}
