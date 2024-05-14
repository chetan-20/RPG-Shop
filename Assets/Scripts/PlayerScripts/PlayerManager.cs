using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text playerCreditstext;
    [SerializeField] private GameObject playerItemPrefab;
    internal int playerMoney;
    internal float playerCurrentLoad = 0;
    internal float playerMaxLoad=100;
    private void Start()
    {
        playerMoney = 0;
    }

    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
}
