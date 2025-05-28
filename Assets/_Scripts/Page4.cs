using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page4 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_title;

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page4_Finished, "none");
        StartCoroutine(COR_TextTimer());
    }

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    private IEnumerator COR_TextTimer()
    {
        int timer = 10;
        while (timer > 0)
        {
            text_title.text = $"{timer}초 후 다음 실험으로 넘어가겠습니다!";
            yield return waitForSeconds;
            timer--;
        }
        Manager.Instance.SetNextStep();
    }
}
