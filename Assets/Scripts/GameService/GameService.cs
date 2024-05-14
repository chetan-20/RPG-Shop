using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ShopManager shopManager;
    public static GameService Instance { get { return instance; } }
    private static GameService instance;   
    public ShopManager ShopManager { get { return shopManager; } }  
    public PlayerManager PlayerManager { get { return playerManager; }  }   
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
