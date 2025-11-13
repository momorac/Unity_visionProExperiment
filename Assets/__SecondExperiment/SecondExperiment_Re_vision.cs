using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SecondExperiment_Re_vision : MonoBehaviour
{
    public enum Type
    {
        AVP,
        PM
    }

    public Type type;

    [Header("Reference")]
    [SerializeField] private Transform go_cube;
    [SerializeField] private Transform tr_cube;
    [SerializeField] private TextMeshProUGUI text_currentStep;
    [SerializeField] private TextMeshProUGUI text_distance;
    [SerializeField] private TextMeshProUGUI text_scale;
    [SerializeField] private TextMeshProUGUI text_button;


    private StepChanger stepChanger;

    private void Awake()
    {
        stepChanger = GetComponent<StepChanger>();

        stepChanger.OnScaleChanged += ((step) => OnScaleChanged(step));
    }

 

    private void OnScaleChanged(Step step)
    {
        if (stepChanger.currentStep == stepChanger.steps_length -1)
        {
            text_button.text = "Finish";
        }
        else if (stepChanger.currentStep >= stepChanger.steps_length)
        {
            Application.Quit();
        }

        go_cube.position = new Vector3(0, 0, step.distance);

        if (type == Type.AVP)
        {
            tr_cube.localScale = step.scale_origin;
            text_scale.text = "Scale : " + step.scale_origin.x.ToString("F3");
        }
        else if (type == Type.PM)
        {
            tr_cube.localScale = step.scale_adjusted;
            text_scale.text = "Scale : " + step.scale_adjusted.x.ToString("F3");
        }

        text_currentStep.text = step.step_label;
        text_distance.text = "Distance : " + step.distance.ToString("F2");

    }
}
