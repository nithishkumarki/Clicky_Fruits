// using System;
using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbodyRB;
    private float minForce=12;
    private float maxForce=16;
    private float xTorque=10;
    private float xRange=4;
    private float yMinRange=5;
    private float yMaxRange=0;
    private int livesNegative=1;
    public GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    public float soundIndicator;

    void Start()
    {
        rigidbodyRB=GetComponent<Rigidbody>();
        rigidbodyRB.AddForce(RandomForce(),ForceMode.Impulse);
        rigidbodyRB.AddTorque(randomTorque(),randomTorque(),randomTorque());//torque is for Rotation using rigidBody
        transform.position=RandomPosition();
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("bad"))
        {
            gameManager.upDateLives(livesNegative);
            if(gameManager.lives<=0)
            {
            gameManager.GameOver();

            }
        }
    }
    public void DestroyTarget()
    {
        if(gameManager.isGameActive)
        {
            if(gameObject.CompareTag("bad"))
            {
                soundIndicator=0;
            }
            else{
                soundIndicator=1;
            }
            gameManager.soundCall(soundIndicator);
           Destroy(gameObject);
           Instantiate(explosionParticle,transform.position,explosionParticle.transform.rotation);
           gameManager.upDateScore(pointValue);
        }
    }
    // private void OnMouseDown()//insted of Destroy target
    // {
    //     if(gameManager.isGameActive)
    //     {

    //     Destroy(gameObject);
    //     gameManager.upDateScore(pointValue);
    //     Instantiate(explosionParticle,transform.position,explosionParticle.transform.rotation);
    //     }
    // }
    Vector3 RandomForce()
    {
        return Vector3.up*Random.Range(minForce,maxForce);// no new because we are multipling existing Vector3;
    }
    float randomTorque()
    {
       return Random.Range(-xTorque,xTorque);
    }
    Vector3 RandomPosition()
    {
       return new Vector3(Random.Range(-xRange,xRange),Random.Range(-yMinRange,yMaxRange),0);
    }
}
