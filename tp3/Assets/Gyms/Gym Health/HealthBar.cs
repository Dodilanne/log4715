using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    static Image Barre;
    public float max;
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
    void Start()
    {
        Barre = GetComponent<Image> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
