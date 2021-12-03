using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
public class UIManager : MonoBehaviour {
  private Button[] _Buttons;
  private GameObject _GameOverPanel;
  private GameObject _VictoryPanel;
  private GameObject _TipPanel;
  private GameObject _CongratsPanel;

  private AudioSource source;
  public AudioClip buttonClickClip;

  private void Awake() {
    _Buttons = this.gameObject.GetComponentsInChildren<Button>();
    _GameOverPanel = GameObject.Find("GameOver");
    _VictoryPanel = GameObject.Find("Victory");
    _TipPanel = GameObject.Find("Tip");
    _CongratsPanel = GameObject.Find("Congrats");
    source = gameObject.AddComponent<AudioSource >();
    if (_Buttons.Count() > 0) {
      foreach (Button button in _Buttons) {
        button.onClick.AddListener(_ReloadScene);
      }
    }
  }

  private void Start() {
    _GameOverPanel.SetActive(false);
    _TipPanel.SetActive(false);
    _CongratsPanel.SetActive(false);
    _VictoryPanel.SetActive(false);
  }

  public void GameOver() {
    GameController.Pause();
    _GameOverPanel.SetActive(true);
  }

  public void Victory() {
    GameController.Pause();
    _VictoryPanel.SetActive(true);
  }

  public void ToggleTipPanel(bool show) {
    _TipPanel.SetActive(show);
  }

  public void ToggleCongratsPanel(bool show) {
    _CongratsPanel.SetActive(show);
  }

  private void _ReloadScene() {
    if (buttonClickClip!=null) {
      source.PlayOneShot(buttonClickClip, 1.0f);
    }
    else Debug.Log("missing button click clip");
      Thread.Sleep(300) ;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      GameController.Resume();
  }
}
