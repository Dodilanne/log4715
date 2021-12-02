using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour {
  [SerializeField] Material LockedMaterial;
  [SerializeField] Material UnlockedMaterial;
  [SerializeField] bool IsLocked = false;
  [SerializeField] bool IsOpened = false;
  [SerializeField] UnityEvent OnClose;

  private UIManager UIManager;
  private bool IsActive = false;
  private Transform _Door;
  private MeshRenderer _MeshRenderer;
  private bool _ShouldTriggerCloseEvent = false;

  private void Awake() {
    UIManager = GameObject.FindObjectOfType<UIManager>();
  }

  private void Update() {
    if (IsActive && !IsLocked && Input.GetButtonDown("Open")) {
      if (IsOpened) Close();
      else Open();
    }

    if (IsOpened && _Door.localPosition.y < 6) {
      _Door.Translate(Vector3.up * Time.deltaTime * 8);
    } else if (!IsOpened && _Door.localPosition.y > 0) {
      _Door.Translate(Vector3.down * Time.deltaTime * 8);
    } else if (!IsOpened && _ShouldTriggerCloseEvent) {
      _ShouldTriggerCloseEvent = false;
      if (OnClose != null) OnClose.Invoke();
    }
  }

  private void Start() {
    _Door = this.gameObject.transform.GetChild(0);
    _MeshRenderer = _Door.gameObject.GetComponent<MeshRenderer>();
    if (IsLocked) Lock();
    else Unlock();
  }

  public void Lock() {
    IsLocked = true;
    _MeshRenderer.material = LockedMaterial;
  }

  public void Unlock() {
    IsLocked = false;
    _MeshRenderer.material = UnlockedMaterial;
  }

  public void Open() {
    IsOpened = true;
  }

  public void Close() {
    IsOpened = false;
    _ShouldTriggerCloseEvent = true;
  }

  private void OnTriggerEnter(Collider other) {
    if (!IsLocked && other.gameObject.tag == "Player") {
      IsActive = true;
      UIManager.ToggleTipPanel(true);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.tag == "Player") {
      IsActive = false;
      UIManager.ToggleTipPanel(false);
    }
  }
}
