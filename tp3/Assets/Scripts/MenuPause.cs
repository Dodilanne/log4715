using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{

    public static bool OnMenu = false;
    public GameObject MenuPauseUI;
    public GameObject MenuOptionUI;
    public GameObject MenuUpgradeUI;
    public GameObject BackgroundImg;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnMenu)
            {
                Resume();
                BackgroundImg.SetActive(false);
                MenuOptionUI.SetActive(false);
                MenuUpgradeUI.SetActive(false);

            }
            else
            {
                Pause();
                BackgroundImg.SetActive(true);
            }
        }
    }


    public void Resume()
    {
        MenuPauseUI.SetActive(false);
        Time.timeScale = 1f;
        OnMenu = false;
    }

    void Pause()
    {
        MenuPauseUI.SetActive(true);
        Time.timeScale = 0f;
        OnMenu = true;
    }

}
