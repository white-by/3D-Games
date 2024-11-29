using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public Camera mainCamera; // 摄像机1
    public Camera secondaryCamera; // 摄像机2
    private bool isMainCameraActive = true; // 用于判断当前显示的摄像机

    void Start()
    {
        // 确保 Display 2 已激活
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate(); // 激活 Display 2
        }
        else
        {
            Debug.LogWarning("没有检测到多个 Display，请确保外接显示器已连接！");
        }

        // 初始化摄像机
        if (mainCamera != null && secondaryCamera != null)
        {
            mainCamera.targetDisplay = 0; // Display 1
            secondaryCamera.targetDisplay = 1; // Display 2
        }
        else
        {
            Debug.LogError("请在 Inspector 中分配摄像机！");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchDisplay();
        }
    }

    void SwitchDisplay()
    {
        isMainCameraActive = !isMainCameraActive;

        if (isMainCameraActive)
        {
            mainCamera.targetDisplay = 0; // 切换回 Display 1
            secondaryCamera.targetDisplay = 1; // 次要摄像机在 Display 2
        }
        else
        {
            mainCamera.targetDisplay = 1; // 切换主摄像机到 Display 2
            secondaryCamera.targetDisplay = 0; // 次要摄像机到 Display 1
        }

    }
}
