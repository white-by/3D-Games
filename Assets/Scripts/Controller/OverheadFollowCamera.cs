using UnityEngine;

public class OverheadFollowCamera : MonoBehaviour
{
    public Transform player; // 玩家对象
    public Vector3 offset = new Vector3(0, 10, -10); // 摄像机相对于玩家的偏移
    public float followSpeed = 5f; // 跟随平滑速度

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player 未绑定！");
            return;
        }

        // 计算摄像机目标位置
        Vector3 targetPosition = player.position + offset;

        // 平滑移动摄像机
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 始终让摄像机朝向玩家
        transform.LookAt(player);
    }
}
