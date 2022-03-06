using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int numOfMovements;
    public GameObject[] arrayOfBlocks;
    public GameObject PausePanel;
    public bool someBlockIsMoving;
    public static bool isPaused;
    public GameObject player;
    public bool playerIsMoving;
    public Text numOfMov;
    public AudioSource morte;
    private bool Dead;

    void Start()
    {
        arrayOfBlocks = GameObject.FindGameObjectsWithTag("movingBlock");

        someBlockIsMoving = false;
        
        numOfMov = GameObject.Find("NumOfMov").GetComponent<Text>();

        player = GameObject.Find("Player");

        Dead = false;

    }

    //=================
    //dir = 1 -> up    
    //dir = 2 -> right 
    //dir = 3 -> down  
    //dir = 4 -> left  
    //=================

    void Update()
    {
        if(Input.GetKey(KeyCode.R) && !Dead){
            Dead = true;
            morte.Play();
            ResetLevel();
        }

        if(Math.Abs(Input.GetAxisRaw("Horizontal")) != 0 || Math.Abs(Input.GetAxisRaw("Vertical")) != 0){
            playerIsMoving = true;
        }
        else{
            playerIsMoving = false;
        }

        someBlockIsMoving = false;
        foreach (GameObject block in arrayOfBlocks)
        {
            if(block.GetComponent<BlockMovement>().isMoving){
                someBlockIsMoving = true;
            }
        }

        numOfMov.text = numOfMovements.ToString();

        if(!someBlockIsMoving && !playerIsMoving){
            if(numOfMovements > 0){
                if(Input.GetKeyUp(KeyCode.UpArrow)){
                    numOfMovements --;
                    for(int g = 0; g < arrayOfBlocks.Length;g++){
                        arrayOfBlocks[g].GetComponent<BlockMovement>().targetPositionSet(1);
                    }
                }
                else if(Input.GetKeyUp(KeyCode.DownArrow)){
                    numOfMovements --;
                    for(int g = 0; g < arrayOfBlocks.Length;g++){
                        arrayOfBlocks[g].GetComponent<BlockMovement>().targetPositionSet(3);
                    }
                }
                else if(Input.GetKeyUp(KeyCode.RightArrow)){
                    numOfMovements --;
                    for(int g = 0; g < arrayOfBlocks.Length;g++){
                        arrayOfBlocks[g].GetComponent<BlockMovement>().targetPositionSet(2);
                    }
                }
                else if(Input.GetKeyUp(KeyCode.LeftArrow)){
                    numOfMovements --;
                    for(int g = 0; g < arrayOfBlocks.Length;g++){
                        arrayOfBlocks[g].GetComponent<BlockMovement>().targetPositionSet(4);
                    }
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused){
                pause();            
            }
            
            else{
                resume();
            }
        }
    }
    public void resume(){
        Time.timeScale = 1;
        isPaused = false;
        PausePanel.gameObject.SetActive(false);
    }
    public void pause(){
        Time.timeScale = 0;
        isPaused = true;
        PausePanel.gameObject.SetActive(true);
    }

    public void ResetLevel(){
        player.GetComponent<PlayerMovement>().anim.SetTrigger("Death");
    }
}
