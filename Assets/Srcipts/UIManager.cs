using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager S;

    private void Awake()
    {
        S = this;
    }

    public Text skullText;
    public Text startText;

    public void RefreshSkullText(int amount)
    {
        skullText.text = amount.ToString();
    }

    public void RefreshStarText(int amount)
    {
        startText.text = amount.ToString();
    }

}
