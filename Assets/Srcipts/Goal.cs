using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Goal");
            AudioManager.S.PlayStageSFX(0);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void ToNextStage()
    {
        GameManager.S.NextStage();
    }
    
    

    public void EnableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
