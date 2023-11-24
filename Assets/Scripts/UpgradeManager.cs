using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    ShipStats shipStats;

    [SerializeField]
    PlayerFabDataManager dataManager;

    
    public GameObject catImage;
    
    void Awake()
    {
        catImage.SetActive(false);
        CheckForCat();
    }

    public void ChangeShipStats(string upgradeName)
    {
        if (upgradeName == "Upgrade1_Speed")
        {
            shipStats.MoveSpeed = 15;
        }

        if (upgradeName == "Upgrade2_Blaster")
        {
            shipStats.FireRate = 0.1f;
        }

        if (upgradeName == "Upgrade3_Bullet")
        {
            shipStats.BulletSpeed = 30;
        }

        if (upgradeName == "Upgrade4_Cat")
        {
            shipStats.hasCat = true;
        }

        CheckForCat();
        dataManager.SendData();

        Debug.Log(shipStats.MoveSpeed);
    }
    
    private void CheckForCat()
    {
        if (shipStats.hasCat)
        {
            catImage.SetActive(true);
            return;
        }
    }

}
