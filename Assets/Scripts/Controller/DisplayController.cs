using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public Camera mainCamera; // �����1
    public Camera secondaryCamera; // �����2
    private bool isMainCameraActive = true; // �����жϵ�ǰ��ʾ�������

    void Start()
    {
        // ȷ�� Display 2 �Ѽ���
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate(); // ���� Display 2
        }
        else
        {
            Debug.LogWarning("û�м�⵽��� Display����ȷ�������ʾ�������ӣ�");
        }

        // ��ʼ�������
        if (mainCamera != null && secondaryCamera != null)
        {
            mainCamera.targetDisplay = 0; // Display 1
            secondaryCamera.targetDisplay = 1; // Display 2
        }
        else
        {
            Debug.LogError("���� Inspector �з����������");
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
            mainCamera.targetDisplay = 0; // �л��� Display 1
            secondaryCamera.targetDisplay = 1; // ��Ҫ������� Display 2
        }
        else
        {
            mainCamera.targetDisplay = 1; // �л���������� Display 2
            secondaryCamera.targetDisplay = 0; // ��Ҫ������� Display 1
        }

    }
}
