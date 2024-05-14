using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopItemsSO[] materialItemsSO;
    [SerializeField] private ShopItemsSO[] weaponItemsSO;
    [SerializeField] private ShopItemsSO[] consumablesItemsSO;
    [SerializeField] private ShopItemsSO[] treasureItemsSO;
    [SerializeField] private ItemsTemplate[] itemsTemplate;
    [SerializeField] private ScrollRect scrollRect;
    private int quant = 1;
    private string selectquant = "Select Quantity : ";
    private void Start()
    {
        DisableAllItems();
        LoadMaterialItems();
    }
    public void LoadMaterialItems()=> ConnectSOtoUI(materialItemsSO);    
    public void LoadWeaponItems()=> ConnectSOtoUI(weaponItemsSO);   
    public void LoadConsumablesItems()=> ConnectSOtoUI(consumablesItemsSO);   
    public void LoadTreasureItems()=> ConnectSOtoUI(treasureItemsSO);   

    public void OnClickBuyButton(int i)
    {
        itemsTemplate[i].buyButton.gameObject.SetActive(false);
        itemsTemplate[i].quantityPanel.SetActive(true);       
    }

    public void OnClickCancelButton(int i)
    {
        itemsTemplate[i].buyButton.gameObject.SetActive(true);
        itemsTemplate[i].quantityPanel.SetActive(false);
        ResetQuant(i);
    }
    public void IncreaseQuantity(int i)
    {
        if (quant < int.Parse(itemsTemplate[i].QuantityText.text))
        {
            quant++;
            itemsTemplate[i].selectQuantityText.text = selectquant + quant;
        }
        else
        {
            itemsTemplate[i].selectQuantityText.text = selectquant + quant;
        }
    }
    public void DecreaseQuantity(int i)
    {
        if (quant < 1)
        {
            quant = 1;
            itemsTemplate[i].selectQuantityText.text = selectquant + quant;
        }
        else
        {      
            itemsTemplate[i].selectQuantityText.text = selectquant + quant; 
            quant--;
        }
    }
    public void GenerateCoin()
    {
        if (GameService.Instance.PlayerManager.playerMoney == 0)
        {
            GameService.Instance.PlayerManager.playerMoney = Random.Range(35, 65);
        }
        GameService.Instance.PlayerManager.UpdateCredits();
    }
    public void SellShopItem(ShopItemsSO item)
    {
        
        if(GameService.Instance.PlayerManager.playerMoney > item.buyingPrice && item.quantity>0 && GameService.Instance.PlayerManager.playerCurrentLoad< GameService.Instance.PlayerManager.playerMaxLoad)
        {
            item.quantity--;
            GameService.Instance.PlayerManager.playerMoney = GameService.Instance.PlayerManager.playerMoney - item.buyingPrice;
            GameService.Instance.PlayerManager.playerCurrentLoad = item.weight;
        }
    }
    private void ResetQuant(int i)
    {
        quant = 1;
        itemsTemplate[i].selectQuantityText.text = selectquant + quant;
    }
    private void ResetSCrollRect()=> scrollRect.normalizedPosition = new Vector2(0, 1);
    private void ConnectSOtoUI(ShopItemsSO[] shopItemsSO)
    {
        DisableAllItems();
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            itemsTemplate[i].gameObject.SetActive(true);
            itemsTemplate[i].priceText.text = shopItemsSO[i].buyingPrice.ToString();
            itemsTemplate[i].iconImage.sprite = shopItemsSO[i].icon;
            itemsTemplate[i].descriptionText.text = shopItemsSO[i].itemDescription;
            itemsTemplate[i].itemweightText.text = shopItemsSO[i].weight.ToString();
            itemsTemplate[i].QuantityText.text = shopItemsSO[i].quantity.ToString();
            itemsTemplate[i].rarityText.text = shopItemsSO[i].itemRarity.ToString();
            itemsTemplate[i].itemType = shopItemsSO[i].itemType;
        }
        ResetSCrollRect();       
    }
   private void DisableAllItems()
    {
        for(int i =0; i < itemsTemplate.Length; i++)
        {
            itemsTemplate[i].gameObject.SetActive(false);
        }
    }
}
