using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTarget : MonoBehaviour
{
    public GameObject fixedTarget;  // 固定靶对象

    public FixedTarget(Vector3 position,Quaternion rotation, int scale){
        fixedTarget = GameObject.Instantiate(Resources.Load("Prefabs/Military target", typeof(GameObject))) as GameObject;
        fixedTarget.name = "Military target";
        fixedTarget.transform.position = position;
        fixedTarget.transform.rotation = rotation;
        fixedTarget.transform.localScale = new Vector3(scale, scale, scale);
    }
}
