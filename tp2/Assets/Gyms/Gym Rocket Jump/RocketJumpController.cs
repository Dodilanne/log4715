using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketJumpController : MonoBehaviour
{

    [SerializeField]
    ParticleSystem fireParticles;
    [SerializeField]
    string rocketInputAction = "Rocket Jump";
    [SerializeField]
    float RocketForce = 10f;

    [SerializeField]
    float rocketFuelCapacity = 5f;
    
    [SerializeField]
    float jerrycanFuelContents = 5f;

    [SerializeField]
    Text rocketFuelUi;
    [SerializeField]
    string rocketFuelUiPrefix = "Rocket Fuel: ";

    Rigidbody rb;

    bool flying = false;

    float currentFuel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentFuel = rocketFuelCapacity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(rocketInputAction) && CanFly()){
            fireParticles.Play();
            flying = true;
        }
        if(Input.GetButtonUp(rocketInputAction)){
            fireParticles.Stop();
            flying = false;
        }

        if(flying){
            rb.AddForce(new Vector3(0, RocketForce * Time.deltaTime, 0), ForceMode.Impulse);
            currentFuel = Mathf.Clamp(currentFuel - Time.deltaTime, 0, rocketFuelCapacity);
            if(!CanFly()){
                fireParticles.Stop();
                flying = false;
            }
        }
        rocketFuelUi.text = rocketFuelUiPrefix + currentFuel.ToString("#.00");

    }

    private bool CanFly(){
        return currentFuel > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "JerryCan"){
            GameObject.Destroy(other.gameObject);
            currentFuel = Mathf.Clamp(currentFuel + jerrycanFuelContents, 0, rocketFuelCapacity);
        }
    }
}
