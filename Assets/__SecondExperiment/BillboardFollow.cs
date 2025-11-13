using UnityEngine;

public class BillboardFollow : MonoBehaviour
{
    public Transform cam;
    public Vector3 offset = new Vector3(0f, 0f, 1f); // 카메라로부터의 X, Y, Z 거리
    public float followSpeed = 5f; // 위치 따라오는 속도

    void Awake()
    {
        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // 카메라의 Y축 회전만 추출
        float yRotation = cam.eulerAngles.y;

        // Y축 회전만으로 방향 벡터 계산
        Vector3 direction = new Vector3(
            Mathf.Sin(yRotation * Mathf.Deg2Rad),
            0f,
            Mathf.Cos(yRotation * Mathf.Deg2Rad)
        );

        // 목표 위치 계산
        Vector3 targetPos = cam.position + (direction * offset.z) + (Vector3.right * offset.x) + (Vector3.up * offset.y);

        // 위치 보간
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

        // Y축 회전만 적용
        transform.rotation = Quaternion.LookRotation(cam.forward, Vector3.up);
    }
}