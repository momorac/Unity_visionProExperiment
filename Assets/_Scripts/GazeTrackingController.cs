using UnityEngine;
using System.IO;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

// public class GazeTrackingController : MonoBehaviour
// {
//     [SerializeField] float interval = 1.0f;
//     float timer = 0;
//     float timer_interval = 0;

//     private List<string> logData = new List<string>();
//     private string filePath;

//     private Manager manager;
//     [HideInInspector] public bool is_started;

//     private void Awake()
//     {
//         manager = GetComponent<Manager>();
//     }

//     void Start()
//     {
//         is_started = false;
//         string time = DateTime.Now.ToString("MMdd_HHmm");
//         filePath = Path.Combine(Application.persistentDataPath, $"GazeLog_{time}.csv");
//         logData.Add("Time,Distance,Page,Detail,XPos,YPos,ZPos"); // 헤더

//         // DeleteAllLogs();
//     }

//     void Update()
//     {
//         if (!is_started) return;

//         timer_interval += Time.deltaTime;
//         timer += Time.deltaTime;
//         if (timer_interval >= interval)
//         {
//             timer_interval = 0f;
//             SimulateVirtualTouch();
//         }
//     }

//     void SimulateVirtualTouch()
//     {
//         Ray gazeRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//         if (Physics.Raycast(gazeRay, out RaycastHit hit, 10f))
//         {
//             var fakeTouch = new SpatialPointerState
//             {
//                 interactionPosition = hit.point,
//                 inputDevicePosition = Camera.main.transform.position,
//                 inputDeviceRotation = Camera.main.transform.rotation,
//                 startInteractionRayOrigin = gazeRay.origin,
//                 startInteractionRayDirection = gazeRay.direction,
//                 Kind = SpatialPointerKind.Touch, // 실제 입력 종류와 일치시킴
//                 // targetObject = hit.collider.gameObject
//             };

//             // 예시로 표시
//             // Debug.Log($"Virtual Touch at {fakeTouch.interactionPosition}");

//             string entry = $"{timer:F2},{manager.currentDistance},{manager.currentState},{manager.currentDetail},{fakeTouch.interactionPosition.x:F5},{fakeTouch.interactionPosition.y:F5},{fakeTouch.interactionPosition.z:F5}";
//             logData.Add(entry);

//         }

//     }
//     public void SaveLog()
//     {
//         try
//         {
//             File.WriteAllLines(filePath, logData);
//             Debug.Log("✅ Gaze log saved: " + filePath);
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogError("❌ Failed to save: " + e.Message);
//         }

//         // 저장 직후 존재 확인
//         if (File.Exists(filePath))
//             Debug.Log("✅ 저장 확인: 파일이 존재합니다.");
//         else
//             Debug.Log("❌ 저장 실패: 파일이 존재하지 않습니다.");
//     }

//     public void DeleteAllLogs()
//     {
//         try
//         {
//             string[] files = Directory.GetFiles(Application.persistentDataPath, "GazeLog_*.csv");

//             foreach (string file in files)
//             {
//                 File.Delete(file);
//                 Debug.Log("🗑️ 삭제됨: " + file);
//             }

//             if (files.Length == 0)
//             {
//                 Debug.Log("📁 삭제할 파일이 없습니다.");
//             }
//             else
//             {
//                 Debug.Log($"✅ 총 {files.Length}개의 로그 파일을 삭제했습니다.");
//             }
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("❌ 로그 파일 삭제 중 오류 발생: " + e.Message);
//         }
//     }

// }
