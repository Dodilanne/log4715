using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreControler : MonoBehaviour {

  [SerializeField]
  Text scoreText;

  public static int score = 0;
  public static float elapsedTime = 0.0f;
  private float startTime;
  public static bool gameOver = false;

  void Awake() 
  {
    scoreText= FindObjectOfType<Text>();
  }

  void Start() 
  {
    scoreText.text = "POINTS: " + score;
    startTime = Time.time;
  }

  void OnGUI()
  {
    if (!gameOver)
      GUILayout.Label("" + elapsedTime.ToString("f2"));
  }

  void Update() 
  {
    scoreText.text = "POINTS: " + score;
    elapsedTime = Time.time - startTime;
  }
  
  void OnCollisionEnter(Collision coll) 
  {
    // Collision avec la porte
    if (coll.gameObject.layer== 3) 
    {
      gameOver = true;
      coll.gameObject.transform.Rotate(0,0,90);
      SceneManager.LoadScene("Gyms/Gym Score/ScoreResultScene");
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag == "OilCan")
    {
      GameObject.Destroy(other.gameObject);
      score+= 10; 
    }
  }

}