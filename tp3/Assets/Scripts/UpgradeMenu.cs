using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

    public static bool upgradeMenu = false;
    public GameObject UpgradeMenuUI;
    private int UpgradePoints = 10;

    ScoreUpdate score;

    UpgradeVie BarreVie;
    UpgradeArmure BarreArmure;
    UpgradeSaut BarreSaut;
    UpgradeVitesse BarreVitesse;

    //PlayerController Stats;
    //UpgradeVie BarreVie = new UpgradeVie();
    //GameObject ImgUpgradeVie = GameObject.FindGameObjectWithTag("Img");
    //UpgradeVie BarreVie = ImgUpgrade.GetComponent<UpgradeVie>();
    //BarreUpgradeVie = UpgradeVie ;

    //Stats = GetComponent<PlayerController>();


    void Start()
    {
        score = GetComponentInChildren<ScoreUpdate>();

        BarreVie = GetComponentInChildren<UpgradeVie>();
        BarreVie.valeur = 0;
        BarreVie.max = 10;

        BarreArmure = GetComponentInChildren<UpgradeArmure>();
        BarreArmure.valeur = 0;
        BarreArmure.max = 10;

        BarreSaut = GetComponentInChildren<UpgradeSaut>();
        BarreSaut.valeur = 0;
        BarreSaut.max = 10;

        BarreVitesse = GetComponentInChildren<UpgradeVitesse>();
        BarreVitesse.valeur = 0;
        BarreVitesse.max = 10;

    }

    // Update is called once per frame
    void Update()
    {
        score.scoreNum = UpgradePoints;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (upgradeMenu)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        UpgradeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        upgradeMenu = false;
    }

    void Pause()
    {
        UpgradeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        upgradeMenu = true;
    }

    public void addPointsVie()
    {
        if (UpgradePoints > 0)
        {
            UpgradePoints--;
            BarreVie.valeur += 1;
        }
    }

    public void takePointsVie()
    {
        if (BarreVie.valeur > 0)
        {
            UpgradePoints++;
            BarreVie.valeur -= 1;
        }
    }

    public void addPointsArmure()
    {
        if (UpgradePoints > 0)
        {
            UpgradePoints--;
            BarreArmure.valeur += 1;
        }
    }

    public void takePointsArmure()
    {
        if (BarreArmure.valeur > 0)
        {
            UpgradePoints++;
            BarreArmure.valeur -= 1;
        }
    }


    public void addPointsSaut()
    {
        if (UpgradePoints > 0)
        {
            UpgradePoints--;
            BarreSaut.valeur += 1;
        }
        //Stats.JumpForce = (float)7 + (float)BarreSaut.valeur;
    }

    public void takePointsSaut()
    {
        if (BarreSaut.valeur > 0)
        {
            UpgradePoints++;
            BarreSaut.valeur -= 1;
        }
        //Stats.JumpForce = (float)7 + (float)BarreSaut.valeur;
    }


    public void addPointsVitesse()
    {
        if (UpgradePoints > 0)
        {
            UpgradePoints--;
            BarreVitesse.valeur += 1;
        }
    }

    public void takePointsVitesse()
    {
        if (BarreVitesse.valeur > 0)
        {
            UpgradePoints++;
            BarreVitesse.valeur -= 1;
        }
    }


}
