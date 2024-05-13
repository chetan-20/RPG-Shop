using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Instance { get { return instance; } }
    private static GameService instance;
    public ItemsTemplate itemsTemplate {  get; private set; }
    public ShopManager shopManager { get; private set; }
    public ShopItemsSO shopItemsSO { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
