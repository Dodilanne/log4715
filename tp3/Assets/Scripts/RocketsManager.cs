using UnityEngine;

public class RocketsManager : MonoBehaviour {
  [SerializeField] Vector3 RocketOffset;

  public void Spawn(GameObject spawner, GameObject rocketPrefab) {
    bool isFacingLeft = spawner.transform.rotation.y > 0;
    Vector3 offset = RocketOffset;
    if (isFacingLeft) offset.z *= -1;
    GameObject rocket = Instantiate(rocketPrefab, offset + spawner.transform.position, spawner.transform.rotation, this.transform);
    rocket.GetComponent<Rocket>().Launch(isFacingLeft);
  }
}
