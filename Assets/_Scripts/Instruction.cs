using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void TextTimerDelay()
    {
        StartCoroutine(COR_TextTimer());
    }

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    private IEnumerator COR_TextTimer()
    {
        int timer = 7;

        while (timer > 0)
        {
            text.text = $"거리 조건 0.5m 실험이 {timer}초 뒤 시작됩니다.";

            yield return waitForSeconds;
            timer--;
        }
        Manager.Instance.StartExperiment();
        gameObject.SetActive(false);
    }
}
