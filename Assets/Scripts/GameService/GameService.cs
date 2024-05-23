using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ShopManager shopManager;   
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private PopUpManager popUpManager;

    public static GameService Instance { get { return instance; } }
    private static GameService instance;    
    public ShopManager ShopManager { get { return shopManager; } }  
    public PlayerManager PlayerManager { get { return playerManager; } }  
    public SoundManager SoundManager { get { return soundManager; } }
    public PopUpManager PopUpManager { get { return popUpManager; } }

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
    private void Update()
    {
        OnEscapePress();
    }
    private void OnEscapePress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
