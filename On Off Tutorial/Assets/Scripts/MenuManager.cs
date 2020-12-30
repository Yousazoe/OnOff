using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    int selectIndex;
    public Transform[] selectItem;

    Color selectColor;
    Color nonSelectColor;
    public Color selectItemStartColor;
    public Color selectItemEndColor;
    public Color nonSelectItemStartColor;
    public Color nonSelectItemEndColor;

    private void Start()
    {
        selectColor = selectItemStartColor;
        nonSelectColor = selectItemEndColor;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectIndex = (selectIndex - 1) % selectItem.Length >= 0 ? (selectIndex - 1) % selectItem.Length : selectItem.Length - 1;
            RenewSelectItem();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectIndex = (selectIndex + 1) % selectItem.Length;
            RenewSelectItem();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.S.SwitchAllColor();

            if(selectColor == selectItemStartColor)
            {
                selectColor = nonSelectItemEndColor;
                nonSelectColor = nonSelectItemStartColor;
            }
            else
            {
                selectColor = selectItemStartColor;
                nonSelectColor = selectItemEndColor;
            }

            RenewSelectItem();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        string loadSceneName = "";
        switch(selectIndex)
        {
            case 0:
                {
                    loadSceneName = "Level001";
                    break;
                }
            /*case 1:
                {
                    loadSceneName = "Control";
                    break;
                }*/
        }

        SceneManager.LoadScene(loadSceneName);
    }

    void RenewSelectItem()
    {
        for (int i = 0; i < selectItem.Length; i++)
        {
            if (i == selectIndex)
            {
                selectItem[i].GetComponent<Text>().color = selectColor;

                foreach (Transform t in selectItem[i].transform)
                {
                    t.gameObject.SetActive(true);
                    t.GetComponent<Image>().color = selectColor;
                }
            }
            else
            {
                selectItem[i].GetComponent<Text>().color = nonSelectColor;

                foreach (Transform t in selectItem[i].transform)
                {
                    t.GetComponent<Image>().color = nonSelectColor;
                    t.gameObject.SetActive(false);
                }
            }
        }
    }
}
