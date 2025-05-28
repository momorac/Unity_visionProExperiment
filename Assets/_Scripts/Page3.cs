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
            UpdateButtonColor(isOn ? buttonColors[0] : buttonColors[1]);
        });
    }

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page3_Purchase, "page3_none");
        toggle.isOn = false;
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
            Manager.Instance.ShowWarningPopup("약관을 동의해주세요.");
        }
    }

    void UpdateButtonColor(Color newNormalColor)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = newNormalColor;

        // 추가로 highlightedColor도 바꿔주면 체감이 잘 됨
        cb.highlightedColor = newNormalColor;

        button.colors = cb;

        // 강제로 버튼 상태를 바꿨다가 되돌리기
        button.interactable = false;
        button.interactable = true;
    }
}
