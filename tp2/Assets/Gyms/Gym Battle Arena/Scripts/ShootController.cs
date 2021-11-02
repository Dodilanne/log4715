using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootController : MonoBehaviour {
  // Serialized attributes
  [SerializeField] RocketManager RocketManager;
  [SerializeField] float PickupAnimSpeed = 6f;
  [SerializeField] float ShotDelay = 0f; // Time between shots (in seconds)

  // Private attributes
  private bool _Grounded { get; set; }
  private Animator _Anim { get; set; }
  private bool _CanShoot;
  private float _ShotDuration = -1f;

  void Awake() {
    _Anim = GetComponent<Animator>();
  }

  void Start() {
    _CanShoot = true;
    SetShotDuration();
  }


  private void Update() {
    if (_CanShoot && Input.GetButtonDown("Fire1")) {
      StartCoroutine(Shoot());
    }
  }

  private IEnumerator Shoot() {
    if (_ShotDuration < 0) yield return false;

    _CanShoot = false;
    _Anim.SetTrigger("Pickup");
    RocketManager.Spawn(this.gameObject);
    yield return new WaitForSeconds(_ShotDuration + ShotDelay);
    _CanShoot = true;
  }

  private void SetShotDuration() {
    IEnumerable<AnimationClip> clips = _Anim.runtimeAnimatorController.animationClips.Where(clip => clip.name == "pickup");
    if (clips.Count() > 0) {
      _ShotDuration = clips.First().length / PickupAnimSpeed;

    }
  }
}
