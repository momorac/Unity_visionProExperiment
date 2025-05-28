using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page1 : MonoBehaviour
{
    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page1_Products, "page1_none");
    }
}
