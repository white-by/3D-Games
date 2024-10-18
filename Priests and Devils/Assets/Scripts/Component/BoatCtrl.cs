using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCtrl : ClickAction
{
    Boat boatModel;
    UserAction userAction;

    public BoatCtrl() {
        userAction = Director.GetInstance().CurrentSceneController as UserAction;
    }
    
    public void CreateBoat(Vector3 position) {
        if (boatModel != null) {
            Object.Destroy(boatModel.boat);
        }
        boatModel = new Boat(position);
        boatModel.boat.GetComponent<Click>().setClickAction(this);
    }

    public Boat GetBoatModel() {
        return boatModel;
    }

    //将角色从岸上移动到船上，返回接下来角色应该到达的位置
    public Vector3 AddRole(Role roleModel) {
        int index = -1;
        if (boatModel.roles[0] == null) index = 0;
        else if (boatModel.roles[1] == null) index = 1;

        if (index == -1) return roleModel.role.transform.localPosition;

        boatModel.roles[index] = roleModel;
        roleModel.inBoat = true;
        roleModel.role.transform.parent = boatModel.boat.transform;
        if (roleModel.isPastor) boatModel.pastorCount++;
        else boatModel.devilCount++;
        return Position.role_boat[index];
    }

    //将角色从船上移到岸上
    public void RemoveRole(Role roleModel) {
        for (int i = 0; i < 2; ++i){
            if (boatModel.roles[i] == roleModel) {
                boatModel.roles[i] = null;
                if (roleModel.isPastor) boatModel.pastorCount--;
                else boatModel.devilCount--;
                break;
            }
        }
    }

    public void DealClick() {
        if (boatModel.roles[0] != null || boatModel.roles[1] != null) {
            userAction.MoveBoat();
        }
    }
}
