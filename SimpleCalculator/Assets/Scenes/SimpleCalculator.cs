using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCalculator : MonoBehaviour
{

    // Entities and their states / Model
    private string input = ""; // 用户输入
    private float result = 0;  // 计算结果
    private string operation = ""; // 当前操作

    // System Handlers
    void OnGUI()
    {
        // 获取屏幕尺寸
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 定义每个按钮和显示框的大小
        float boxWidth = 300f;
        float boxHeight = 60f;
        float buttonWidth = 80f;
        float buttonHeight = 60f;
        float spacing = 10f;

        // 居中显示输入和结果框
        GUI.Box(new Rect((screenWidth - boxWidth) / 2 + 10, screenHeight / 2 - 200, boxWidth - 40, boxHeight),
            input != "" ? input : result.ToString());

        // 布局数字按钮
        for (int i = 1; i <= 9; i++)
        {
            float xPos = (screenWidth - (3 * buttonWidth + 2 * spacing)) / 2 + ((i - 1) % 3) * (buttonWidth + spacing);
            float yPos = screenHeight / 2 - 100 + ((i - 1) / 3) * (buttonHeight + spacing);

            if (GUI.Button(new Rect(xPos - 10, yPos, buttonWidth, buttonHeight), i.ToString()))
            {
                input += i.ToString();
            }
        }

        // 单独布局 "0" 按钮
        if (GUI.Button(new Rect((screenWidth - buttonWidth) / 2 - 10, screenHeight / 2 + 110, buttonWidth, buttonHeight), "0"))
        {
            input += "0";
        }

        // 布局操作符按钮
        string[] operators = { "+", "-", "*", "/" };
        for (int i = 0; i < operators.Length; i++)
        {
            float xPos = (screenWidth - (3 * buttonWidth + 2 * spacing)) / 2 + 3 * (buttonWidth + spacing);
            float yPos = screenHeight / 2 - 100 + i * (buttonHeight + spacing);

            if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), operators[i]))
            {
                SetOperation(operators[i]);
            }
        }

        // 布局 "=" 和 "C" 按钮
        if (GUI.Button(new Rect((screenWidth - buttonWidth) / 2 + buttonWidth + spacing - 10, screenHeight / 2 + 110, buttonWidth, buttonHeight), "="))
        {
            Calculate();
        }
        if (GUI.Button(new Rect((screenWidth - buttonWidth) / 2 - buttonWidth - spacing - 10, screenHeight / 2 + 110, buttonWidth, buttonHeight), "C"))
        {
            Clear();
        }
    }

    // Components /controls
    void SetOperation(string op)
    {
        if (input != "")
        {
            result = float.Parse(input);
            input = "";
        }
        operation = op;
    }

    void Calculate()
    {
        if (input != "")
        {
            float second = float.Parse(input);
            switch (operation)
            {
                case "+": result += second; break;
                case "-": result -= second; break;
                case "*": result *= second; break;
                case "/": result /= second; break;
            }
            input = "";
            operation = "";
        }
    }

    void Clear()
    {
        input = "";
        result = 0;
        operation = "";
    }
}
