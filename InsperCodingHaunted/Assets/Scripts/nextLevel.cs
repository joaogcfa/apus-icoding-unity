using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public LayerMask MaskPlayer;
    public bool gotKey;
    public Sprite[] facingDoor;

    public int facingNumber;
    public int nextSceneLoad;

    public AudioSource abertura;
    public AudioSource pegoChave;

    private bool toca;

    public Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        gotKey = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = facingDoor[facingNumber];
        toca = true;
        anim = GameObject.Find("Door").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gotKey){
            if(toca == true){
                pegoChave.Play();
                anim.SetBool("open", true);
                toca = false;
            }
            this.gameObject.layer = 0;
        }
        checkPlayerPos();
    }

    private void checkPlayerPos(){


        Vector3 blockPos = new Vector3 (this.transform.position.x+1/2, this.transform.position.y ,this.transform.position.z);
        Collider2D[] doorCollision = Physics2D.OverlapCircleAll(blockPos,0.05f,MaskPlayer);

        if(doorCollision.Length > 0){

            if(doorCollision[0].tag == "Player"){
                abertura.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }

    }

}
