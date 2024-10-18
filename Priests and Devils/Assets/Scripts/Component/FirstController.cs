using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController : MonoBehaviour, SceneController, UserAction
{
    public CCActionManager actionManager;
    public ShoreCtrl leftShoreController, rightShoreController;
    public River river;
    public BoatCtrl boatController;
    public RoleCtrl[] roleControllers;
    public bool isRunning;
    public float time;

    public void JudgeCallback(bool isRuning, string message)
    {
        this.gameObject.GetComponent<UserGUI>().gameMessage = message;
        this.gameObject.GetComponent<UserGUI>().time = (int)time;
        this.isRunning = isRunning;

    }

    public void LoadResources()
    {
        //role
        roleControllers = new RoleCtrl[6];
        for (int i = 0; i < 6; ++i)
        {
            roleControllers[i] = new RoleCtrl();
            roleControllers[i].CreateRole(Position.role_shore[i], i < 3 ? true : false, i);
        }

        // 加载此岸和彼岸
        leftShoreController = new ShoreCtrl();
        leftShoreController.CreateShore(Position.left_shore);
        leftShoreController.GetShore().shore.name = "this_shore";
        rightShoreController = new ShoreCtrl();
        rightShoreController.CreateShore(Position.right_shore);
        rightShoreController.GetShore().shore.name = "other_shore";

        // 将人物添加并定位至左边 
        foreach (RoleCtrl roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition = leftShoreController.AddRole(roleController.GetRoleModel());
        }
        //boat
        boatController = new BoatCtrl();
        boatController.CreateBoat(Position.left_boat);

        //river
        river = new River(Position.river);

        isRunning = true;
        time = 60;
    }

    public void MoveBoat()
    {
        if (isRunning == false || actionManager.IsMoving())
            return;

        Vector3 destination = boatController.GetBoatModel().isRight ? Position.left_boat : Position.right_boat;
        actionManager.MoveBoat(boatController.GetBoatModel().boat, destination, 5);

        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;

    }

    public void MoveRole(Role roleModel)
    {
        if (isRunning == false || actionManager.IsMoving())
            return;
        Vector3 destination, mid_destination;
        if (roleModel.inBoat)
        {

            if (boatController.GetBoatModel().isRight)
                destination = rightShoreController.AddRole(roleModel);
            else
                destination = leftShoreController.AddRole(roleModel);
            if (roleModel.role.transform.localPosition.y > destination.y)
                mid_destination = new Vector3(destination.x, roleModel.role.transform.localPosition.y, destination.z);
            else
                mid_destination = new Vector3(roleModel.role.transform.localPosition.x, destination.y, destination.z);

            actionManager.MoveRole(roleModel.role, mid_destination, destination, 10);
            roleModel.onRight = boatController.GetBoatModel().isRight;
            boatController.RemoveRole(roleModel);
        }
        else
        {

            if (boatController.GetBoatModel().isRight == roleModel.onRight)
            {
                if (roleModel.onRight)
                {
                    rightShoreController.RemoveRole(roleModel);
                }
                else
                {
                    leftShoreController.RemoveRole(roleModel);
                }
                destination = boatController.AddRole(roleModel);
                if (roleModel.role.transform.localPosition.y > destination.y)
                    mid_destination = new Vector3(destination.x, roleModel.role.transform.localPosition.y, destination.z);
                else
                    mid_destination = new Vector3(roleModel.role.transform.localPosition.x, destination.y, destination.z);
                actionManager.MoveRole(roleModel.role, mid_destination, destination, 5);
            }
        }
    }

    public void Check(){    }

    public void RestartGame()
    {
        if (GUI.Button(new Rect(0, 35, 100, 30), "Restart"))
        {
            // 重新加载游戏场景，只有一个场景，那编号就是0
            SceneManager.LoadScene(0);
        }
    }

    void Awake()
    {
        Director.GetInstance().CurrentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<JudgeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            time -= Time.deltaTime;
            this.gameObject.GetComponent<UserGUI>().time = (int)time;
        }
    }
}
