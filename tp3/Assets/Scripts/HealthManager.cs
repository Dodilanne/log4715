using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
  [SerializeField] private int maxHealth = 100;
  [SerializeField] private int initialHealth = -1;
  [SerializeField] private Vector3 healthBarOffset = Vector3.zero;

  private int _currentHealth;
  private Slider _healthBar;
  private GameController _game;
  private UIManager _uiManager;

  private AudioSource source;
  public AudioClip gameOverClip;
  public AudioClip enemyDeathClip;
  public AudioClip enemyHitClip;
  public AudioClip playerHitClip;

  private void Awake() {
    _healthBar = this.gameObject.GetComponentInChildren<Slider>();
    _game = GameObject.FindObjectOfType<GameController>();
    _uiManager = GameObject.FindObjectOfType<UIManager>();

    _healthBar.maxValue = maxHealth;
    _setCurrentHealth(initialHealth >= 0 ? initialHealth : maxHealth);

    source = gameObject.AddComponent<AudioSource >();
  }

  private void Update() {
    _healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + healthBarOffset);
  }

  private bool _isEnemy() {
    return this.tag == "Enemy";
  }


  private void _setCurrentHealth(int health) {
    _currentHealth = health;
    _healthBar.value = health;
  }

  public void Die() {
    Quaternion rotation = this.transform.rotation;
    this.transform.Rotate(Vector3.forward, 90);
    this.transform.Rotate(Vector3.left, 45);

    _setCurrentHealth(0);

    if (_isEnemy()) {
      if (enemyDeathClip!=null) {
        source.PlayOneShot(enemyDeathClip, 1f);
      }
      else Debug.Log("missing enemy death clip");
      _game.RemoveEnemy();
      this.gameObject.layer = 9; // Dead enemies
      GetComponent<EnemyController>().Die();
      this.transform.Find("Health Bar").gameObject.SetActive(false);
    } else {
        if (gameOverClip!=null) {
          source.PlayOneShot(gameOverClip, 1.5f);
        }
        else Debug.Log("missing game over clip");
        _uiManager.GameOver();
    }
  }

  public void Hit(int amount) {
    _setCurrentHealth(Math.Max(0, _currentHealth - amount));
    if (_currentHealth == 0) {
      Die();
    }
    else {
      if(_isEnemy()) {
        if (enemyHitClip!=null) {
          source.PlayOneShot(enemyDeathClip, 1f);
        }
        else Debug.Log("missing enemy hit clip");
      }
      else {
        if (playerHitClip!=null) {
        source.PlayOneShot(playerHitClip, 1.5f);
        }
        else Debug.Log("missing player hit clip");
      }
    }
  }

  public void Heal(int amount) {
    _setCurrentHealth(Math.Min(maxHealth, _currentHealth + amount));
  }
}
