using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;


public class StepChanger : MonoBehaviour
{ 
    public Step[] steps;
    public Action<Step> OnScaleChanged;

    private int currentStep;

    private void Start()
    {
        InitSteps();
        //ShuffleSteps();

        currentStep = 0;
        ChangeStep();
    }


    public void ChangeStep()
    {
        OnScaleChanged.Invoke(steps[currentStep]);
        currentStep++;
    }

    private void InitSteps()
    {
        for (int i = 0; i<steps.Length; i++)
        {
            float originValue = steps[i].scale_origin.x;

            float randomOffset = originValue * Random.Range(-0.3f, 0.3f);
            steps[i].scale_adjusted = new Vector3(originValue + randomOffset, originValue + randomOffset, originValue + randomOffset);
        }
    }


    // 🔹 Fisher–Yates Shuffle (Unity 전용)
    private void ShuffleSteps()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            int randIndex = Random.Range(i, steps.Length);
            (steps[i], steps[randIndex]) = (steps[randIndex], steps[i]);
        }
    }

}

[System.Serializable]
public class Step
{
    public string step_label;
    public float distance;
    public Vector3 scale_origin;
    [HideInInspector]public Vector3 scale_adjusted;
}
