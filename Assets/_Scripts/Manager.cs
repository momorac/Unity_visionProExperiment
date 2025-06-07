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


    [Space(10)]
    [Header("Toast")]
    [SerializeField] private GameObject go_toast;
    [SerializeField] private TextMeshProUGUI text_toast;
    private CanvasGroup cg_toast;

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

        cg_toast = go_toast.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        cycle = 1;
        go_toast.SetActive(false);
    }

    // 1번째, 2번재 실험 초기화 메소드
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

    // 각 단계 종료 후 초기화 메소드
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

    // 거리 설정 메소드
    private void SetDistance()
    {
        frame.position = distances[currentDistance];
        frame.localScale = scales[currentDistance];
    }

    // 로그용 현재 state 설정 메소드
    public void SetState(State _state, string _detail)
    {
        currentState = _state;
        currentDetail = _detail;
    }

    public void ShowToastMessageDelay(string ment)
    {
        StartCoroutine(ment);
    }

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.04f);
    private IEnumerator ShowToastMessage(string ment)
    {
        text_toast.text = ment;
        cg_toast.alpha = 0;
        go_toast.SetActive(true);

        while (cg_toast.alpha < 1f)
        {
            cg_toast.alpha += 0.05f;
            yield return waitForSeconds;
        }

        yield return new WaitForSeconds(1.5f);

        while (cg_toast.alpha > 0)
        {
            cg_toast.alpha -= 0.05f;
            yield return waitForSeconds;
        }
        go_toast.SetActive(false); ;
    }




    public void ExitApplication()
    {
        Application.Quit();
    }

}
