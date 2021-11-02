using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LockAndKeyControler : MonoBehaviour
{

    [SerializeField]
    Text keyText;

    [SerializeField]
    Text noKeyText;

    private int keyNumber = 0;
    private bool showNoKeyWarning = false;
    private bool startCoroutine = false;
    private bool touchedDoorWithKey = false;

    // Start is called before the first frame update
    void Start()
    {
        noKeyText.text = "You need a key!";
        noKeyText.gameObject.SetActive(false);
        keyText.text = "Key number: " + keyNumber;
    }

    // Update is called once per frame
    void Update()
    {
      keyText.text = "Key number: " + keyNumber;
      if(showNoKeyWarning) 
      {
        noKeyText.gameObject.SetActive(true);
        StartCoroutine(ShowWarning());
        showNoKeyWarning = false;
      }
      if(touchedDoorWithKey) 
      {
        keyNumber--;
        touchedDoorWithKey = false;
      }
    }

    void OnCollisionEnter(Collision coll) 
    {
      if (coll.gameObject.layer== 3) 
      {
        if (keyNumber >=1) 
        {
          coll.gameObject.transform.Rotate(0,0,-90);
          touchedDoorWithKey = true;
        }
        else 
        {
          showNoKeyWarning = true;
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

    private IEnumerator ShowWarning()
    {
      yield return new WaitForSeconds(1);
      noKeyText.gameObject.SetActive(false);
    }
}
