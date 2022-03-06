using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class getkey : MonoBehaviour
{

    public LayerMask Player;
    public Image noKey;
    public Image withKey;
    public AudioSource audiosource;
    

    // Start is called before the first frame update
    void Start()
    {
        withKey = GameObject.Find("WithKey").GetComponent<Image>();
        noKey = GameObject.Find("NoKey").GetComponent<Image>();
        withKey.enabled = false;

        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerGotKey();
    }

    private void checkPlayerGotKey(){

        Vector3 blockPos = new Vector3 (this.transform.position.x, this.transform.position.y ,this.transform.position.z);
        Collider2D[] KeyCollision = Physics2D.OverlapCircleAll(blockPos,0.05f,Player);

        if(KeyCollision.Length > 0){

            if(KeyCollision[0].tag == "Player"){
                GameObject.Find("Door").GetComponent<nextLevel>().gotKey = true;
                noKey.enabled = false;
                withKey.enabled = true;

                audiosource.Play();

                GameObject.Destroy(GameObject.Find("Key"));
            }
        } 
    }
}
