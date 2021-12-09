using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {
  [SerializeField] private float speedInc = 2f;
  [SerializeField] private float jumpInc = 1f;
  [SerializeField] private float dashInc = 1f;

  public static bool upgradeMenu = false;
  public GameObject UpgradeMenuUI;
  public int UpgradePoints = 0;

  ScoreUpdate score;

  UpgradeArmure BarreArmure;
  UpgradeSaut BarreSaut;
  UpgradeVitesse BarreVitesse;

  private PlayerController _player;

  private void Awake() {
    _player = GameObject.FindObjectOfType<PlayerController>();
  }

  void Start() {
    score = GetComponentInChildren<ScoreUpdate>();

    BarreArmure = GetComponentInChildren<UpgradeArmure>();
    BarreArmure.valeur = 0;
    BarreArmure.max = 10;

    BarreSaut = GetComponentInChildren<UpgradeSaut>();
    BarreSaut.valeur = 0;
    BarreSaut.max = 10;

    BarreVitesse = GetComponentInChildren<UpgradeVitesse>();
    BarreVitesse.valeur = 0;
    BarreVitesse.max = 10;
  }

  // Update is called once per frame
  void Update() {
    score.scoreNum = UpgradePoints;

    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (upgradeMenu) {
        Resume();
      } else {
        Pause();
      }
    }
  }

  public void Resume() {
    UpgradeMenuUI.SetActive(false);
    Time.timeScale = 1f;
    upgradeMenu = false;
  }

  void Pause() {
    UpgradeMenuUI.SetActive(true);
    Time.timeScale = 0f;
    upgradeMenu = true;
  }

  public void addPointsArmure() {
    if (UpgradePoints > 0) {
      UpgradePoints--;
      BarreArmure.valeur += 1;
      _player.DashDistance += dashInc;
    }
  }

  public void takePointsArmure() {
    if (BarreArmure.valeur > 0) {
      UpgradePoints++;
      BarreArmure.valeur -= 1;
      _player.DashDistance -= dashInc;
    }
  }


  public void addPointsSaut() {
    if (UpgradePoints > 0) {
      UpgradePoints--;
      BarreSaut.valeur += 1;
      _player.JumpForce += jumpInc;
    }
  }

  public void takePointsSaut() {
    if (BarreSaut.valeur > 0) {
      UpgradePoints++;
      BarreSaut.valeur -= 1;
      _player.JumpForce -= jumpInc;
    }
  }


  public void addPointsVitesse() {
    if (UpgradePoints > 0) {
      UpgradePoints--;
      BarreVitesse.valeur += 1;
      _player.MoveSpeed += speedInc;
    }
  }

  public void takePointsVitesse() {
    if (BarreVitesse.valeur > 0) {
      UpgradePoints++;
      BarreVitesse.valeur -= 1;
      _player.MoveSpeed -= speedInc;
    }
  }


}
