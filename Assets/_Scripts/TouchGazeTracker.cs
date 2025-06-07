using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchGazeTracker : MonoBehaviour
{
    public static TouchGazeTracker Instance;

    float timer = 0;

    private List<string> logData = new List<string>();
    private string filePath;

    private Manager manager;
    [HideInInspector] public bool is_started;
    [SerializeField] public TextMeshProUGUI m_ActiveTouches;


    [SerializeField]
    InputActionReference m_TouchZeroValue;

    [SerializeField]
    InputActionReference m_TouchOneValue;

    [SerializeField]
    InputActionReference m_TouchZeroPhase;

    [SerializeField]
    InputActionReference m_TouchOnePhase;


    private void Awake()
    {
        manager = GetComponent<Manager>();

        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        is_started = false;
        // is_started = true;
        string time = DateTime.Now.ToString("MMdd_HHmm");
        filePath = Path.Combine(Application.persistentDataPath, $"TouchLog_{time}.csv");
        logData.Add("Time,Distance,Page,Detail,XPos,YPos,ZPos"); // 헤더

        // DeleteAllLogs();
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        m_TouchZeroValue.action.Enable();
        m_TouchZeroPhase.action.Enable();
        m_TouchOneValue.action.Enable();
        m_TouchOnePhase.action.Enable();
    }

    private void Update()
    {
        if (!is_started) return;

        timer += Time.deltaTime;
        m_ActiveTouches.text = $"Active Touches: {Touch.activeTouches.Count}";

        // get values
        var touchZeroValue = m_TouchZeroValue.action.ReadValue<SpatialPointerState>();
        var touchZeroPhase = m_TouchZeroPhase.action.ReadValue<TouchPhase>();

        var touchOneValue = m_TouchOneValue.action.ReadValue<SpatialPointerState>();
        var touchOnePhase = m_TouchOnePhase.action.ReadValue<TouchPhase>();

        // show input visualization
        if (touchZeroPhase == TouchPhase.Began)
        {
            string entry = $"{timer:F1},{manager.currentDistance},{manager.currentState},{manager.currentDetail},{touchZeroValue.interactionPosition.x:F5},{touchZeroValue.interactionPosition.y:F5},{touchZeroValue.interactionPosition.z:F5}";
            logData.Add(entry);
        }

        if (touchOnePhase == TouchPhase.Began)
        {
            string entry = $"{timer:F1},{manager.currentDistance},{manager.currentState},{manager.currentDetail},{touchOneValue.interactionPosition.x:F5},{touchOneValue.interactionPosition.y:F5},{touchOneValue.interactionPosition.z:F5}";
            logData.Add(entry);
        }
    }

    public void AddLog(string ui)
    {
        if (!is_started) return;

        string entry = $"{timer:F1},{manager.currentDistance},{manager.currentState},{manager.currentDetail},--,--,--,{ui}";
        logData.Add(entry);
    }

    public void SaveLog()
    {
        try
        {
            File.WriteAllLines(filePath, logData);
            Debug.Log("✅ Gaze log saved: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Failed to save: " + e.Message);
        }

        // 저장 직후 존재 확인
        if (File.Exists(filePath))
            Debug.Log("✅ 저장 확인: 파일이 존재합니다.");
        else
            Debug.Log("❌ 저장 실패: 파일이 존재하지 않습니다.");
    }

    public void DeleteAllLogs()
    {
        try
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*.csv");

            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log("🗑️ 삭제됨: " + file);
            }

            if (files.Length == 0)
            {
                Debug.Log("📁 삭제할 파일이 없습니다.");
            }
            else
            {
                Debug.Log($"✅ 총 {files.Length}개의 로그 파일을 삭제했습니다.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("❌ 로그 파일 삭제 중 오류 발생: " + e.Message);
        }
    }
}
