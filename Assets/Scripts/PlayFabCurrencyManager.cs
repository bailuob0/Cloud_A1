using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabCurrencyManager : MonoBehaviour
{
    public TMP_Text coinsValueText;

    public List<ShopItemUI> shopItemUIs;

    public UpgradeManager upgradeManager;

    [HideInInspector]
    public List<CatalogItem> catalogItems;

    [HideInInspector]
    public List<ItemInstance> inventoryItems;

    public void PrepareShop()
    {
        GetVirtualCurrencies();
        GetCatalog();
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

    private void OnError(PlayFabError e)
    {
        //messageBox.text = "Error" + e.GenerateErrorReport();
        Debug.Log("Error" + e.GenerateErrorReport());
    }

    public void CheckPlayerInventory()
    {
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
