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
        buttons[0].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_1"));
        buttons[1].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_2"));
        buttons[2].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_3"));
        buttons[3].onClick.AddListener(() => TouchGazeTracker.Instance.AddLog("Button_4"));
    }

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page1_Products, "page1_none");
    }


}
