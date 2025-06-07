using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page4 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        Manager.Instance.SetState(Manager.State.Page4_Finished, "page4_none");
        StartCoroutine(COR_TextTimer());
    }

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    private IEnumerator COR_TextTimer()
    {
        int timer = 7;
        string nextDistance = "0.5m";
        if (Manager.Instance.currentDistance == Manager.Distance.Half) nextDistance = "1m";
        else if (Manager.Instance.currentDistance == Manager.Distance.One) nextDistance = "2m";
        else if (Manager.Instance.currentDistance == Manager.Distance.Two) nextDistance = "0.5m";

        while (timer > 0)
        {
            if (Manager.Instance.cycle == 2 && Manager.Instance.currentDistance == Manager.Distance.Two)
                text.text = $"실험을 모두 마쳤습니다. {timer}초 후 자동으로 종료됩니다.";
            else
                text.text = $"거리 조건 {nextDistance} 실험이 {timer}초 뒤 시작됩니다.";

            yield return waitForSeconds;
            timer--;
        }
        Manager.Instance.SetNextStep();
    }
}
