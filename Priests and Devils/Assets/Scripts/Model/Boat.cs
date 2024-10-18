using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat
{
    public GameObject boat; //船对象
    public Role[] roles;    //船上的角色
    public bool isRight;    //用于判断船的位置
    public int pastorCount, devilCount;

    public Boat(Vector3 position) {
        boat = GameObject.Instantiate(Resources.Load("Prefabs/boat", typeof(GameObject))) as GameObject;
        boat.name = "boat";
        boat.transform.position = position;
        boat.transform.localScale = new Vector3(2.8f, 0.4f, 2);

        roles = new Role[2];    // 只能有两个角色
        isRight = false;        // 初始在右边
        pastorCount = devilCount = 0;

        boat.AddComponent<BoxCollider>();
        boat.AddComponent<Click>();
    }
}
