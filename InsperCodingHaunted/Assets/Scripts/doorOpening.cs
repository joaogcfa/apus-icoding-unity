using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpening : MonoBehaviour
{

    public bool opened;
    public GameObject currentGameObj;
    public float alpha = 0.5f;
    public float beta = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentGameObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (opened){
            changeAlpha(GetComponent<Renderer>().material, alpha);
        }
        else{
            // changeAlpha(GetComponent<Renderer>().material, beta);
        }
        
    }
    void changeAlpha(Material mat, float alphaVal)
    {
        Color oldcolor = mat.color;
        Color newColor = new Color(oldcolor.r, oldcolor.g, oldcolor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
