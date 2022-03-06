using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask colisionMask;
    public float speed = 1;
    public Rigidbody2D rb;

    public int facing = -1;


    void Start()
    {
        if (facing == 0){
            rb.velocity = new Vector2(0, speed);
        }
        if (facing == 1){
            rb.velocity = new Vector2(0, -speed);
        }
        if (facing == 2){
            rb.velocity = new Vector2(speed, 0);
        }
        if (facing == 3){
            rb.velocity = new Vector2(-speed, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // while(facing == -1){
        //     x++;
        // }
        // launchArrow();
        checkWallHit();
        
    }

    private void checkWallHit(){

        Vector3 blockPos = new Vector3 (this.transform.position.x, this.transform.position.y ,this.transform.position.z);
        Collider2D[] arrowCollision = Physics2D.OverlapCircleAll(blockPos,0.05f,colisionMask);

        if(arrowCollision.Length > 0){

            if(arrowCollision[0].tag == "wall" || arrowCollision[0].tag == "unitaryBlock" || arrowCollision[0].tag == "box"){
                Destroy(this.gameObject);
            }
        } 
    }
    private void launchArrow(){
        if (facing == 0){
            rb.velocity = new Vector2(speed, 0);
        }
        if (facing == 1){
            rb.velocity = new Vector2(0, speed);
        }
        if (facing == 2){
            rb.velocity = new Vector2(0, speed);
        }
        if (facing == 3){
            rb.velocity = new Vector2(0, speed);
        }
    }
}
