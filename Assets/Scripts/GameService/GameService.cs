using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] internal TMP_Text popUpMsgText;
    [SerializeField] internal GameObject popUpMsgGameobject;
    internal float delay = 3f;
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
    private void Start()
    {
        popUpMsgGameobject.SetActive(false);
    }
    public IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
       popUpMsgGameobject.SetActive(false);
    }
}
