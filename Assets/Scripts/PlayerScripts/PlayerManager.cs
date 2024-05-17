using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text playerCreditstext;
    [SerializeField] private GameObject playerItemPrefab;
    [SerializeField] private GameObject playerItemParent;
    [SerializeField] private TMP_Text playerinventoryLoadText;
    [SerializeField] private ScrollRect scrollRect;

    private Dictionary<int, ItemsTemplate> playerInventory = new Dictionary<int, ItemsTemplate>();
    internal int playerMoney;
    internal float playerCurrentLoad = 0;
    internal float playerMaxLoad=80;

    private void Start()
    {
        playerMoney = 0;
        UpdateCredits();
    }
    public void UpdateInventory(ItemsTemplate item,int quantity)
    {
        int totalPrice = item.itemSO.buyingPrice * quantity;
        float totalWeight = item.itemSO.weight * quantity;                  
        if(playerMoney < totalPrice)
        {
           GameService.Instance.ShowPopupMessage("Not Enough Credits");
           GameService.Instance.SoundManager.PlaySound(Sounds.CantBuyorSellSound);
        }
        else  if(playerCurrentLoad + totalWeight > playerMaxLoad)
        {               
           GameService.Instance.ShowPopupMessage("Cant Exceed Max Weight");
           GameService.Instance.SoundManager.PlaySound(Sounds.CantBuyorSellSound);
        }
        else
        {
           GameService.Instance.SoundManager.PlaySound(Sounds.BuySound);
           AddOrUpdateItem(item,quantity,totalWeight,totalPrice);            
        }        
    }
    private void AddOrUpdateItem(ItemsTemplate item,int quantity,float TotalWeight,int TotalPrice)
    {
        playerMoney -= TotalPrice;
        playerCurrentLoad += TotalWeight;
        if (playerInventory.ContainsKey(item.uniqueTemplateID))
        {
            UpdateExistingItem(item, quantity);
        }
        else
        {
            AddNewItem(item, quantity);
        }
    }
    private void UpdateExistingItem(ItemsTemplate item, int quantity)
    {
        ItemsTemplate existingItem = playerInventory[item.uniqueTemplateID];
        existingItem.tempItemQuantity += quantity;
        existingItem.QuantityText.text = existingItem.tempItemQuantity.ToString();
        existingItem.itemSO.quantity -= quantity;        
        GameService.Instance.ShopManager.ResetBuyButton(existingItem);
        RefreshPlayerInventory(existingItem);
    }  
    private void AddNewItem(ItemsTemplate item,int quantity)
    {
        item.itemSO.quantity -= quantity;      
        GameObject playerItemPre = Instantiate(playerItemPrefab, parent: playerItemParent.transform);
        ItemsTemplate playerItem = playerItemPre.GetComponent<ItemsTemplate>();
        SetPlayerItem(playerItem, item, quantity);
        playerInventory.Add(playerItem.uniqueTemplateID, playerItem);
        UpdateCredits();
        UpdatePlayerLoad();
        GameService.Instance.ShopManager.ResetScrollRect(scrollRect);
    }  
    private void SellInventoryItem(ItemsTemplate playerItem)
    {
        if (playerItem.tempItemQuantity > 0 )
        {
            playerItem.tempItemQuantity -= playerItem.itemIncDecQuantity;
            playerItem.itemSO.quantity += playerItem.itemIncDecQuantity;
            playerCurrentLoad -= playerItem.itemIncDecQuantity*playerItem.itemSO.weight;
            SellPlusRarityBonus(playerItem);
            GameService.Instance.ShopManager.ResetBuyButton(playerItem);                              
            GameService.Instance.ShopManager.RefreshShopUI(playerItem);
            RefreshPlayerInventory(playerItem);
            GameService.Instance.SoundManager.PlaySound(Sounds.SellSound);
        }
        else
        {
            RefreshPlayerInventory(playerItem);
            GameService.Instance.ShopManager.ResetBuyButton(playerItem);            
        }
    }
    private void SellPlusRarityBonus(ItemsTemplate playerItem)
    {
        int bonusCredits = GetRarityBonus(playerItem.itemSO.itemRarity);       
        playerMoney += (playerItem.itemSO.buyingPrice+bonusCredits) * playerItem.itemIncDecQuantity;       
    }
    private int GetRarityBonus(ShopItemsSO.rarity rarity)
    {
        switch (rarity)
        {
            case ShopItemsSO.rarity.Common:
                return 2;               
            case ShopItemsSO.rarity.Rare:
                return 3;                
            case ShopItemsSO.rarity.Epic:
                return 4;
            case ShopItemsSO.rarity.Legendary:
                return 5;
            default:
                return 1;
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
        SetUpButtonListeners(PlayerItem);
        GameService.Instance.ShopManager.ResetBuyButton(PlayerItem);
    }

    private void SetUpButtonListeners(ItemsTemplate PlayerItem)
    { 
        PlayerItem.buyButton.onClick.AddListener(GameService.Instance.ShopManager.OnClickBuyButton);
        PlayerItem.cancelButton.onClick.AddListener(GameService.Instance.ShopManager.OnClickCancelButton);
        PlayerItem.increaseQuantityButton.onClick.AddListener(GameService.Instance.ShopManager.IncreaseQuantity);
        PlayerItem.decreaseQuantityButton.onClick.AddListener(GameService.Instance.ShopManager.DecreaseQuantity);
        PlayerItem.purhcaseButton.onClick.AddListener(() => SellInventoryItem(PlayerItem));
    }
    private void RefreshPlayerInventory(ItemsTemplate playerItem)
    {
        if (playerItem.tempItemQuantity > 0)
        {           
            playerItem.QuantityText.text = playerItem.tempItemQuantity.ToString();
            UpdateCredits();
            UpdatePlayerLoad();
            GameService.Instance.ShopManager.ResetScrollRect(scrollRect);
        }
        else
        {
            RemoveFromInventory(playerItem);
            GameService.Instance.ShopManager.ResetScrollRect(scrollRect);
        }
    }
    private void RemoveFromInventory(ItemsTemplate playerItem)
    {
        playerInventory.Remove(playerItem.uniqueTemplateID);
        Destroy(playerItem.gameObject);
        UpdateCredits();
        UpdatePlayerLoad();
    }
    public void GenerateCoin()
    {
        if (playerMoney == 0 && playerInventory.Count==0)
        {
            playerMoney += UnityEngine.Random.Range(65,75);
            GameService.Instance.SoundManager.PlaySound(Sounds.GenerateMoneySound);
            UpdateCredits();
        }
        else
        {
            GameService.Instance.SoundManager.PlaySound(Sounds.CantBuyorSellSound);
            GameService.Instance.ShowPopupMessage("Cant Generate More Coin");
        }
       
    }
    public void UpdateCredits()
    {
        playerCreditstext.text = "Credits : " + playerMoney;
    }
    private void UpdatePlayerLoad()
    {
        playerinventoryLoadText.text = playerCurrentLoad + "/" + playerMaxLoad;
    }
}
