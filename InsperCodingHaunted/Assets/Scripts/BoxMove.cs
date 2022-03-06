using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    private float moveSpeed = 6.5f;
    private Vector3 pos;
    private Vector3 actualPos;
    
    private void Start() {
        pos = this.transform.position;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);

        actualPos = pos;
    }

    public void MoveBox(Vector3 pos){
        if (transform.position == this.pos){
            this.pos = pos;
        }
    }

}
