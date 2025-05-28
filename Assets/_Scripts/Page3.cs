using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page3 : MonoBehaviour
{
    [SerializeField] private GameObject go_page4;
    [SerializeField] private Button button;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Color[] buttonColors;


    private void Awake()
    {
        ColorBlock cb = button.colors;        // 현재 ColorBlock을 복사
        cb.normalColor = buttonColors[1];     // 수정
        button.colors = cb;                   // 다시 버튼에 할당

        button.onClick.AddListener(OnButtonClick);

        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                ColorBlock cb = button.colors;        // 현재 ColorBlock을 복사
                cb.normalColor = buttonColors[0];     // 수정
                button.colors = cb;                   // 다시 버튼에 할당
            }
            else
            {
                ColorBlock cb = button.colors;        // 현재 ColorBlock을 복사
                cb.normalColor = buttonColors[1];     // 수정
                button.colors = cb;
            }
        });
    }

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page3_Purchase, "page3_none");
        toggle.onValueChanged.Invoke(false);
    }

    private void OnButtonClick()
    {
        if (toggle.isOn)
        {
            gameObject.SetActive(false);
            go_page4.SetActive(true);
        }
        else
        {
            WarningPopup.Instance.ShowWarningPopup("약관을 동의해주세요.");
        }
    }
}
