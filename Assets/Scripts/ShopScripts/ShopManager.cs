
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button materialButton;
    [SerializeField] private Button weaponsButton;
    [SerializeField] private Button consumablesButton;
    [SerializeField] private Button treasureButton;
    [SerializeField] private ShopItemsSO[] materialItemsSO;
    [SerializeField] private ShopItemsSO[] weaponItemsSO;
    [SerializeField] private ShopItemsSO[] consumablesItemsSO;
    [SerializeField] private ShopItemsSO[] treasureItemsSO;
    [SerializeField] private ItemsTemplate[] itemsTemplate;
    [SerializeField] private ScrollRect scrollRect;    

    private const string selectquant = "Select Quantity : ";

    private void Start()
    {
        DisableAllItems();
        LoadMaterialItems();
        AddSoundToTabButtons();
        LoadItemsTab();
    }

    public void LoadMaterialItems()=> ConnectSOtoUI(materialItemsSO);    
    public void LoadWeaponItems()=> ConnectSOtoUI(weaponItemsSO);   
    public void LoadConsumablesItems()=> ConnectSOtoUI(consumablesItemsSO);   
    public void LoadTreasureItems()=> ConnectSOtoUI(treasureItemsSO);   
    public void ResetScrollRect(ScrollRect scroll)=> scroll.normalizedPosition = new Vector2(0, 1);
    private Button GetButton() => UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

    private ItemsTemplate GetItemTemplate(Button button) 
    {
        ItemsTemplate template1 = button.transform.parent.GetComponent<ItemsTemplate>();
        ItemsTemplate template2 = button.transform.parent.parent.GetComponent<ItemsTemplate>();
        if(template1 != null)
        {
            return template1;
        }
        else
        {
            return template2;
        }
    }
    public void OnClickBuyButton()
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        Button button = GetButton();             
        ItemsTemplate itemsTemplate = GetItemTemplate(button);
        ResetQuant(itemsTemplate);
        itemsTemplate.buyButton.gameObject.SetActive(false);
        itemsTemplate.quantityPanel.SetActive(true);       
    }
    public void OnClickCancelButton()
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        Button button = GetButton();
        ItemsTemplate itemsTemplate = GetItemTemplate(button);
        itemsTemplate.buyButton.gameObject.SetActive(true);
        itemsTemplate.quantityPanel.SetActive(false);
        ResetQuant(itemsTemplate);
    }
    public void IncreaseQuantity()
    {
        Button button = GetButton();
        ItemsTemplate itemsTemplate = GetItemTemplate(button);
        if (itemsTemplate.itemIncDecQuantity<itemsTemplate.tempItemQuantity)
        {
            itemsTemplate.itemIncDecQuantity++;
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
            GameService.Instance.SoundManager.PlaySound(Sounds.IncDecQuantitySound);
        }
        else
        {
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
        }
    }
    public void DecreaseQuantity()
    {
        Button button = GetButton();
        ItemsTemplate itemsTemplate = GetItemTemplate(button);
        if (itemsTemplate.itemIncDecQuantity <= 1)
        {
            itemsTemplate.itemIncDecQuantity = 1;
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity;
        }
        else
        {
            itemsTemplate.itemIncDecQuantity--;
            itemsTemplate.selectQuantityText.text = selectquant + itemsTemplate.itemIncDecQuantity; 
            GameService.Instance.SoundManager.PlaySound(Sounds.IncDecQuantitySound);                      
        }
    } 
    public void ResetQuant(ItemsTemplate itemT)
    {      
        itemT.itemIncDecQuantity = 1;
        itemT.selectQuantityText.text = selectquant + itemT.itemIncDecQuantity;
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
    public void SellShopItem()
    {
        Button button = GetButton();
        ItemsTemplate item = GetItemTemplate(button);
        if (item.itemSO.quantity > 0)
        {           
            GameService.Instance.PlayerManager.UpdateInventory(item, item.itemIncDecQuantity);
            ResetBuyButton(item);
            RefreshShopUI(item);            
        }
        else
        {
            GameService.Instance.SoundManager.PlaySound(Sounds.CantBuyorSellSound);
            GameService.Instance.PopUpManager.ShowPopupMessage("Item Not Available");           
        }
    }  
    private void ConnectSOtoUI(ShopItemsSO[] shopItemsSO)
    {      
        DisableAllItems();
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            SetItemTemplate(shopItemsSO[i], itemsTemplate[i]);
        }
        ResetScrollRect(scrollRect);       
    }
    private void SetItemTemplate(ShopItemsSO shopItem, ItemsTemplate template)
    {
        template.tempItemQuantity = shopItem.quantity;
        template.itemSO = shopItem;
        template.gameObject.SetActive(true);
        template.priceText.text = shopItem.buyingPrice.ToString();
        template.iconImage.sprite = shopItem.icon;
        template.descriptionText.text = shopItem.itemDescription;
        template.itemweightText.text = shopItem.weight.ToString();
        template.QuantityText.text = shopItem.quantity.ToString();
        template.rarityText.text = shopItem.itemRarity.ToString();
        template.uniqueTemplateID = shopItem.uniqueID;
        template.itemRarity = shopItem.itemRarity;
        template.buyButton.gameObject.SetActive(true);
        template.quantityPanel.SetActive(false);
        ResetBuyButton(template);
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
    private void LoadItemsTab()
    {
        materialButton.onClick.AddListener(LoadMaterialItems);
        weaponsButton.onClick.AddListener(LoadWeaponItems);
        consumablesButton.onClick.AddListener(LoadConsumablesItems);
        treasureButton.onClick.AddListener(LoadTreasureItems);
    }
    private void AddSoundToTabButtons()
    {
        materialButton.onClick.AddListener(()=>GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound));
        weaponsButton.onClick.AddListener(() => GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound));
        consumablesButton.onClick.AddListener(() => GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound));
        treasureButton.onClick.AddListener(() => GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound));
    }
}
