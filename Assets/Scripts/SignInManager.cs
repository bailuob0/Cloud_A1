using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignInManager : MonoBehaviour
{
    public GameObject InputToDisable;
    public GameObject InputToEnable;
    public GameObject ButtonToEnable;

    public void ChangeInputField()
    {
        InputToEnable.SetActive(true);
        InputToDisable.SetActive(false);
        ButtonToEnable.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
