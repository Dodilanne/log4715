using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipal : MonoBehaviour {
  public void Jouer() {
    SceneManager.LoadScene("Final Level");
  }

  public void Quitter() {
    Debug.Log("quitter");
    Application.Quit();
  }
}
