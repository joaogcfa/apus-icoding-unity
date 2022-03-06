using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonpress : MonoBehaviour
{
    public LayerMask boxMask;

    public GameObject spikesA;
    public GameObject spikesB;



    // Start is called before the first frame update
    void Start()
    {
        spikesA = GameObject.Find("SpikesA");
        spikesB = GameObject.Find("SpikesB");
    }

    // Update is called once per frame
    void Update()
    {
        checkButtonPressed();
        
    }

    private void checkButtonPressed(){

        Vector3 buttonPos = new Vector3 (this.transform.position.x, this.transform.position.y ,this.transform.position.z);
        Collider2D[] butCollision = Physics2D.OverlapCircleAll(buttonPos,0.05f,boxMask);

        if(butCollision.Length > 0){

            if(butCollision[0].tag == "box"){
                spikesA.SetActive(false);
                spikesB.SetActive(true);
            }
        }
        else{
            spikesA.SetActive(true);
            spikesB.SetActive(false);
        }   
    }
}