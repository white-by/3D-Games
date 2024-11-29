using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;

    public float moveSpeed = 80.0f;   // 移动速度
    public float jumpSpeed = 20.0f;   // 跳跃初速度
    public float gravity = 20.0f;     // 重力加速度
    public float fallMultiplier = 2.5f; // 加强下落加速度的倍数

    private float horizontalMove, verticalMove;
    private Vector3 dir;
    private Vector3 velocity;

    public Transform groundCheck;
    public float checkRedius = 0.2f;
    public LayerMask groundLayer;
    public bool isGround;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, checkRedius, groundLayer);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;  // 确保贴地
        }

        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        dir = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(dir * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = jumpSpeed;  // 初始跳跃速度
        }

        // 重力处理
        if (velocity.y < 0)
        {
            velocity.y -= gravity * fallMultiplier * Time.deltaTime;  // 更快地下落
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;  // 正常的上升重力
        }

        cc.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRedius);
    }
}
