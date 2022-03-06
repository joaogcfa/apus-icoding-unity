using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheckPos : MonoBehaviour
{
    public bool rightBlocked;
    public bool leftBlocked;
    public bool upBlocked;
    public bool downBlocked;

    public bool rightBlockedParent;
    public bool leftBlockedParent;
    public bool upBlockedParent;
    public bool downBlockedParent;

    public LayerMask obstacleMask;
    public LayerMask playerMask;
    public LayerMask boxMask;

    public List<GameObject> listofBoxesUP;
    public List<GameObject> listofBoxesDOWN;
    public List<GameObject> listofBoxesRIGHT;
    public List<GameObject> listofBoxesLEFT;

    public Collider2D boxCollisionUp;
    public Collider2D boxCollisionDown;
    public Collider2D boxCollisionLeft;
    public Collider2D boxCollisionRight;
 
    public virtual void Start()
    {
        rightBlocked = false;
        leftBlocked = false;
        upBlocked = false;
        downBlocked = false;
    }

    public virtual void Update()
    {
        upBlockedParent = this.transform.parent.gameObject.GetComponent<BlockMovement>().upBlockedGlobal;
        downBlockedParent = this.transform.parent.gameObject.GetComponent<BlockMovement>().downBlockedGlobal;
        rightBlockedParent = this.transform.parent.gameObject.GetComponent<BlockMovement>().rightBlockedGlobal;
        leftBlockedParent = this.transform.parent.gameObject.GetComponent<BlockMovement>().leftBlockedGlobal;

        checkBlockedPos(); 
    }

    private void checkBlockedPos(){

        upBlocked = false;
        downBlocked = false;
        leftBlocked = false;
        rightBlocked = false;
        listofBoxesRIGHT.Clear();
        listofBoxesUP.Clear();
        listofBoxesLEFT.Clear();
        listofBoxesDOWN.Clear();

        //UP
        Vector3 blockPos = new Vector3 (this.transform.position.x,this.transform.position.y + 1,this.transform.position.z);

        Collider2D[] blockCollisionUp = Physics2D.OverlapCircleAll(blockPos,0.05f,obstacleMask);

        Collider2D[] playerCollisionUp = Physics2D.OverlapCircleAll(blockPos,0.05f,playerMask);

        boxCollisionUp = Physics2D.OverlapCircle(blockPos,0.05f,boxMask);

        Vector3 blockPosX2 = new Vector3 (this.transform.position.x,this.transform.position.y + 2,this.transform.position.z);

        Collider2D blockCollisionUpX2 = Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask);

        if (boxCollisionUp != null){
            if(!Physics2D.OverlapCircle(blockPosX2,0.05f,boxMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,playerMask)){
                if(!upBlockedParent){
                    listofBoxesUP.Add(boxCollisionUp.gameObject);
                }
            }
            else if (blockCollisionUpX2 != null){
                if(blockCollisionUpX2.tag == "wall" || blockCollisionUpX2.tag == "door"){
                    upBlocked = true;
                }
                else if(blockCollisionUpX2.tag == "unitaryBlock"){
                    if(this.transform.parent != blockCollisionUpX2.transform.parent){
                        if(blockCollisionUpX2.gameObject.GetComponent<BlockCheckPos>().upBlockedParent){
                            upBlocked = true;
                        } 
                        else{
                            if(!upBlockedParent){
                                listofBoxesUP.Add(boxCollisionUp.gameObject);
                            }
                        }
                    }
                }
            }    
            else{
                upBlocked = true;
            }
        }

        if(!this.transform.parent.GetComponent<BlockMovement>().hasSpike && playerCollisionUp.Length > 0){
            if(playerCollisionUp[0].tag == "Player"){
                upBlocked = true;
            }
        }

        if(blockCollisionUp.Length > 0){

            if(blockCollisionUp[0].tag == "wall"  || blockCollisionUp[0].tag == "door"){
                upBlocked = true;
            }
            if(blockCollisionUp[0].tag == "unitaryBlock"){
                if(this.transform.parent != blockCollisionUp[0].transform.parent){
                    if(blockCollisionUp[0].gameObject.GetComponent<BlockCheckPos>().upBlockedParent){
                        upBlocked = true;
                    } 
                }
                else{
                    if(blockCollisionUp[0].gameObject.GetComponent<BlockCheckPos>().upBlocked){
                        upBlocked = true;
                    }   
                }  
            }
        }
        //========================

        //DOWN
        blockPos = new Vector3 (this.transform.position.x,this.transform.position.y - 1,this.transform.position.z);

        Collider2D[] blockCollisionDown = Physics2D.OverlapCircleAll(blockPos,0.05f,obstacleMask);

        Collider2D[] playerCollisionDown = Physics2D.OverlapCircleAll(blockPos,0.05f,playerMask);

        boxCollisionDown = Physics2D.OverlapCircle(blockPos,0.05f,boxMask);

        blockPosX2 = new Vector3 (this.transform.position.x,this.transform.position.y - 2,this.transform.position.z);

        Collider2D blockCollisionDownX2 = Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask);

        if (boxCollisionDown != null){
            if(!Physics2D.OverlapCircle(blockPosX2,0.05f,boxMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,playerMask)){
                if(!downBlockedParent){
                    listofBoxesDOWN.Add(boxCollisionDown.gameObject);
                }
            }
            else if (blockCollisionDownX2 != null){
                if(blockCollisionDownX2.tag == "wall"  || blockCollisionDownX2.tag == "door"){
                    downBlocked = true;
                }
                else if(blockCollisionDownX2.tag == "unitaryBlock"){
                    if(this.transform.parent != blockCollisionDownX2.transform.parent){
                        if(blockCollisionDownX2.gameObject.GetComponent<BlockCheckPos>().downBlockedParent){
                            downBlocked = true;
                        } 
                        else{
                            if(!downBlockedParent){
                                listofBoxesDOWN.Add(boxCollisionDown.gameObject);
                            }   
                        }
                    }
                }
            }    
            else{
                downBlocked = true;
            }   
        }

        if(!this.transform.parent.GetComponent<BlockMovement>().hasSpike && playerCollisionDown.Length > 0){
            if(playerCollisionDown[0].tag == "Player"){
                downBlocked = true;
            }
        }

        if(blockCollisionDown.Length > 0){

            if(blockCollisionDown[0].tag == "wall"  || blockCollisionDown[0].tag == "door"){
                downBlocked = true;
            }
            if(blockCollisionDown[0].tag == "unitaryBlock"){
                if(this.transform.parent != blockCollisionDown[0].transform.parent){
                    if(blockCollisionDown[0].gameObject.GetComponent<BlockCheckPos>().downBlockedParent){
                        downBlocked = true;
                    } 
                }
                else{
                    if(blockCollisionDown[0].gameObject.GetComponent<BlockCheckPos>().downBlocked){
                        downBlocked = true;
                    }   
                }   
            }
        }
        //========================

        //RIGHT
        blockPos = new Vector3 (this.transform.position.x + 1,this.transform.position.y,this.transform.position.z);

        Collider2D[] blockCollisionRight = Physics2D.OverlapCircleAll(blockPos,0.05f,obstacleMask);

        Collider2D[] playerCollisionRight = Physics2D.OverlapCircleAll(blockPos,0.05f,playerMask);

        boxCollisionRight = Physics2D.OverlapCircle(blockPos,0.05f,boxMask);

        blockPosX2 = new Vector3 (this.transform.position.x + 2,this.transform.position.y,this.transform.position.z);

        Collider2D blockCollisionRightX2 = Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask);

        if (boxCollisionRight != null){
            if(!Physics2D.OverlapCircle(blockPosX2,0.05f,boxMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,playerMask)){
                if(!rightBlockedParent){
                    listofBoxesRIGHT.Add(boxCollisionRight.gameObject);
                }
            }
            else if (blockCollisionRightX2 != null){
                if(blockCollisionRightX2.tag == "wall" || blockCollisionRightX2.tag == "door"){
                    rightBlocked = true;
                }
                else if(blockCollisionRightX2.tag == "unitaryBlock"){
                    if(this.transform.parent != blockCollisionRightX2.transform.parent){
                        if(blockCollisionRightX2.gameObject.GetComponent<BlockCheckPos>().rightBlockedParent){
                            rightBlocked = true;
                        } 
                        else{
                            if(!rightBlockedParent){
                                listofBoxesRIGHT.Add(boxCollisionRight.gameObject);
                            }   
                        }
                    }
                }
            }    
            else{
                rightBlocked = true;
            }      
        }

        if(!this.transform.parent.GetComponent<BlockMovement>().hasSpike && playerCollisionRight.Length > 0){
            if(playerCollisionRight[0].tag == "Player"){
                rightBlocked = true;
            }
        }

        if(blockCollisionRight.Length > 0){

            if(blockCollisionRight[0].tag == "wall"  || blockCollisionRight[0].tag == "door"){
                rightBlocked = true;
            }
            if(blockCollisionRight[0].tag == "unitaryBlock"){
                if(this.transform.parent != blockCollisionRight[0].transform.parent){
                    if(blockCollisionRight[0].gameObject.GetComponent<BlockCheckPos>().rightBlockedParent){
                        rightBlocked = true;
                    } 
                }
                else{
                    if(blockCollisionRight[0].gameObject.GetComponent<BlockCheckPos>().rightBlocked){
                        rightBlocked = true;
                    }  
                }    
            }
        }
        //========================

        //LEFT
        blockPos = new Vector3 (this.transform.position.x - 1,this.transform.position.y,this.transform.position.z);

        Collider2D[] blockCollisionLeft = Physics2D.OverlapCircleAll(blockPos,0.05f,obstacleMask);

        Collider2D[] playerCollisionLeft = Physics2D.OverlapCircleAll(blockPos,0.05f,playerMask);

        boxCollisionLeft = Physics2D.OverlapCircle(blockPos,0.05f,boxMask);

        blockPosX2 = new Vector3 (this.transform.position.x - 2,this.transform.position.y,this.transform.position.z);

        Collider2D blockCollisionLeftX2 = Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask);

        if (boxCollisionLeft != null){
            if(!Physics2D.OverlapCircle(blockPosX2,0.05f,boxMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,obstacleMask) && !Physics2D.OverlapCircle(blockPosX2,0.05f,playerMask)){
                if(!leftBlockedParent){
                    listofBoxesLEFT.Add(boxCollisionLeft.gameObject);
                }
                
            }
            else if (blockCollisionLeftX2 != null){
                if(blockCollisionLeftX2.tag == "wall" || blockCollisionLeftX2.tag == "door"){
                    leftBlocked = true;
                }
                else if(blockCollisionLeftX2.tag == "unitaryBlock"){
                    if(this.transform.parent != blockCollisionLeftX2.transform.parent){
                        if(blockCollisionLeftX2.gameObject.GetComponent<BlockCheckPos>().leftBlockedParent){
                            leftBlocked = true;
                        } 
                        else{
                            if(!leftBlockedParent){
                                listofBoxesLEFT.Add(boxCollisionLeft.gameObject);
                            }     
                        }
                    }
                }
            }    
            else{
                leftBlocked = true;
            }     
        }

        if(!this.transform.parent.GetComponent<BlockMovement>().hasSpike && playerCollisionLeft.Length > 0){
            if(playerCollisionLeft[0].tag == "Player"){
                leftBlocked = true;
            }
        }

        if(blockCollisionLeft.Length > 0){

            if(blockCollisionLeft[0].tag == "wall" || blockCollisionLeft[0].tag == "door"){
                leftBlocked = true;
            }
            if(blockCollisionLeft[0].tag == "unitaryBlock"){
                if(this.transform.parent != blockCollisionLeft[0].transform.parent){
                    if(blockCollisionLeft[0].gameObject.GetComponent<BlockCheckPos>().leftBlockedParent){
                        leftBlocked = true;
                    } 
                }
                else{
                    if(blockCollisionLeft[0].gameObject.GetComponent<BlockCheckPos>().leftBlocked){
                        leftBlocked = true;
                    } 
                }     
            }
        }
        //========================

    }
}
