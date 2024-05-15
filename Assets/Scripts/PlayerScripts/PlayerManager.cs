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
    private Dictionary<int, ItemsTemplate> playerInventory = new Dictionary<int, ItemsTemplate>();
    internal int playerMoney;
    internal float playerCurrentLoad = 0;
    internal float playerMaxLoad=65;
    private void Start()
    {
        playerMoney = 0;
    }
    public void UpdateInventory(ItemsTemplate item,int quantity)
    {
        if (playerMoney > item.itemSO.buyingPrice*quantity && item.itemSO.quantity> 0 && playerCurrentLoad < playerMaxLoad)
        {
            if (playerInventory.ContainsKey(item.uniqueTemplateID))
            {
                ItemsTemplate existingItem = playerInventory[item.uniqueTemplateID];
                existingItem.playerTempItemQuantity += quantity;
                existingItem.QuantityText.text = existingItem.playerTempItemQuantity.ToString();
                existingItem.itemSO.quantity -= quantity;
                playerMoney -= existingItem.itemSO.buyingPrice * quantity;
                playerCurrentLoad += existingItem.itemSO.weight * quantity;                
                Debug.Log(playerCurrentLoad);
                UpdateCredits();
            }
            else
            {
                item.itemSO.quantity -= quantity;
                playerMoney -= item.itemSO.buyingPrice * quantity;
                playerCurrentLoad += quantity * item.itemSO.weight;
                GameObject playerItemPre = Instantiate(playerItemPrefab, parent: playerItemParent.transform);
                ItemsTemplate playerItem = playerItemPre.GetComponent<ItemsTemplate>();
                playerItem.playerTempItemQuantity = quantity;
                SetPlayerItem(playerItem, item, quantity);
                playerInventory.Add(playerItem.uniqueTemplateID, playerItem);
                Debug.Log(playerCurrentLoad);
                UpdateCredits();
            }
        }
    }
    private void SellInventoryItem(ItemsTemplate playerItem,int quant)
    {
        if (playerItem.playerTempItemQuantity > 0)
        {
            playerItem.playerTempItemQuantity -= quant;
            playerItem.itemSO.quantity += quant;
            if (playerItem.itemSO.itemRarity == ShopItemsSO.rarity.Common)
            {
                playerMoney += (playerItem.itemSO.buyingPrice * quant) + 2;
            }
        }
    }
    private void SetPlayerItem(ItemsTemplate PlayerItem,ItemsTemplate shopItem,int quantity)
    {
        PlayerItem.itemSO = shopItem.itemSO;
        PlayerItem.priceText.text = shopItem.priceText.text;       
        PlayerItem.itemweightText.text = shopItem.itemweightText.text;       
        PlayerItem.descriptionText.text = shopItem.descriptionText.text;        
        PlayerItem.iconImage.sprite = shopItem.iconImage.sprite;
        PlayerItem.rarityText.text = shopItem.rarityText.text;
        PlayerItem.QuantityText.text = quantity.ToString();
        PlayerItem.uniqueTemplateID = shopItem.uniqueTemplateID;
        PlayerItem.buyButton.onClick.AddListener(GameService.Instance.ShopManager.OnClickBuyButton);
        PlayerItem.cancelButton.onClick.AddListener(GameService.Instance.ShopManager.OnClickCancelButton);
        PlayerItem.increaseQuantityButton.onClick.AddListener(GameService.Instance.ShopManager.IncreaseQuantity);
        PlayerItem.decreaseQuantityButton.onClick.AddListener(GameService.Instance.ShopManager.DecreaseQuantity);
        PlayerItem.purhcaseButton.onClick.AddListener(() => SellInventoryItem(PlayerItem, quantity));
    }
    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
}
