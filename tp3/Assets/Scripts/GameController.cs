using UnityEngine;

public class GameController : MonoBehaviour {
  private AudioSource source;
  public AudioClip bgClip;

  private void Start() {
    source = gameObject.AddComponent<AudioSource>();
    if (bgClip != null) {
      source.clip = bgClip;
      source.volume = 0.3f;
      source.loop = true;
      source.Play();
    } else Debug.Log("missing arena door open clip");
  }

  public static void Pause() {
    Time.timeScale = 0f;
  }

  public static void Resume() {
    Time.timeScale = 1;
  }
}
