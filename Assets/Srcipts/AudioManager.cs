using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    private void Awake() {
        if (S == null) {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public AudioSource playerSFX;
    public AudioSource stageSFX;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
