using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class clickAndSwipe : MonoBehaviour
{
    // Start is called before the first frame update
    // public AudioSource audioSource;
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping=false;
    // public AudioClip swingsound;
    void Start()
    {
      // audioSource=GetComponent<AudioSource>();
    }
    void Awake()
    {
        cam=Camera.main;
        trail=GetComponent<TrailRenderer>();
        col=GetComponent<BoxCollider>();
        trail.enabled=false;
        col.enabled=false;
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
          if(Input.GetMouseButtonDown(0))
          {
            swiping=true;
            updateComponents();
          }
          else if(Input.GetMouseButtonUp(0))
          {
            swiping=false;
            updateComponents();
          }
          if(swiping)
          {
            updateMousePosition();
          }


        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Target>())
        { 
          // gameManager.soundCall();
          //  audioSource.PlayOneShot(swingsound,1.0f);
           
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
    void updateMousePosition()
    {
        mousePos=cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10.0f));
        transform.position=mousePos;

    }
    void updateComponents()
    {
        trail.enabled=swiping;
        col.enabled=swiping;
    }
}
