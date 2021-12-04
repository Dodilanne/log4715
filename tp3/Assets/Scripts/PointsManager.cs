using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {
  [SerializeField] private int baseTarget = 50;
  [SerializeField] private int xpPerCan = 10;

  private int _xp = 0;
  private int _currTarget;

  private Slider _slider;
  private Text _progress;
  private Text _target;

  private void Awake() {
    GameObject bar = GameObject.Find("XP Bar");
    _slider = bar.transform.GetComponentInChildren<Slider>();
    _progress = bar.transform.Find("Progress").GetComponent<Text>();
    _target = bar.transform.Find("Target").GetComponent<Text>();

    _xp = 0;
    _currTarget = baseTarget;
    _updateUI();
  }

  private void OnCollisionEnter(Collision other) {
    if (other.gameObject.tag == "OilCan") {
      Destroy(other.gameObject);
      _updateXP(_xp + xpPerCan);
    }
  }

  private void _levelUp() {
    _currTarget *= 2;
  }

  private void _updateUI() {
    _target.text = _currTarget.ToString();
    _progress.text = _xp.ToString();
    _slider.maxValue = _currTarget;
    _slider.value = _xp;
  }

  private void _updateXP(int value) {
    _xp = value;
    if (_xp >= _currTarget) _levelUp();
    _updateUI();
  }
}
