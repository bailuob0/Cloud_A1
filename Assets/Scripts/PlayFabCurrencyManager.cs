using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.Collections;

public class PlayFabCurrencyManager : MonoBehaviour
{
    public TMP_Text coinsValueText;

    public List<ShopItemUI> shopItemUIs;

    public UpgradeManager upgradeManager;

    [SerializeField] 
    private GameObject messageBox;

    private TMP_Text message;


    [HideInInspector]
    public List<CatalogItem> catalogItems;

    [HideInInspector]
    public List<ItemInstance> inventoryItems;


    public void PrepareShop()
    {
        GetCatalog();
        GetVirtualCurrencies();
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int coinsAmt = result.VirtualCurrency["CN"];
        coinsValueText.text = "Coins: " + coinsAmt.ToString();

        inventoryItems = result.Inventory;
        CheckPlayerInventory();
    }

    public void GetCatalog()
    {
        var catalogRequest = new GetCatalogItemsRequest
        {
            CatalogVersion = "Items"
        };

        PlayFabClientAPI.GetCatalogItems(catalogRequest, OnGetCatalogSuccess, OnError);
    }

    public void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        catalogItems = result.Catalog;
        int i = 0;
        foreach (CatalogItem item in catalogItems)
        {
            shopItemUIs[i].gameObject.SetActive(true);
            shopItemUIs[i].Name.text = item.DisplayName;
            shopItemUIs[i].Description.text = item.Description;
            shopItemUIs[i].Price.text = item.VirtualCurrencyPrices["CN"].ToString();

            i++;
        }
    }

    public void BuyItem(int itemIndex)
    {   
        var buyRequest = new PurchaseItemRequest
        {
            CatalogVersion = "Items",
            ItemId = catalogItems[itemIndex].ItemId,
            VirtualCurrency = "CN",
            Price = (int)catalogItems[itemIndex].VirtualCurrencyPrices["CN"]
        };

        PlayFabClientAPI.PurchaseItem(buyRequest, OnBuyItemSuccess, OnError);
    }

    void OnBuyItemSuccess(PurchaseItemResult result)
    {
        //Debug.Log("omg u made a purchase owo");

        string itemName = result.Items[0].ItemId;

        GetVirtualCurrencies();
        upgradeManager.ChangeShipStats(itemName);
    }

    public void AddVirtualCurrency(int amtToAdd)
    {
        var addRequest = new AddUserVirtualCurrencyRequest
        {
            Amount = amtToAdd,
            VirtualCurrency = "CN"
        };

        PlayFabClientAPI.AddUserVirtualCurrency(addRequest, OnAddVirtualCurrencySuccess, OnError);
    }

    void OnAddVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log( result.BalanceChange + "added to player currency");
    }

    private void OnError(PlayFabError e)
    {
        //messageBox.text = "Error" + e.GenerateErrorReport();
        UpdateMessage("Error" + e.GenerateErrorReport());
    }

    public void UpdateMessage(string msg)
    {
        StartCoroutine(UpdateMessageBox(msg));
    }

    private IEnumerator UpdateMessageBox(string msg)
    {
       
        messageBox.SetActive(true);
        
        message.text = msg;

        yield return new WaitForSeconds(2);

        message.text = " ";

        messageBox.gameObject.SetActive(false);
    }

    public void CheckPlayerInventory()
    {
        StartCoroutine(CheckInventory());
    }

    private IEnumerator CheckInventory()
    {
        yield return new WaitForSeconds(1.2f);

        foreach (var inventoryItem in inventoryItems)
        {
            foreach (var shopItem in shopItemUIs)
            {
                if (inventoryItem.DisplayName == shopItem.Name.text)
                {
                    shopItem.Disable();
                }
            }
        }
    }
}
