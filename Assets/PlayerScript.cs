using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Vertical movement from input.
    private float verticalMove;
    //Horizontal movement from input.
    private float horizontalMove;
    //Speed of sphere.
    private float speed = 400;
    //Rigidbody of sphere.
    private Rigidbody rigidbody;
    //Number of capsule collected.
    private int numberOfCapsule;
    //Object of ending screen.
    private GameObject endingScreen;
    // Deactivating ending screen and geting rigidbody.
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        numberOfCapsule = 0;
        endingScreen = GameObject.Find("EndingScreen");
        endingScreen.SetActive(false);
        Cursor.visible = false;
    }

    // Updating movement of sphere.
    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");

        Vector3 movingVector = new Vector3(-verticalMove, 0.0f, horizontalMove);
        rigidbody.AddForce(movingVector * speed);
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // When interact with capsule increse parameter and chceck if game should end.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Fire")
        {
            foreach (ParticleSystem particle in other.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (particle.name == "Sparks" || particle.name == "Fire")
                {
                    particle.Stop();
                }
                else if (particle.name == "Smoke")
                {
                    particle.Play();
                }
            }
            StartCoroutine(Die(other.gameObject));
        }
        else
        {
            numberOfCapsule++;
            burstParticle(other.gameObject);
            StartCoroutine(Destroy(other.gameObject));
            if (numberOfCapsule == 5)
            {
                endingScreen.SetActive(true);
                Cursor.visible = true;
                rigidbody.Sleep();
            }
        }
        
    }

    private void burstParticle(GameObject capsule)
    {
        ParticleSystem particleSystem = capsule.GetComponent<ParticleSystem>();
        if(particleSystem)
        {
            particleSystem.startColor = Color.yellow;
            particleSystem.emissionRate = 100;
            particleSystem.loop = false;
        }
    }

    IEnumerator Destroy(GameObject capsule)
    {
        capsule.GetComponent<MeshRenderer>().enabled = false;
        capsule.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(4);
        capsule.SetActive(false);
    }

    IEnumerator Die(GameObject fire)
    {
        yield return new WaitForSeconds(4);
        Destroy(fire);
    }
}
