using UnityEngine;

public class GameController : MonoBehaviour {
  public static void Pause() {
    Time.timeScale = 0f;
  }

  public static void Resume() {
    Time.timeScale = 1;
  }
}
