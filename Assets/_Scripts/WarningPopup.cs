using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopup : MonoBehaviour
{
    public static WarningPopup Instance;

    [SerializeField] Button button_close;
    [SerializeField] TextMeshProUGUI text_ment;

    private Button hideButton;
    private TMP_Dropdown hideDropdown;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowWarningPopup(String ment)
    {
        text_ment.text = ment;
        gameObject.SetActive(true);
    }

    public void CloseWarningPopup()
    {
        gameObject.SetActive(false);
    }

}
