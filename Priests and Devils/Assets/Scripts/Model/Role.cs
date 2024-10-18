using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role 
{
    public GameObject role; //model对应的游戏对象
    public bool isPastor;
    public bool inBoat;
    public bool onRight;
    public int id;
    
    public Role (Vector3 position, bool isPastor, int id) {
        this.isPastor = isPastor;
        this.id = id;
        onRight = false;
        inBoat = false;
        role = GameObject.Instantiate(Resources.Load("Prefabs/" + (isPastor ? "pastor" : "devil"), typeof(GameObject))) as GameObject;
        role.transform.localScale = new Vector3(1, 1.2f, 1);
        role.transform.position = position;
        role.name = "role" + id;
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
    }
}
