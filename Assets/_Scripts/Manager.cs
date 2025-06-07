using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public int cycle;
    public Distance currentDistance;
    public State currentState;
    public string currentDetail;


    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform frame;
    [SerializeField] private CanvasGroup mainCanvas;
    [SerializeField] private GameObject[] pages;

    private Dictionary<Distance, Vector3> distances = new Dictionary<Distance, Vector3>
    {
        { Distance.Half, new Vector3(0, 1.2f, 0.5f) },
        { Distance.One, new Vector3(0, 1.2f, 1) },
        { Distance.Two, new Vector3(0, 1.2f, 2) },
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

        // popup_button_close.onClick.AddListener(CloseWarningPopup);
    }

    private void Start()
    {
        cycle = 1;
    }

    public void StartExperiment()
    {
        currentDistance = Distance.Half;
        currentState = State.Page1_Products;
        currentDetail = "null";

        SetDistance();

        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);
    }

    public void SetNextStep()
    {
        if (currentDistance == Distance.Half)
            currentDistance = Distance.One;
        else if (currentDistance == Distance.One)
            currentDistance = Distance.Two;
        else if (currentDistance == Distance.Two)
        {
            if (cycle == 1)
            {
                cycle = 2;
                StartExperiment();
                GetComponent<GazeTrackingController>().is_started = true;
                return;
            }
            else if (cycle == 2)
            {
                GetComponent<GazeTrackingController>().SaveLog();
                Application.Quit();
                return;
            }
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


    public void ExitApplication()
    {
        Application.Quit();
    }

}
