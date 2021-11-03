using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
  [SerializeField] private int maxHealth = 100;
  [SerializeField] private int initialHealth = -1;
  [SerializeField] private Vector3 healthBarOffset = Vector3.zero;

  private int _currentHealth;
  private Slider _healthBar;

  private void Awake() {
    _healthBar = this.gameObject.GetComponentInChildren<Slider>();

    _healthBar.maxValue = maxHealth;
    _setCurrentHealth(initialHealth >= 0 ? initialHealth : maxHealth);
  }

  private void Update() {
    _healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + healthBarOffset);
  }

  private void _die() {
    Destroy(this.gameObject);
  }

  private void _setCurrentHealth(int health) {
    _currentHealth = health;
    _healthBar.value = health;
  }

  public void Hit(int amount) {
    _setCurrentHealth(Math.Max(0, _currentHealth - amount));
    if (_currentHealth == 0) _die();
  }

  public void Heal(int amount) {
    _setCurrentHealth(Math.Min(maxHealth, _currentHealth + amount));
  }
}
