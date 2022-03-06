using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockMovement : MonoBehaviour
{
    public float vel;
    private Vector3 dirVector;
    private Vector3 checkPos;
    public Vector3 pos;
    public bool rightBlockedGlobal;
    public bool leftBlockedGlobal;
    public bool upBlockedGlobal;
    public bool downBlockedGlobal;
    public List<GameObject> listOfBlocks;
    public List<GameObject> listOfBoxesToMove;
    private int numOfBlocks;
    public bool isMoving;

    public bool hasSpike;

    public  void Start()
    {
        pos = this.gameObject.transform.position;
        isMoving = false;
        vel = 6f;
        
        foreach (Transform child in this.transform)
        {
            if(child.CompareTag("unitaryBlock")){
                listOfBlocks.Add(child.gameObject);
            }
            
        }

        numOfBlocks = listOfBlocks.Count;

        dirVector = new Vector3 (0f,0f,0f);
    }

    public void Update()
    {

        if(transform.position != pos){
            isMoving = true;
        }
        else{
            isMoving = false;
        }

        foreach (GameObject box in listOfBoxesToMove)
        {
            box.GetComponent<BoxMove>().MoveBox(box.transform.position + dirVector);
        }
        listOfBoxesToMove.Clear();

        this.transform.position = Vector3.MoveTowards(transform.position, pos, vel * Time.deltaTime);

        rightBlockedGlobal = false;
        leftBlockedGlobal = false;
        upBlockedGlobal = false;
        downBlockedGlobal = false;

        for (int j = 0; j < numOfBlocks; j++){
            if(listOfBlocks[j].GetComponent<BlockCheckPos>().rightBlocked){
                rightBlockedGlobal = true;
            }
            if(listOfBlocks[j].GetComponent<BlockCheckPos>().leftBlocked){
                leftBlockedGlobal = true;
            }
            if(listOfBlocks[j].GetComponent<BlockCheckPos>().upBlocked){
                upBlockedGlobal = true;
            }
            if(listOfBlocks[j].GetComponent<BlockCheckPos>().downBlocked){
                downBlockedGlobal = true;
            }
        }
    }

    public void targetPositionSet(int dir)
    {
        //set directional vector
        //dir = 1 -> up
        //dir = 2 -> right
        //dir = 3 -> down
        //dir = 4 -> left

        //up
        if(dir == 1){
            dirVector = new Vector3 (0,1,0);
            if(!upBlockedGlobal){
                checkPos = dirVector;
            }
            else{
                checkPos = new Vector3(0,0,0);
            }
            foreach (GameObject block in listOfBlocks)
            {
                if(block.GetComponent<BlockCheckPos>().listofBoxesUP.Count > 0){
                    listOfBoxesToMove.Add(block.GetComponent<BlockCheckPos>().listofBoxesUP[0]);
                }    
            }
        }
        //right
        else if(dir == 2){
            dirVector = new Vector3 (1,0,0);
            if(!rightBlockedGlobal){
                checkPos = dirVector;
            }
            else{
                checkPos = new Vector3(0,0,0);
            }
            foreach (GameObject block in listOfBlocks)
            {
                if(block.GetComponent<BlockCheckPos>().listofBoxesRIGHT.Count > 0){
                    listOfBoxesToMove.Add(block.GetComponent<BlockCheckPos>().listofBoxesRIGHT[0]);
                }   
            }
        }
        //down
        else if(dir == 3){
            dirVector = new Vector3 (0,-1,0);
            if(!downBlockedGlobal){
                checkPos = dirVector;
            }
            else{
                checkPos = new Vector3(0,0,0);
            }
            foreach (GameObject block in listOfBlocks)
            {
                if(block.GetComponent<BlockCheckPos>().listofBoxesDOWN.Count > 0){
                    listOfBoxesToMove.Add(block.GetComponent<BlockCheckPos>().listofBoxesDOWN[0]);
                }   
            }
        }
        //left
        else if(dir == 4){
            dirVector = new Vector3 (-1,0,0);
            if(!leftBlockedGlobal){
                checkPos = dirVector;
            }
            else{
                checkPos = new Vector3(0,0,0);
            }
            foreach (GameObject block in listOfBlocks)
            {
                if(block.GetComponent<BlockCheckPos>().listofBoxesLEFT.Count > 0){
                    listOfBoxesToMove.Add(block.GetComponent<BlockCheckPos>().listofBoxesLEFT[0]);
                }   
            }
        }

        pos = this.transform.position + checkPos;
    }
}
