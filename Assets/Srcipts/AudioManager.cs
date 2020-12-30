using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    private void Awake() 
    {
        if (S == null) 
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource playerAudio;
    public AudioSource stageAudio;

    public AudioClip[] playerSfx;
    public AudioClip[] stageSfx;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayPlayerSFX(int index)
    {
        playerAudio.clip = playerSfx[index];
        playerAudio.Play();
    }
    
    public void PlayStageSFX(int index)
    {
        stageAudio.clip = stageSfx[index];
        stageAudio.Play();
    }
}
