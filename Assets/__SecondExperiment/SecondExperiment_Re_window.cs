using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondExperiment_Re_window : MonoBehaviour
{
    public float initialDistance = 1f;
    public float wheelSpeed = 0.1f;

    [Space(10)]
    [SerializeField] private Transform go_cube;
    [SerializeField] private Transform tr_cube;
    [SerializeField] private Transform tr_shadow;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI text_distance;
    [SerializeField] private TextMeshProUGUI text_scale;


    private float currentDistance;
    private Vector3 currentScale;


    private void Start()
    {
        currentScale = new Vector3(1, 1, 1);
        currentDistance = initialDistance;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdjustDistance(-0.1f);
            text_distance.text = $"Distance : {currentDistance}";
        }
        else if (Input.GetMouseButtonDown(1))
        {
            AdjustDistance(0.1f);
            text_distance.text = $"Distance : {currentDistance}";
        }


        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput>0)
        {
            AdjustScale(wheelInput * wheelSpeed);
            text_scale.text = $"Scale : {currentScale}";
        }
    }

    private void AdjustDistance(float value)
    {
        float newDistance = currentDistance + value;

        go_cube.position = new Vector3(0, 0, newDistance);

        currentDistance = newDistance;
    }

    private void AdjustScale(float value)
    {
        Vector3 newScale = currentScale + new Vector3(value, value, value);

        tr_cube.localScale = newScale;
        tr_shadow.localScale = newScale;

        currentScale = newScale;
    }

}
