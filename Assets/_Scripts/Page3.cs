using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page3 : MonoBehaviour
{
    [SerializeField] private GameObject go_page4;
    [SerializeField] private Button button;
    [SerializeField] private Image buttonFront;
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
            Manager.Instance.SetState(Manager.State.Page3_Purchase, "page3_toggleOn");
            UpdateButtonColor(isOn ? buttonColors[0] : buttonColors[1]);
        });
    }

    private void OnEnable()
    {
        toggle.isOn = false;
        Manager.Instance.SetState(Manager.State.Page3_Purchase, "page3_none");
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
            Manager.Instance.ShowToastMessageDelay("약관에 동의해주세요.");
        }
    }

    void UpdateButtonColor(Color newNormalColor)
    {
        buttonFront.color = newNormalColor;
    }
}
