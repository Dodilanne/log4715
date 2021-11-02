using UnityEngine;

public class RocketController : MonoBehaviour {
  private LayerMask _WhatIsGround;

  private bool _IsEnemyRocket;

  private void Start() {
    _IsEnemyRocket = this.gameObject.tag == "EnemyRocket";
  }

  public void Init(LayerMask whatIsGround) {
    _WhatIsGround = whatIsGround;
  }

  private bool IsLayer(LayerMask layer, Collider collider) {
    return (layer & (1 << collider.gameObject.layer)) > 0;
  }

  private void OnTriggerEnter(Collider other) {
    bool isPlayer = other.gameObject.tag == "Player";
    bool isArena = other.gameObject.tag == "Arena";
    bool isGround = IsLayer(_WhatIsGround, other);
    bool isRocket = other.gameObject.tag == "EnemyRocket" || other.gameObject.tag == "PlayerRocket";
    if (!isArena && !isRocket && (isGround || (isPlayer && _IsEnemyRocket || !isPlayer && !_IsEnemyRocket))) {
      Destroy(this.gameObject);
    }
  }
}
