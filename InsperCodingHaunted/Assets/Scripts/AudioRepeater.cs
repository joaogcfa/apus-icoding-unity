using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioRepeater : MonoBehaviour
{
   public static AudioRepeater instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
