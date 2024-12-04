using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public Animator animator;
    public FirstController firstController;
    public float power = 0.4f;
    private bool isHolding = false;
    private bool isMouseDown;
    private bool isMouseLongPressed;

    private float longPressDuration = 0.2f; // 长按的初始持续时间
    private float maxChargeTime = 1f; // 最大充能时间（1秒）

    // 引入 AudioController
    public AudioController audioController;

    void Start()
    {
        animator = GetComponent<Animator>();
        firstController = (FirstController)Director.getInstance().currentSceneController;
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    private void Update()
    {
        // 长按左键时逐渐增加力量，直到充满
        if (isMouseLongPressed && !isHolding)
        {
            power = Mathf.Min(power + Time.deltaTime / maxChargeTime, 1f); // 限制充能最大值为 1
        }

        animator.SetFloat("power", power);

        if (firstController.GetArea() && firstController.arrowNum > 0)
        {
            ClickCheck();
        }
    }

    public void ClickCheck()
    {
        // 按下左键
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                isMouseDown = true;
                isMouseLongPressed = false;

                // 开始协程检测长按
                StartCoroutine(CheckLongPress());

                // 触发start
                animator.SetFloat("power", power);
                animator.SetTrigger("start");
            }
            else
            {
                ShootAnimator();
            }
        }
        else if (isMouseLongPressed && Input.GetMouseButtonDown(1)) // 右键按下
        {
            isHolding = true;

            // 停止协程
            StopCoroutine(CheckLongPress());
            isMouseLongPressed = false;

            animator.SetFloat("hold power", power);
            animator.SetTrigger("hold");
        }
        // 松开左键
        else if (isMouseDown && Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;

            if (!isHolding)
            {
                isMouseLongPressed = false;

                // 停止协程
                StopCoroutine(CheckLongPress());

                // 触发hold
                animator.SetFloat("hold power", power);
                animator.SetTrigger("hold");

                // 触发shoot
                ShootAnimator();
            }
        }
    }

    private IEnumerator CheckLongPress()
    {
        yield return new WaitForSeconds(longPressDuration);
        // 如果鼠标处于按下状态，则表示长按
        if (isMouseDown)
        {
            isMouseLongPressed = true;
        }
    }

    private void ShootAnimator()
    {
        animator.SetTrigger("shoot");
        firstController.ShootCallback(true, power);
        isHolding = false;
        power = 0.4f; // 重置力量为初始值
        audioController.PlaySfx(audioController.shoot);
    }
}
