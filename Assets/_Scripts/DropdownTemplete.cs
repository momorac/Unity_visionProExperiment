using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownTemplete : MonoBehaviour
{
    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page2_Option, "page2_size_select_started");
        TouchGazeTracker.Instance.AddLog("Dropdown_opened");
    }
}
