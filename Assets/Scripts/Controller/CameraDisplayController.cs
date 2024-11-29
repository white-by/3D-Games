using UnityEngine;
using UnityEngine.UI;

public class CameraDisplayController : MonoBehaviour
{
    [Header("Main Camera")]
    public Camera mainCamera;  // �������

    [Header("Secondary Camera")]
    public Camera secondaryCamera;  // ��Ҫ�����

    [Header("Raw Image Display")]
    public RawImage rawImage;  // ��ʾ��Ҫ���������� RawImage

    private RenderTexture renderTexture;

    void Start()
    {
        if (secondaryCamera == null || rawImage == null)
        {
            Debug.LogError("��ȷ����Ҫ�������RawImage�����ã�");
            return;
        }

        // ����һ�� RenderTexture ������������Ҫ�����
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        secondaryCamera.targetTexture = renderTexture;

        // �� RenderTexture ����� RawImage
        rawImage.texture = renderTexture;
    }

    void Update()
    {
        // ʾ����ͨ�����̿��ƴ�Ҫ���������ת
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
        // ȷ������ RenderTexture ���ͷ���Դ
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}
