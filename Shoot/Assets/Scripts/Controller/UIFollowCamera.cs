using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // 主摄像机
    public Vector3 offset = new Vector3(-0, -0, 0); // RawImage 在屏幕上的偏移量

    private RectTransform uiTransform;

    void Start()
    {
        // 获取 RawImage 的 RectTransform
        uiTransform = GetComponent<RectTransform>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 自动获取主摄像机
        }
    }

    void Update()
    {
        // 获取主摄像机的屏幕右上角位置
        Vector3 screenPosition = mainCamera.ViewportToScreenPoint(new Vector3(1, 1, 0)); // 右上角
        uiTransform.position = screenPosition + offset;
    }
}
