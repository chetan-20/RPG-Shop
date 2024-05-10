using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemHoverScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] GameObject itemDescriptionPanel;
    [SerializeField] private Transform targetElement;
    private void Start()
    {
        itemDescriptionPanel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {        
       itemDescriptionPanel.SetActive(true);
        if (targetElement != null)
        {
            SetPanelInFrontOfTarget();
        }
        else
        {
            Debug.Log("Missing Next Items Rect Transform for ItemDescription Panel");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescriptionPanel?.SetActive(false);
    }
    public void SetPanelInFrontOfTarget()
    {
        itemDescriptionPanel.transform.SetParent(targetElement.parent); // Set the panel's parent to the same parent as the target element
        itemDescriptionPanel.transform.SetAsLastSibling(); // Make sure the panel is rendered last in the hierarchy
       
    }
}
