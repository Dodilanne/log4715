using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAndKeyControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll) 
  {
    // Collision avec la porte
    if (coll.gameObject.layer== 3) 
    {
      Debug.Log("hit door");
    }
  }
}
