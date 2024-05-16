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
    [SerializeField] private TMP_Text playerinventoryLoadText;
    private Dictionary<int, ItemsTemplate> playerInventory = new Dictionary<int, ItemsTemplate>();
    internal int playerMoney;
    internal float playerCurrentLoad = 0;
    internal float playerMaxLoad=80;
    private void Start()
    {
        playerMoney = 0;
    }
    public void UpdateInventory(ItemsTemplate item,int quantity)
    {
        float totalPrice = item.itemSO.buyingPrice * quantity;
        float totalWeight = item.itemSO.weight * quantity;
        if (playerMoney >= totalPrice && (playerCurrentLoad + totalWeight) <= playerMaxLoad)
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
                UpdateCredits();
                UpdatePlayerLoad();
            }
        }
        else {
            
            if(playerMoney < totalPrice)
            {   
                Debug.Log("Broke boi");
            }
            else  if(playerCurrentLoad + totalWeight >= playerMaxLoad)
            {
                Debug.Log("Cant Go Over Max Weight");
            }
        }
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
        }
        else
        {
            RefreshPlayerInventory(playerItem);
            GameService.Instance.ShopManager.ResetBuyButton(playerItem);            
        }
    }

    private void SellPlusRarityBonus(ItemsTemplate playerItem)
    {
        if (playerItem.itemSO.itemRarity == ShopItemsSO.rarity.Common)
        {
            playerMoney += (playerItem.itemSO.buyingPrice * playerItem.itemIncDecQuantity) + 2;
        }
        else if (playerItem.itemSO.itemRarity == ShopItemsSO.rarity.Rare)
        {
            playerMoney += (playerItem.itemSO.buyingPrice * playerItem.itemIncDecQuantity) + 4;
        }
        else if (playerItem.itemSO.itemRarity == ShopItemsSO.rarity.Epic)
        {
            playerMoney += (playerItem.itemSO.buyingPrice * playerItem.itemIncDecQuantity) + 6;
        }
        else
        {
            playerMoney += (playerItem.itemSO.buyingPrice * playerItem.itemIncDecQuantity) + 8;
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
            UpdatePlayerLoad();
        }
        else
        {
            playerInventory.Remove(playerItem.uniqueTemplateID);
            Destroy(playerItem.gameObject);
            UpdateCredits();
            UpdatePlayerLoad();
        }
    }
    public void GenerateCoin()
    {
        if (playerMoney == 0)
        {
            playerMoney += UnityEngine.Random.Range(35, 65);
        }
        UpdateCredits();
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
