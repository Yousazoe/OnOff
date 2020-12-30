using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    private void Awake()
    {
        S = this;
    }

    SwitchColor[] colorObjs;
    public static int deadCount;
    public static int starCount;
    public Transform playerRevivePos;

    void Start()
    {
        colorObjs = FindObjectsOfType<SwitchColor>();
        if(UIManager.S != null)
        {
            UIManager.S.RefreshSkullText(deadCount);
            UIManager.S.RefreshStarText(starCount);
        }
    }

    public void SwitchAllColor()
    {
        foreach(SwitchColor colorObj in colorObjs)
        {
            colorObj.SwitchObjColor();
        }
    }

    public void PlayerDie()
    {
        deadCount++;
        UIManager.S.RefreshSkullText(deadCount);

        StartCoroutine(PlayerRevive());
    }

    IEnumerator PlayerRevive()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<PlayerController>().transform.position = playerRevivePos.position;
        FindObjectOfType<PlayerController>().Revive();
    }

    public void NextStage()
    {
        starCount++;
        UIManager.S.RefreshStarText(starCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
