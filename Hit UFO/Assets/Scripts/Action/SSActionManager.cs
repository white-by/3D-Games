using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>(); 
    
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        foreach (SSAction ac in waitingAdd) {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();
        foreach(KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.destroy) {
                waitingDelete.Add(ac.GetInstanceID());
            } 
            else if (ac.enable) {
                ac.Update();
            }
        }

        foreach(int key in waitingDelete) {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameObject, SSAction action, IActionCallback manager) {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public int RemainActionCount() {
        return actions.Count;
    }
}
