using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArmure : MonoBehaviour
{
    Image Barre;
    public float max=1;
    private float m_valeur;
    public float valeur
    {
        get { return m_valeur; }
        set
        {
            m_valeur = Mathf.Clamp(value, 0, max);
            Barre.fillAmount = valeur / max;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        Barre = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
