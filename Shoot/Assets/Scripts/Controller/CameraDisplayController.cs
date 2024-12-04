using UnityEngine;
using UnityEngine.UI;

public class CameraDisplayController : MonoBehaviour
{
    [Header("Main Camera")]
    public Camera mainCamera;  // 主摄像机

    [Header("Secondary Camera")]
    public Camera secondaryCamera;  // 次要摄像机

    [Header("Raw Image Display")]
    public RawImage rawImage;  // 显示次要摄像机画面的 RawImage

    private RenderTexture renderTexture;

    void Start()
    {
        if (secondaryCamera == null || rawImage == null)
        {
            Debug.LogError("请确保次要摄像机和RawImage已设置！");
            return;
        }

        // 创建一个 RenderTexture 并将其分配给次要摄像机
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        secondaryCamera.targetTexture = renderTexture;

        // 将 RenderTexture 分配给 RawImage
        rawImage.texture = renderTexture;
    }

    void Update()
    {
        // 示例：通过键盘控制次要摄像机的旋转
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            secondaryCamera.transform.Rotate(Vector3.up, -50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            secondaryCamera.transform.Rotate(Vector3.up, 50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            secondaryCamera.transform.Rotate(Vector3.right, -50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            secondaryCamera.transform.Rotate(Vector3.right, 50 * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        // 确保销毁 RenderTexture 以释放资源
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}
