using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    UserAction userAction;
    public string gameMessage ;
    public int time;
    GUIStyle style, bigstyle, dstyle, pstyle;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        userAction = Director.GetInstance().CurrentSceneController as UserAction;

        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;
        // 把牧师和魔鬼的颜色设置不一样
        bigstyle = new GUIStyle();
        bigstyle.normal.textColor = Color.white;
        bigstyle.fontSize = 50;
        dstyle = new GUIStyle();
        dstyle.normal.textColor = Color.red;
        dstyle.fontSize = 50;
        pstyle = new GUIStyle();
        pstyle.normal.textColor = Color.yellow;
        pstyle.fontSize = 50;
    }

    // Update is called once per frame
    void OnGUI() {
        userAction.Check();
        GUI.Label(new Rect(250, Screen.height * 0.05f, 50, 200), "Priests", pstyle);
        GUI.Label(new Rect(415, Screen.height * 0.05f, 50, 200), "and", bigstyle);
        GUI.Label(new Rect(520, Screen.height * 0.05f, 50, 200), "Devils", dstyle);
        GUI.Label(new Rect(350, 100, 50, 200), gameMessage, style);
        GUI.Label(new Rect(0, 0, 100, 50), "Time: " + time, style);
        userAction.RestartGame();
    }
}
