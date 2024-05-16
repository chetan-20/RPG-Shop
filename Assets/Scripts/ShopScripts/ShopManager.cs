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
    private void ResetSCrollRect()=> scrollRect.normalizedPosition = new Vector2(0, 1);
    public void OnClickBuyButton()
    {     
          
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();      
        GameObject parentGameObject = button.transform.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
        ResetQuant(itemsTemplate);
        itemsTemplate.buyButton.gameObject.SetActive(false);
        itemsTemplate.quantityPanel.SetActive(true);
        
    }
    public void OnClickCancelButton()
    {
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
        itemsTemplate.buyButton.gameObject.SetActive(true);
        itemsTemplate.quantityPanel.SetActive(false);
        ResetQuant(itemsTemplate);
    }
    public void IncreaseQuantity()
    {
       
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
        if (itemsTemplate.itemIncDecQuantity<itemsTemplate.tempItemQuantity)
        {
            itemsTemplate.itemIncDecQuantity++;
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
        }
        else
        {
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
        }
    }
    public void DecreaseQuantity()
    {
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
        if (itemsTemplate.itemIncDecQuantity < 1)
        {
            itemsTemplate.itemIncDecQuantity = 1;
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
        }
        else
        {      
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
            itemsTemplate.itemIncDecQuantity--;
        }
    } 
    public void ResetQuant(ItemsTemplate itemT)
    {      
        itemT.itemIncDecQuantity = 1;
        itemT.selectQuantityText.text = selectquant + itemT.itemIncDecQuantity;
    }
   
    public void SellShopItem()
    {
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate item = parentGameObject.GetComponent<ItemsTemplate>();
        if (item.itemSO.quantity > 0)
        {           
            GameService.Instance.PlayerManager.UpdateInventory(item, item.itemIncDecQuantity);
            ResetBuyButton(item);
            RefreshShopUI(item);           
        }
        else
        {
            Debug.Log("Item Not Available");
        }
    }  
    private void ConnectSOtoUI(ShopItemsSO[] shopItemsSO)
    {
        DisableAllItems();
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            itemsTemplate[i].tempItemQuantity = shopItemsSO[i].quantity;
            itemsTemplate[i].itemSO = shopItemsSO[i]; 
            itemsTemplate[i].gameObject.SetActive(true);
            itemsTemplate[i].priceText.text = shopItemsSO[i].buyingPrice.ToString();
            itemsTemplate[i].iconImage.sprite = shopItemsSO[i].icon;
            itemsTemplate[i].descriptionText.text = shopItemsSO[i].itemDescription;
            itemsTemplate[i].itemweightText.text = shopItemsSO[i].weight.ToString();
            itemsTemplate[i].QuantityText.text = shopItemsSO[i].quantity.ToString();
            itemsTemplate[i].rarityText.text = shopItemsSO[i].itemRarity.ToString();
            itemsTemplate[i].uniqueTemplateID = shopItemsSO[i].uniqueID;
            itemsTemplate[i].itemRarity = shopItemsSO[i].itemRarity;          
            itemsTemplate[i].buyButton.gameObject.SetActive(true);
            itemsTemplate[i].quantityPanel.SetActive(false);
            ResetBuyButton(itemsTemplate[i]);
        }
        ResetSCrollRect();       
    }
    public void RefreshShopUI(ItemsTemplate itemsT)
    {
        if(itemsT.itemSO.itemType == ShopItemsSO.type.Material)
        {
            LoadMaterialItems();
        }
        if (itemsT.itemSO.itemType == ShopItemsSO.type.Weapon)
        {
            LoadWeaponItems();
        }
        if (itemsT.itemSO.itemType == ShopItemsSO.type.Consumable)
        {
            LoadConsumablesItems();
        }
        if (itemsT.itemSO.itemType == ShopItemsSO.type.Treause)
        {
            LoadTreasureItems();
        }
    }
    public void ResetBuyButton(ItemsTemplate itemT)
    {
        itemT.buyButton.gameObject.SetActive(true);
        itemT.quantityPanel.SetActive(false); 
        ResetQuant(itemT);
    }
   private void DisableAllItems()
    {
        for(int i =0; i < itemsTemplate.Length; i++)
        {
            itemsTemplate[i].gameObject.SetActive(false);
        }
    }
    
}
