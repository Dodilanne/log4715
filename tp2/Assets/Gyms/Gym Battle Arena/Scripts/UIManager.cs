using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  private Button _Button;


  private void Awake() {
    _Button = this.gameObject.GetComponentInChildren<Button>();
    if (_Button != null) {
      _Button.onClick.AddListener(_ReloadScene);
    }
  }

  private void Start() {
    this.gameObject.SetActive(false);
  }

  public void GameOver() {
    GameController.Pause();
    this.gameObject.SetActive(true);
  }

  private void _ReloadScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    GameController.Resume();
  }
}
