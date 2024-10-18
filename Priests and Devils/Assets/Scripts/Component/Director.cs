using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : System.Object   // 项目的导演类
{
    // 单实例
    static Director instance;
    public SceneController CurrentSceneController {get; set;}
    public static Director GetInstance() {
        if (instance == null) {
            instance = new Director();
        }
        return instance;
    }
}
