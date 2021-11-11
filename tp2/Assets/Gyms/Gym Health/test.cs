using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    HealthBar BarreVie = new HealthBar();
    public static bool gameOver = false;

    void Start()
    {
        BarreVie.max = 100;
        BarreVie.valeur = 50;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Life")
        {
            GameObject.Destroy(other.gameObject);
            BarreVie.valeur += 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            BarreVie.valeur -= 10;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            BarreVie.valeur += 10;
        }
    }
}
