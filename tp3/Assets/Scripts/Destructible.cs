using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    ParticleSystem particleSystem;

    private AudioSource source;
    public AudioClip doorExplosionClip;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        source = gameObject.AddComponent<AudioSource >();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destruct(){
        if (doorExplosionClip!=null) {
          source.PlayOneShot(doorExplosionClip, 1.5f);
        }
        else Debug.Log("missing destruction clip");
        particleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("EndLife", particleSystem.startLifetime);
    }

    private void EndLife(){
        Destroy(this.gameObject);
    }
}
