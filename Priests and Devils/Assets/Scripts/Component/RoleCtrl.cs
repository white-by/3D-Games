using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : ClickAction
{
    Role roleModel;
    UserAction userAction;

    public RoleCtrl() {
        userAction = Director.GetInstance().CurrentSceneController as UserAction;
    }

    public void CreateRole(Vector3 position, bool isPastor, int id) {
        if (roleModel != null) {
            Object.DestroyImmediate(roleModel.role);
        }
        roleModel = new Role(position, isPastor, id);
        roleModel.role.GetComponent<Click>().setClickAction(this);
    }

    public Role GetRoleModel() {
        return roleModel;
    }

    public void DealClick() {
        userAction.MoveRole(roleModel);
    }
}
