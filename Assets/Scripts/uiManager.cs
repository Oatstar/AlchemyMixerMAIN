using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiManager : MonoBehaviour
{

    [SerializeField] TMP_Text playerMoneyText;

    public void RefreshPlayerMoney()
    {
        playerMoneyText.text = GameMasterManager.instance.GetPlayerMoney().ToString();
    }
}
