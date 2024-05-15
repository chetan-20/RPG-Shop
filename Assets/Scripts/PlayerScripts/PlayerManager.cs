using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text playerCreditstext;
    [SerializeField] private GameObject playerItemPrefab;
    [SerializeField] private GameObject playerItemParent;
    internal int playerMoney;
    internal float playerCurrentLoad = 0;
    internal float playerMaxLoad=100;
    private void Start()
    {
        playerMoney = 0;
    }
    public void UpdateInventory(ItemsTemplate item)
    {
        if (playerMoney > item.itemSO.buyingPrice && item.itemSO.quantity> 0 && playerCurrentLoad < playerMaxLoad)
        {
            item.itemSO.quantity--;
            playerMoney -= item.itemSO.buyingPrice;
            playerCurrentLoad += item.itemSO.weight;
            GameObject playerItemPre = Instantiate(playerItemPrefab, parent: playerItemParent.transform);
            ItemsTemplate playerItem = playerItemPre.GetComponent<ItemsTemplate>();          
            SetPlayerItem(playerItem, item);
            UpdateCredits();
        }
    }

    private void SetPlayerItem(ItemsTemplate PlayerItem,ItemsTemplate shopItem)
    {
        PlayerItem.priceText.text = shopItem.priceText.text;       
        PlayerItem.itemweightText.text = shopItem.itemweightText.text;       
        PlayerItem.descriptionText.text = shopItem.descriptionText.text;        
        PlayerItem.iconImage.sprite = shopItem.iconImage.sprite;
        PlayerItem.rarityText.text = shopItem.rarityText.text;      
    }
    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
}
