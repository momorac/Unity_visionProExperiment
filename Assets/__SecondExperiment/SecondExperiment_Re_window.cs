using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondExperiment_Re_window : MonoBehaviour
{

    public enum Type
    {
        AVP,
        PM
    }

    public Type type;
    public float wheelSpeed = 0.1f;

    [Space(10)]
    [SerializeField] private Transform go_cube;
    [SerializeField] private Transform tr_cube;
    [SerializeField] private Transform tr_shadow;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI text_currentStep;
    [SerializeField] private TextMeshProUGUI text_distance;
    [SerializeField] private TextMeshProUGUI text_scale;


    private Vector3 currentScale;

    private StepChanger stepChanger;


    private void Awake()
    {
        stepChanger = GetComponent<StepChanger>();

        stepChanger.OnScaleChanged += ((step) => OnScaleChanged(step));
    }

    private void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput!=0)
        {
            AdjustScale(wheelInput * wheelSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            stepChanger.ChangeStep();
        }
    }

    private void AdjustScale(float value)
    {
        Vector3 newScale = currentScale + new Vector3(value, value, value);

        tr_cube.localScale = newScale;
        tr_shadow.localScale = newScale;

        currentScale = newScale;
        text_scale.text = $"Scale : {currentScale}";
    }

    private void OnScaleChanged(Step step)
    {
        go_cube.position = new Vector3(0, 0, step.distance);

        if (type == Type.AVP)
        {
            tr_cube.localScale = step.scale_origin;
            currentScale = step.scale_origin;
            text_scale.text = "Scale : " + step.scale_origin.x.ToString("F3");
        }
        else if (type == Type.PM)
        {
            tr_cube.localScale = step.scale_adjusted;
            currentScale = step.scale_adjusted;
            text_scale.text = "Scale : " + step.scale_adjusted.x.ToString("F3");
        }

        text_currentStep.text = step.step_label;
        text_distance.text = "Distance : " + step.distance.ToString("F2");
    }

}
