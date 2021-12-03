using UnityEngine;

public class RocketManager : MonoBehaviour {
  [SerializeField] LayerMask WhatIsGround;
  [SerializeField] GameObject PlayerRocketPrefab;
  [SerializeField] GameObject EnemyRocketPrefab;
  [SerializeField] Vector3 RocketOffset;

  public void Spawn(GameObject spawner) {
    GameObject RocketPrefab = spawner.tag == "Player" ? PlayerRocketPrefab : EnemyRocketPrefab;
    bool isFacingLeft = spawner.transform.rotation.y > 0;
    Vector3 offset = RocketOffset;
    if (isFacingLeft) offset.z *= -1;
    GameObject rocket = Instantiate(RocketPrefab, offset + spawner.transform.position, spawner.transform.rotation, this.transform);
    rocket.GetComponent<RocketController>().Init(WhatIsGround);
    rocket.transform.Rotate(Vector3.right, 90);
    rocket.GetComponent<Rigidbody>().velocity = (isFacingLeft ? Vector3.back : Vector3.forward) * 10;
  }
}
