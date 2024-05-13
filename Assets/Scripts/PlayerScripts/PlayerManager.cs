using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text playerCreditstext;
    internal int playerMoney;

    private void Start()
    {
        playerMoney = 0;
    }

    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
}
