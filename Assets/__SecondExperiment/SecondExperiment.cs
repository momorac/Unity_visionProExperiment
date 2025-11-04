using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SecondExperiment : MonoBehaviour
{

    public enum Objects
    {
        Human = 0,
        Cube = 1,
        Furniture = 2,
    }

    [Header("Distance Controll")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text_sliderValue;

    [Header("Objects")]
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Toggle[] toggles;


    private GameObject currentObject;

    private void Awake()
    {
        toggles[(int)Objects.Human].onValueChanged.AddListener((isOn) =>
        {
            objects[(int)Objects.Human].SetActive(isOn);
            currentObject = objects[(int)Objects.Human];
        });
        toggles[(int)Objects.Cube].onValueChanged.AddListener((isOn) =>
        {
            objects[(int)Objects.Cube].SetActive(isOn);
            currentObject = objects[(int)Objects.Cube];
        });
        toggles[(int)Objects.Furniture].onValueChanged.AddListener((isOn) =>
        {
            objects[(int)Objects.Furniture].SetActive(isOn);
            currentObject = objects[(int)Objects.Furniture];
        });


        foreach (var obj in objects) obj.SetActive(false);
        currentObject = objects[0];
    }

    public void OnSliderValueChanged(float value)
    {
        float targetDistance = Mathf.Lerp(minDistance, maxDistance, value);

        currentObject.transform.position = new Vector3(0, 0, targetDistance);
        text_sliderValue.text = targetDistance.ToString("F2");
    }
}
