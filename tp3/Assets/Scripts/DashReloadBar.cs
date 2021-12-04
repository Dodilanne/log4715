using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashReloadBar : MonoBehaviour {
  private Slider _slider;

  private void Awake() {
    _slider = this.transform.GetComponentInChildren<Slider>();
    _slider.maxValue = 1;
    _slider.value = 1;
  }

  private void Update() {
    if (_slider.value < _slider.maxValue) {
      _slider.value += Time.deltaTime;
    }
  }

  public void Dash(float reloadTime) {
    _slider.value = 0;
    _slider.maxValue = reloadTime;
  }
}
