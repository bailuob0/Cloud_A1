using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text Price;
    public GameObject BoughtPanel;
    public Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void Disable()
    {
        BoughtPanel.SetActive(true);
        button.interactable = false;
    }

}
