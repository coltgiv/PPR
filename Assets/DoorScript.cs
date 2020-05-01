using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject particleObject;
    private ParticleSystem particle;
    private bool isHitted = false;
    // Start is called before the first frame update
    void Start()
    {
        particle = particleObject.GetComponent<ParticleSystem>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.name == "Player" && !isHitted)
        {
            
            particle.Play();
            isHitted = true;
        }
    }
}
