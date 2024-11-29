using UnityEngine;

public class OverheadFollowCamera : MonoBehaviour
{
    public Transform player; // ��Ҷ���
    public Vector3 offset = new Vector3(0, 10, -10); // ������������ҵ�ƫ��
    public float followSpeed = 5f; // ����ƽ���ٶ�

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player δ�󶨣�");
            return;
        }

        // ���������Ŀ��λ��
        Vector3 targetPosition = player.position + offset;

        // ƽ���ƶ������
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ʼ����������������
        transform.LookAt(player);
    }
}
