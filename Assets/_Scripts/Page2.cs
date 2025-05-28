using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page2 : MonoBehaviour
{
    [SerializeField] private GameObject go_page3;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Color[] buttonColors;

    private void Awake()
    {
        dropdown.onValueChanged.AddListener((value) =>
        {
            if (dropdown.options[dropdown.value].text == "Free")
            {
                ColorBlock cb = button.colors;        // 현재 ColorBlock을 복사
                cb.normalColor = buttonColors[0];     // 수정
                button.colors = cb;
            }
            else
            {
                ColorBlock cb = button.colors;        // 현재 ColorBlock을 복사
                cb.normalColor = buttonColors[1];     // 수정
                button.colors = cb;
            }
        });

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page2_Option, "page2_none");
        dropdown.onValueChanged.Invoke(0);
    }

    private void OnButtonClick()
    {
        if (dropdown.options[dropdown.value].text == "Free")
        {
            Manager.Instance.SetState(Manager.State.Page2_Option, "size_select_finished");
            gameObject.SetActive(false);
            go_page3.SetActive(true);
        }
        else
        {
            WarningPopup.Instance.ShowWarningPopup("사이즈를 선택해주세요.", button, dropdown);
        }
    }


}
