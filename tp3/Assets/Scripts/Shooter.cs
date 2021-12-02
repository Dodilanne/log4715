using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour {

  [SerializeField] private GameObject _equippedRocket;
  private bool _isReloading = false;

  private Animator _anim;
  private RocketsManager _rocketsManager;
  private float _shotDuration = -1f;

  public void EquipRocket(GameObject rocketPrefab) {
    _equippedRocket = rocketPrefab;
  }

  private bool _canShoot() {
    return !_isReloading && _equippedRocket != null;
  }

  private bool _isEnemy() {
    return this.tag == "Enemy";
  }

  private void SetShotDuration() {
    IEnumerable<AnimationClip> clips = _anim.runtimeAnimatorController.animationClips.Where(clip => clip.name == "pickup");
    if (clips.Count() > 0) {
      _shotDuration = clips.First().length / 6f;
    }
  }

  public IEnumerator Shoot(Action action = null) {
    if (_shotDuration < 0) yield return false;

    _isReloading = true;
    _anim.SetTrigger("Pickup");
    _rocketsManager.Spawn(this.gameObject, _equippedRocket);
    yield return new WaitForSeconds(_shotDuration);
    _isReloading = false;
    if (action != null) {
      action();
    }
  }

  void Awake() {
    _anim = GetComponent<Animator>();
    _rocketsManager = GameObject.FindObjectOfType<RocketsManager>();
  }

  void Start() {
    _isReloading = false;
    SetShotDuration();
  }

  private void Update() {
    if (!_isEnemy() && _canShoot() && Input.GetButtonDown("Fire1")) {
      StartCoroutine(Shoot());
    }
  }
}
