using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {
  private ParticleSystem _particleSystem;

  void Start() {
    _particleSystem = GetComponent<ParticleSystem>();
  }

  public void Destruct() {
    _particleSystem.Play();
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
    Invoke("EndLife", _particleSystem.main.startLifetimeMultiplier);
  }

  private void EndLife() {
    Destroy(this.gameObject);
  }
}
