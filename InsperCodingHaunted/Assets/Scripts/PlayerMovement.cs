using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 targetPos;
    public float moveSpeed;
    public bool playerIsDead;
    public LayerMask whatStopsMovement;
    public LayerMask killsPLayer;
    public LayerMask whatYouCanMove;
    public GameObject GM;

    public Animator anim;
    public bool isMoving;

    public Collider2D boxCheckCollision;
    public GameObject box;

    public AudioSource morte;

    private float direction;

    void Start()
    {
        targetPos = this.transform.position;
        moveSpeed = 6f;
        GM = GameObject.Find("GameManager");
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        animSetBools();

        Collider2D[] CollisionDeathBug = Physics2D.OverlapCircleAll(transform.position, 0.005f, killsPLayer);

		if(CollisionDeathBug.Length > 0 && !playerIsDead){
            playerIsDead = true;
			anim.SetTrigger("Death");
            morte.Play();
		}

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (transform.position == targetPos){
            //===============================================================================================================
            //movimento horizontal
            //===============================================================================================================
            if (Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f && (Math.Abs(Input.GetAxisRaw("Vertical")) == 0f)){

                boxCheckCollision = Physics2D.OverlapCircle(targetPos + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatYouCanMove);
                
                //se houver uma caixa na direção em que o char pretende se mover
                if (boxCheckCollision != null)
                {
                    //se não houver parede na direção em que a caixa vai ser movida
                    //se não houver outra caixa na direção em que a caixa vai ser movida
                    if ((!Physics2D.OverlapCircle(targetPos + new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f), .2f, whatStopsMovement)) && (!Physics2D.OverlapCircle(targetPos + new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f), .2f, whatYouCanMove)))
                    {
                        //atualiza a posição da caixa e do char
                        box = boxCheckCollision.gameObject;
                        box.GetComponent<BoxMove>().MoveBox(targetPos + new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f));
                        targetPos += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                }
                if (!Physics2D.OverlapCircle(targetPos + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement) && boxCheckCollision == null)
                {
                    //move
                    targetPos += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }


            //===============================================================================================================
            //movimento vertical
            //===============================================================================================================
            else if (Math.Abs(Input.GetAxisRaw("Vertical")) == 1f && (Math.Abs(Input.GetAxisRaw("Horizontal")) == 0f))
            {

                boxCheckCollision = Physics2D.OverlapCircle(targetPos + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatYouCanMove);
                
                //se houver uma caixa na direção em que o char pretende se mover
                if (boxCheckCollision != null)
                {
                    //se não houver parede na direção em que a caixa vai ser movida
                    //se não houver outra caixa na direção em que a caixa vai ser movida
                    if ((!Physics2D.OverlapCircle(targetPos + new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f), .2f, whatStopsMovement)) && (!Physics2D.OverlapCircle(targetPos + new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f), .2f, whatYouCanMove)))
                    {
                        //atualiza a posição da caixa e do char
                        box = boxCheckCollision.gameObject;
                        box.GetComponent<BoxMove>().MoveBox(targetPos + new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f));
                        targetPos += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }
                }

                if (!Physics2D.OverlapCircle(targetPos + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement) && boxCheckCollision == null)
                {   
                    //move
                    targetPos += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }

        
        
    }

    public void animSetBools(){

        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0){
            isMoving = true;
        }
        else{
            isMoving = false;
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

        anim.SetBool("isMoving", isMoving);

        if(isMoving){
            if(Input.GetAxisRaw("Vertical") == 1){
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
            }
            else if (Input.GetAxisRaw("Vertical") == -1){
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
            }
            else if (Input.GetAxisRaw("Horizontal") == 1){
                anim.SetBool("Right", true);
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Left", false);
            }
            else if (Input.GetAxisRaw("Horizontal") == -1){
                anim.SetBool("Left", true);
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
            }
        }
    }

    public void PlayerDeath(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
