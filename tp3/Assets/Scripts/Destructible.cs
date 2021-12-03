using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {
  private ParticleSystem _particleSystem;

  private AudioSource source;
  public AudioClip doorExplosionClip;

  void Start() {
    _particleSystem = GetComponent<ParticleSystem>();
    source = gameObject.AddComponent<AudioSource>();
  }

  public void Destruct() {
    if (doorExplosionClip != null) {
      source.PlayOneShot(doorExplosionClip, 1.5f);
    } else Debug.Log("missing destruction clip");

    _particleSystem.Play();

    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;

    Invoke("EndLife", _particleSystem.main.startLifetimeMultiplier);
  }

  private void EndLife() {
    Destroy(this.gameObject);
  }
}
