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
        if (playerMoney > item.itemSO.buyingPrice*quantity  && (playerCurrentLoad+(item.itemSO.weight*quantity)) < playerMaxLoad)
        {
            if (playerInventory.ContainsKey(item.uniqueTemplateID))
            {
                ItemsTemplate existingItem = playerInventory[item.uniqueTemplateID];
                existingItem.tempItemQuantity += quantity;
                existingItem.QuantityText.text = existingItem.tempItemQuantity.ToString();
                existingItem.itemSO.quantity -= quantity;
                playerMoney -= existingItem.itemSO.buyingPrice * quantity;
                playerCurrentLoad += existingItem.itemSO.weight * quantity;
                GameService.Instance.ShopManager.ResetBuyButton(existingItem);
                // Debug.Log(playerCurrentLoad);
                UpdateCredits();
                Debug.Log("Current player item amount : " + existingItem.tempItemQuantity);
                RefreshPlayerInventory(existingItem);
            }
            else
            {
                item.itemSO.quantity -= quantity;
                playerMoney -= item.itemSO.buyingPrice * quantity;
                playerCurrentLoad += quantity * item.itemSO.weight;
                GameObject playerItemPre = Instantiate(playerItemPrefab, parent: playerItemParent.transform);
                ItemsTemplate playerItem = playerItemPre.GetComponent<ItemsTemplate>();               
                SetPlayerItem(playerItem, item, quantity);
                playerInventory.Add(playerItem.uniqueTemplateID, playerItem);
               // Debug.Log(playerCurrentLoad);
                UpdateCredits();
                Debug.Log("Current player item amount : " + playerItem.tempItemQuantity);
            }
        }
        else
        {
            Debug.Log("Cant Go Over Max Weight");
        }
    }
    private void SellInventoryItem(ItemsTemplate playerItem)
    {
        if (playerItem.tempItemQuantity > 0 )
        {
            playerItem.tempItemQuantity -= playerItem.itemIncDecQuantity;
            playerItem.itemSO.quantity += playerItem.itemIncDecQuantity;
            playerCurrentLoad -= playerItem.itemIncDecQuantity*playerItem.itemSO.weight;
            if (playerItem.itemSO.itemRarity == ShopItemsSO.rarity.Common)
            {
                playerMoney += (playerItem.itemSO.buyingPrice * playerItem.itemIncDecQuantity) + 2;
            }
            GameService.Instance.ShopManager.ResetBuyButton(playerItem);          
            Debug.Log("Current player item amount : "+playerItem.tempItemQuantity);
            Debug.Log("Current SO item amount : " + playerItem.itemSO.quantity);
            GameService.Instance.ShopManager.RefreshShopUI(playerItem);
            RefreshPlayerInventory(playerItem);
            Debug.Log("Current Player Load : " + GameService.Instance.PlayerManager.playerCurrentLoad);
        }
        else
        {
            RefreshPlayerInventory(playerItem);
            GameService.Instance.ShopManager.ResetBuyButton(playerItem);
            Debug.Log("Current Player Load : " + GameService.Instance.PlayerManager.playerCurrentLoad);
        }
    }
    private void SetPlayerItem(ItemsTemplate PlayerItem,ItemsTemplate shopItem,int quantity)
    {
        PlayerItem.tempItemQuantity = quantity;
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
        PlayerItem.purhcaseButton.onClick.AddListener(() => SellInventoryItem(PlayerItem));
        GameService.Instance.ShopManager.ResetBuyButton(PlayerItem);
    }
    private void RefreshPlayerInventory(ItemsTemplate playerItem)
    {
        if (playerItem.tempItemQuantity > 0)
        {           
            playerItem.QuantityText.text = playerItem.tempItemQuantity.ToString();
            UpdateCredits();
        }
        else
        {
            playerInventory.Remove(playerItem.uniqueTemplateID);
            Destroy(playerItem.gameObject);
            UpdateCredits();          
        }
    }
    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
}
