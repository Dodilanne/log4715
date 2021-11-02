using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LockAndKeyControler : MonoBehaviour
{

    [SerializeField]
    Text keyText;

    private int keyNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        keyText.text = "Key number: " + keyNumber;
    }

    // Update is called once per frame
    void Update()
    {
      keyText.text = "Key number: " + keyNumber;
    }

    void OnCollisionEnter(Collision coll) 
    {
      // Collision avec la porte
      if (coll.gameObject.layer== 3) 
      {
        Debug.Log("hit door");
        if (keyNumber >=1) {
          keyNumber--;
          // TODO Open door
        }
        else {
          // TODO popup "no key"
        }
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if(other.tag == "Key")
      {
        GameObject.Destroy(other.gameObject);
        keyNumber ++;
        
      }
    }
}
