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
    private void ResetSCrollRect()=> scrollRect.normalizedPosition = new Vector2(0, 1);
    public void OnClickBuyButton()
    {        
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();      
        GameObject parentGameObject = button.transform.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
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
        if (quant < int.Parse(itemsTemplate.QuantityText.text))
        {
            quant++;
            itemsTemplate.selectQuantityText.text = selectquant + quant;
        }
        else
        {
            itemsTemplate.selectQuantityText.text = selectquant + quant;
        }
    }
    public void DecreaseQuantity()
    {
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate itemsTemplate = parentGameObject.GetComponent<ItemsTemplate>();
        if (quant < 1)
        {
            quant = 1;
            itemsTemplate.selectQuantityText.text = selectquant + quant;
        }
        else
        {      
            itemsTemplate.selectQuantityText.text = selectquant + quant; 
            quant--;
        }
    } 
    private void ResetQuant(ItemsTemplate itemT)
    {      
        quant = 1;
        itemT.selectQuantityText.text = selectquant + quant;
    }
    public void GenerateCoin()
    {
        if (GameService.Instance.PlayerManager.playerMoney == 0)
        {
            GameService.Instance.PlayerManager.playerMoney = Random.Range(35, 65);
        }
        GameService.Instance.PlayerManager.UpdateCredits();
    }
    public void SellShopItem()
    {
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        GameObject parentGameObject = button.transform.parent.parent.gameObject;
        ItemsTemplate item = parentGameObject.GetComponent<ItemsTemplate>();      
        GameService.Instance.PlayerManager.UpdateInventory(item);                  
    }  
    private void ConnectSOtoUI(ShopItemsSO[] shopItemsSO)
    {
        DisableAllItems();
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            itemsTemplate[i].itemSO = shopItemsSO[i]; 
            itemsTemplate[i].gameObject.SetActive(true);
            itemsTemplate[i].priceText.text = shopItemsSO[i].buyingPrice.ToString();
            itemsTemplate[i].iconImage.sprite = shopItemsSO[i].icon;
            itemsTemplate[i].descriptionText.text = shopItemsSO[i].itemDescription;
            itemsTemplate[i].itemweightText.text = shopItemsSO[i].weight.ToString();
            itemsTemplate[i].QuantityText.text = shopItemsSO[i].quantity.ToString();
            itemsTemplate[i].rarityText.text = shopItemsSO[i].itemRarity.ToString();          
            ResetBuyButton(i);
            ResetQuant(itemsTemplate[i]);
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
    private void ResetBuyButton(int i)
    {
        itemsTemplate[i].buyButton.gameObject.SetActive(true);
        itemsTemplate[i].quantityPanel.SetActive(false);
    }
}
