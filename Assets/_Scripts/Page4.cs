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
        int timer = 7;
        while (timer > 0)
        {
            if (Manager.Instance.currentDistance != Manager.Distance.Two)
                text_title.text = $"4. 단계를 마쳤습니다. {timer}초 후 다음으로 넘어갑니다.";
            else
                text_title.text = $"4. 실험을 모두 마쳤습니다. {timer}초 후 자동으로 종료됩니다.";
            yield return waitForSeconds;
            timer--;
        }
        Manager.Instance.SetNextStep();
    }
}
