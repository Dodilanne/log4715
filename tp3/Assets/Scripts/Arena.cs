using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

  [SerializeField] DoorController _entrance;
  [SerializeField] DoorController _exit;
  [SerializeField] int _enemiesCount = 0;
  [SerializeField] bool _hasStarted = false;

  private EnemyController[] _enemies;

  private void Awake() {
    _entrance = this.transform.Find("Entrance").gameObject.GetComponent<DoorController>();
    _exit = this.transform.Find("Exit").gameObject.GetComponent<DoorController>();
    _enemies = this.transform.Find("Enemies").gameObject.GetComponentsInChildren<EnemyController>();
    _enemiesCount = _enemies.Length;
  }

  private void Start() {
    foreach (EnemyController enemy in _enemies) {
      enemy.Die();
      enemy.gameObject.GetComponent<HealthManager>().OnDie = _removeEnemy;
    }
  }

  private void _removeEnemy() {
    _enemiesCount--;
    if (_enemiesCount == 0) {
      _endBattle();
    }
  }

  private void _startBattle() {
    _hasStarted = true;

    _entrance.Lock();
    _entrance.Close();
    _exit.Lock();
    _exit.Close();

    foreach (EnemyController enemy in _enemies) {
      enemy.Live();
    }
  }

  private void _endBattle() {
    _entrance.Open();
    _exit.Open();
  }

  public void OnPlayerEnter() {
    if (!_hasStarted) {
      _startBattle();
    }
  }
}
