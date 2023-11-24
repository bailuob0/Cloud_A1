using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    ShipStats shipStats;

    public GameObject catImage;
    
    void Start()
    {
        CheckForCat();
    }
    
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    private void CheckForCat()
    {
        if (shipStats.hasCat)
        {
            catImage.SetActive(true);
            return;
        }

        catImage.SetActive(false);
    }
}
