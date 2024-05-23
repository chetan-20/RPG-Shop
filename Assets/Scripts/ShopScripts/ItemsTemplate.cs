
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemsTemplate : MonoBehaviour
{
    [SerializeField] public Button buyButton;
    [SerializeField] public Image iconImage;
    [SerializeField] public TMP_Text priceText;
    [SerializeField] public TMP_Text QuantityText;
    [SerializeField] public TMP_Text descriptionText;
    [SerializeField] public TMP_Text rarityText;
    [SerializeField] public TMP_Text itemweightText;
    [SerializeField] public GameObject quantityPanel;
    [SerializeField] public TMP_Text selectQuantityText;
    [SerializeField] public Button increaseQuantityButton;
    [SerializeField] public Button decreaseQuantityButton;
    [SerializeField] public Button purhcaseButton;
    [SerializeField] public Button cancelButton;

    public int itemIncDecQuantity=1;
    public ShopItemsSO itemSO;
    public ShopItemsSO.rarity itemRarity;
    public int tempItemQuantity;
    public int uniqueTemplateID;
}
