using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SecondExperiment_Re_vision : MonoBehaviour
{
    [Header("Controll")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float initialDistance = 1f;
    [SerializeField] private float initialScale = 0.55f;




    [Header("Reference")]
    [SerializeField] private Transform go_cube;
    [SerializeField] private Transform tr_cube;
    [SerializeField] private TextMeshProUGUI text_distance;
    [SerializeField] private TextMeshProUGUI text_scale;


    private Vector3 scaleBuffer = new Vector3();

    private void Start()
    {
        go_cube.position = new Vector3(0, 0, initialDistance);
        tr_cube.localScale = new Vector3(initialScale, initialScale, initialScale);
    }

    public void OnDistanceChanged(float value)
    {
        float targetDistance = Mathf.Lerp(minDistance, maxDistance, value);

        go_cube.position = new Vector3(0, 0, targetDistance);
        text_distance.text = targetDistance.ToString("F2");
    }

    public void OnScaleChanged(float value)
    {
        float targetValue = Mathf.Lerp(minScale, maxScale, value);

        Vector3 newScale = new Vector3(targetValue, targetValue, targetValue);
        tr_cube.localScale = newScale;
        text_scale.text = targetValue.ToString("F2");

    }
}
