using UnityEngine;

public class GameController : MonoBehaviour {
  [SerializeField] DoorController Entrance;
  [SerializeField] DoorController Exit;
  [SerializeField] UIManager UIManager;

  [System.NonSerialized] public int EnemiesCount = 0;
  [System.NonSerialized] public bool HasEntered = false;

  private void Start() {
    // EnemiesCount = GameObject.Find("Enemies").transform.childCount;
  }

  public static void Pause() {
    Time.timeScale = 0f;
  }

  public static void Resume() {
    Time.timeScale = 1;
  }

  public void RemoveEnemy() {
    // EnemiesCount--;
    // if (EnemiesCount == 0) {
    //   EndFight();
    // }
  }

  public void EnterArena() {
    Entrance.Lock();
    Entrance.Close();
    HasEntered = true;
  }

  public void EndFight() {
    Exit.Unlock();
    UIManager.ToggleCongratsPanel(true);
  }

  public void ExitArena() {
    Exit.Lock();
    Exit.Close();
    HasEntered = false;
    UIManager.ToggleCongratsPanel(false);
  }
}
