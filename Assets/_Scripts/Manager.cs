using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    public enum State
    {
        Page1_Products,
        Page2_Option,
        Page3_Purchase,
        Page4_Finished,
    }
    public enum Distance
    {
        Half,
        One,
        Two,
    }

    public static Manager Instance { get; private set; }

    public Distance currentDistance;
    public State currentState;
    public string currentDetail;


    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform frame;
    [SerializeField] private GameObject warningPopup;
    [SerializeField] private GameObject[] pages;

    private Dictionary<Distance, Vector3> distances = new Dictionary<Distance, Vector3>
    {
        { Distance.Half, new Vector3(0, 1, 0.5f) },
        { Distance.One, new Vector3(0, 1, 1) },
        { Distance.Two, new Vector3(0, 1, 2) },
    };

    private Dictionary<Distance, Vector3> scales = new Dictionary<Distance, Vector3>
    {
        { Distance.Half, new Vector3(0.04f, 0.04f, 0.04f) },
        { Distance.One, new Vector3(0.1f, 0.1f, 0.1f) },
        { Distance.Two, new Vector3(0.18f, 0.18f, 0.18f) },
    };



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        warningPopup.SetActive(false);

        currentDistance = Distance.Half;
        currentState = State.Page1_Products;
        currentDetail = "null";

        SetDistance();
    }

    public void SetNextStep()
    {
        if (currentDistance == Distance.Half)
            currentDistance = Distance.One;
        else if (currentDistance == Distance.One)
            currentDistance = Distance.Two;
        else if (currentDistance == Distance.Two)
        {
            GetComponent<GazeTrackingController>().SaveLog();
            Application.Quit();
            return;
        }

        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);

        SetDistance();
    }

    private void SetDistance()
    {
        frame.position = distances[currentDistance];
        frame.localScale = scales[currentDistance];
    }

    public void SetState(State _state, string _detail)
    {
        currentState = _state;
        currentDetail = _detail;
    }

}
