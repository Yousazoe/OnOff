using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public static int deadCount;
    public static int starCount;
    
    private void Awake()
    {
        S = this;
    }


    private SwitchColor[] colorObjs;

    void Start()
    {
        colorObjs = FindObjectsOfType<SwitchColor>();

        if (UIManager.S != null)
        {
            UIManager.S.RefreshSkullText(deadCount);
            UIManager.S.RefreshStarText(starCount);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    
    public void SwitchAllColor()
    {
        foreach (SwitchColor colorObj in colorObjs)
        {
            colorObj.SwichObjColor();
        }
    }

    public void PlayerDie()
    {
        deadCount++;
        UIManager.S.RefreshSkullText(deadCount);

        StartCoroutine(PlayerRevive());
    }



    public Transform playerRevivewPos;
    IEnumerator PlayerRevive()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<PlayerController>().transform.position = playerRevivewPos.position;
        FindObjectOfType<PlayerController>().Revive();
    }

    public void NextStage()
    {
        starCount++;
        UIManager.S.RefreshStarText(starCount);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    
}
