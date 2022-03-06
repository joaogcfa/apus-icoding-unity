using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausePanel : MonoBehaviour
{
     // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume(){
        GameObject.Find("GameManager").GetComponent<gameManager>().resume();
    }

    public void menu(){
        // Destroy(GameObject.Find("Music"));
        GameObject.Find("GameManager").GetComponent<gameManager>().resume();
        SceneManager.LoadScene("menu");
    }
}
