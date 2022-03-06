using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowshooting : MonoBehaviour
{

    float StartCoolDown;
    float Cooldown;
    public GameObject arrow;
    public Vector3 upDirection;
    public Vector3 downDirection;
    public Vector3 rightDirection;
    public Vector3 leftDirection;

    public Quaternion upDirectionRotation;
    public Quaternion downDirectionRotation;
    public Quaternion rightDirectionRotation;
    public Quaternion leftDirectionRotation;

    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoolDown = 0.2f;
        Cooldown = StartCoolDown;

        upDirection = new Vector3(this.transform.position.x,this.transform.position.y + 1,this.transform.position.z);

        downDirection = new Vector3(this.transform.position.x,this.transform.position.y - 1,this.transform.position.z);

        rightDirection = new Vector3(this.transform.position.x + 1,this.transform.position.y,this.transform.position.z);

        leftDirection = new Vector3(this.transform.position.x - 1,this.transform.position.y,this.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;

        if(Cooldown <= 0 ){
           Launch();
        }
        
    }

    void Launch(){
        arrow.GetComponent<arrowMovement>().facing = 0;
        Instantiate(arrow, upDirection, transform.rotation * Quaternion.Euler (0f, 0f, 90f));
        arrow.GetComponent<arrowMovement>().facing = 1;
        Instantiate(arrow, downDirection, transform.rotation * Quaternion.Euler (0f, 0f, 270f));
        arrow.GetComponent<arrowMovement>().facing = 2;
        Instantiate(arrow, rightDirection, transform.rotation * Quaternion.Euler (0f, 0f, 0f));
        arrow.GetComponent<arrowMovement>().facing = 3;
        Instantiate(arrow, leftDirection, transform.rotation * Quaternion.Euler (0f, 0f, 180f));
        Cooldown = StartCoolDown;
        
    }

    
}
