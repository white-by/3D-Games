using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // �������
    public Vector3 offset = new Vector3(-0, -0, 0); // RawImage ����Ļ�ϵ�ƫ����

    private RectTransform uiTransform;

    void Start()
    {
        // ��ȡ RawImage �� RectTransform
        uiTransform = GetComponent<RectTransform>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // �Զ���ȡ�������
        }
    }

    void Update()
    {
        // ��ȡ�����������Ļ���Ͻ�λ��
        Vector3 screenPosition = mainCamera.ViewportToScreenPoint(new Vector3(1, 1, 0)); // ���Ͻ�
        uiTransform.position = screenPosition + offset;
    }
}
