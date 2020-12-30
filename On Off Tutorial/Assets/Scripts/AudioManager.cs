using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    private void Awake()
    {
        if (S == null)
            S = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource playerAu;
    public AudioSource stageAu;

    public AudioClip[] playerSfx;
    public AudioClip[] stageSfx;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayPlayerSFX(int index)
    {
        playerAu.clip = playerSfx[index];
        playerAu.Play();
    }

    public void PlayStageSFX(int index)
    {
        stageAu.clip = stageSfx[index];
        stageAu.Play();
    }
}
