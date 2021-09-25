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
    public Text agiText;
    public Text dexText;
    public Text vitText;
    public Text intText;
    public Text lukText;
    public Text hpText;
    public Text mpText;
    public Text atkText;
    public Text defText;
    public Text critText;
    public Text critDmgText;
    public Text hitText;
    public Text fleeText;
    public Text atkSpeedText;

    [Header("Level Bar:")]
    public Text expBarText;
    public Image expBar;
    public Text levelText;

    [Header("Menu Bar:")]
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject statusUI;

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
        agiText.text = "Agility : " + status.GetAGI.ToString();
        dexText.text = "Dexterity : " + status.GetDEX.ToString();
        vitText.text = "Vitality : " + status.GetVIT.ToString();
        intText.text = "Intellect : " + status.GetINT.ToString();
        lukText.text = "Lucky : " + status.GetLUK.ToString();
        hpText.text = "HP : " + status.GetHP.ToString();
        mpText.text = "MP : " + status.GetMP.ToString();
        atkText.text = "Attack : " + status.GetATK.ToString();
        defText.text = "Defense : " + status.GetDEF.ToString();
        critText.text = "Critical : " + status.GetCRIT.ToString() + " %";
        critDmgText.text = "Critical Damage : " + status.GetCRITDMG.ToString() + " %";
        hitText.text = "Hit : " + status.GetHIT.ToString() + " %";
        fleeText.text = "Flee : " + status.GetFLEE.ToString() + " %";
        atkSpeedText.text = "Attack Speed : " + status.GetATKSPEED.ToString();
    }

    void LevelandEXPBar()
    {
        expBarText.text = level.currentExp + " / " + level.maxEXP;
        expBar.fillAmount = level.currentExp / level.maxEXP;
        levelText.text = level.level.ToString();
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void OpenEquipment()
    {
        equipmentUI.SetActive(!equipmentUI.activeSelf);
    }

    public void OpenStatus()
    {
        statusUI.SetActive(!statusUI.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
