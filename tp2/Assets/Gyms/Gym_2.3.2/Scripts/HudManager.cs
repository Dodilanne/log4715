using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
  private GameObject _pickup;
  private GameObject _equipment;

  private void Awake() {
    _pickup = this.transform.Find("Pickup").gameObject;
    _equipment = this.transform.Find("Equipment").gameObject;
    _pickup.SetActive(false);
    _equipment.SetActive(false);
  }

  public void ShowPickup(string rocketType) {
    _pickup.transform.Find("RocketType").gameObject.GetComponent<Text>().text = rocketType;
    _pickup.SetActive(true);
  }

  public void ShowEquipment(string rocketType, int damage, float speed) {
    _equipment.transform.Find("RocketType").gameObject.GetComponent<Text>().text = rocketType;
    _equipment.transform.Find("Damage").gameObject.GetComponent<Text>().text = damage.ToString();
    _equipment.transform.Find("Speed").gameObject.GetComponent<Text>().text = speed.ToString();
    _equipment.SetActive(true);
  }

  public void HidePickup() {
    _pickup.SetActive(false);
  }

  public void HideEquipment() {
    _equipment.SetActive(false);
  }
}
