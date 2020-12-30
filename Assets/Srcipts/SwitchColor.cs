using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchColor : MonoBehaviour
{
    public ComponentType componentType;
    public Color startColor;
    public Color endColor;
    
    public void SwichObjColor()
    {
        switch (componentType)
        {
            case ComponentType.player:
            {
                //if black
                //change to white
                //else
                //change to black
                GetComponentInChildren<SpriteRenderer>().color =
                    GetComponentInChildren<SpriteRenderer>().color == startColor ? endColor : startColor;
                break;
            }

            case ComponentType.spriteRender:
            {
                GetComponent<SpriteRenderer>().color =
                    GetComponent<SpriteRenderer>().color == startColor ? endColor : startColor;
                break;
            }

            case ComponentType.image:
            {
                GetComponent<Image>().color =
                    GetComponent<Image>().color == startColor ? endColor : startColor;
                break;
            }

            case ComponentType.text:
            {
                GetComponent<Text>().color =
                    GetComponent<Text>().color == startColor ? endColor : startColor;
                break;
            }

            case ComponentType.camera:
            {
                Camera.main.backgroundColor = Camera.main.backgroundColor == startColor ? endColor : startColor;
                break;
            }

            case ComponentType.ground:
            {
                GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;

                GetComponentInChildren<SpriteRenderer>().color =
                    GetComponentInChildren<SpriteRenderer>().color == startColor ? endColor : startColor;
                break;
            }
            
            
        }
    }
    
}

public enum ComponentType
{
    player,spriteRender,image,text,camera,ground
}
