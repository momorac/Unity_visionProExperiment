using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Page1 : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        buttons[0].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_1_clicked"));
        buttons[1].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_2_clicked"));
        buttons[2].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_3_clicked"));
        buttons[3].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_4_clicked"));
    }

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page1_Products, "page1_none");
    }


}
