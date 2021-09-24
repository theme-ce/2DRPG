using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("References:")]
    public PlayerStatus status;
    public PlayerLevel level;

    [Header("HP & MP Bar:")]
    public Text hpBarText;
    public Image hpBar;
    public Text mpBarText;
    public Image mpBar;

    [Header("Status Window:")]
    public Text strText;
    public Text dexText;
    public Text vitText;
    public Text intText;
    public Text lukText;
    public Text hpText;
    public Text mpText;
    public Text atkText;
    public Text defText;
    public Text critText;

    [Header("Level Bar")]
    public Text expBarText;
    public Image expBar;
    public Text levelText;

    void Update()
    {
        HPandMPBar();
        StatusWindow();
        LevelandEXPBar();
    }

    void HPandMPBar()
    {
        hpBarText.text = status.currentHp.ToString() + " / " + status.GetHP.ToString();
        hpBar.fillAmount = status.currentHp / status.GetHP;

        mpBarText.text = status.currentMP.ToString() + " / " + status.GetMP.ToString();
        mpBar.fillAmount = status.currentMP / status.GetMP;
    }

    void StatusWindow()
    {
        strText.text = "Strength : " + status.GetSTR.ToString();
        dexText.text = "Dexterity : " + status.GetDEX.ToString();
        vitText.text = "Vitality : " + status.GetVIT.ToString();
        intText.text = "Intellect : " + status.GetINT.ToString();
        lukText.text = "Lucky : " + status.GetLUK.ToString();
        hpText.text = "HP : " + status.GetHP.ToString();
        mpText.text = "MP : " + status.GetMP.ToString();
        atkText.text = "Attack : " + status.GetATK.ToString();
        defText.text = "Defense : " + status.GetDEF.ToString();
        critText.text = "Critical : " + status.GetCRIT.ToString() + " %";
    }

    void LevelandEXPBar()
    {
        expBarText.text = level.currentExp + " / " + level.maxEXP;
        expBar.fillAmount = level.currentExp / level.maxEXP;
        levelText.text = level.level.ToString();
    }
}
