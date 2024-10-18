using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shore 
{
    public GameObject shore;
    public int pastorCount, devilCount;
    public Shore (Vector3 position){
        shore = GameObject.Instantiate(Resources.Load("Prefabs/this_shore", typeof(GameObject))) as GameObject;
        shore.transform.localScale = new Vector3(8, 4, 2);
        shore.transform.position = position;
        pastorCount = devilCount = 0;
    }
}
