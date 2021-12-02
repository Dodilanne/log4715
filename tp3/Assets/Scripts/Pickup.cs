using UnityEngine;

public class Pickup : MonoBehaviour {
  [SerializeField] GameObject rocketPrefab;

  private bool _isActive = false;
  private Quaternion _initialRotation;
  private Shooter _player;
  private HudManager _hudManager;

  private void Awake() {
    _initialRotation = this.transform.rotation;
    _hudManager = GameObject.FindObjectOfType<HudManager>();
  }

  private void Update() {
    if (_isActive) {
      this.transform.Rotate(new Vector3(3, 5, 0) / 2);
    } else if (this.transform.rotation != _initialRotation) {
      this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _initialRotation, 90 * Time.deltaTime);
    }

    if (_isActive && _player != null && Input.GetButtonDown("Open")) {
      _player.EquipRocket(rocketPrefab);
      Rocket rocket = rocketPrefab.GetComponent<Rocket>();
      _hudManager.ShowEquipment(this.gameObject.name, rocket.Damage, rocket.Speed);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      _isActive = true;
      _hudManager.ShowPickup(this.gameObject.name);
      _player = other.gameObject.GetComponentInChildren<Shooter>();
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.tag == "Player") {
      _isActive = false;
      _hudManager.HidePickup();
      _player = null;
    }
  }
}
