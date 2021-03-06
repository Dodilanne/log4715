using System.Linq;
using UnityEngine;
using UnityEngine.Android;

public class Rocket : MonoBehaviour {
  [SerializeField] int damage = 20;
  [SerializeField] float speed = 1f;
  [SerializeField] bool canBreakWalls = false;
  [SerializeField] string[] dealsDamageTo;

  public void Launch(bool isFacingLeft) {
    this.transform.Rotate(Vector3.right, 90);
    this.GetComponent<Rigidbody>().velocity = (isFacingLeft ? Vector3.back : Vector3.forward) * speed;
  }

  public int Damage { get => damage; }
  public float Speed { get => speed; }

  private void _removeFromScene() {
    Destroy(this.gameObject);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Destructible") {
      if (canBreakWalls) {
        Destructible destructible = other.gameObject.GetComponentInChildren<Destructible>();
        if (destructible != null) {
          destructible.Destruct();
        } else {
          Debug.Log("Destructible has no Destructible component");
        }
      }
      _removeFromScene();
    };
    if (dealsDamageTo.Contains(other.tag)) {
      HealthManager healthManager = other.gameObject.GetComponentInChildren<HealthManager>();
      if (healthManager != null) healthManager.Hit(damage);
      _removeFromScene();
    }

    if (other.tag == "Door" || other.gameObject.layer == 6) {
      _removeFromScene();
    }
  }
}
