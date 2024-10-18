using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour
{
    public FirstController mainController;
    public Shore leftShoreModel;
    public Shore rightShoreModel;
    public Boat boatModel;
    
    // Start is called before the first frame update
    void Start()
    {
        mainController = (FirstController)Director.GetInstance().CurrentSceneController;
        this.leftShoreModel = mainController.leftShoreController.GetShore();
        this.rightShoreModel = mainController.rightShoreController.GetShore();
        this.boatModel = mainController.boatController.GetBoatModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainController.isRunning)
            return;
        if (mainController.time <= 0)
        {
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        //判断是否已经胜利
        if (rightShoreModel.pastorCount == 3 && leftShoreModel.devilCount == 3)
        {
            mainController.JudgeCallback(false, "You Win!");
            return;
        }
        else
        {
            
            int leftPastorNum, leftDevilNum, rightPastorNum, rightDevilNum;
            leftPastorNum = leftShoreModel.pastorCount + (boatModel.isRight ? 0 : boatModel.pastorCount);
            leftDevilNum = leftShoreModel.devilCount + (boatModel.isRight ? 0 : boatModel.devilCount);
            if (leftPastorNum != 0 && leftPastorNum < leftDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
            rightPastorNum = rightShoreModel.pastorCount + (boatModel.isRight ? boatModel.pastorCount : 0);
            rightDevilNum = rightShoreModel.devilCount + (boatModel.isRight ? boatModel.devilCount : 0);
            if (rightPastorNum != 0 && rightPastorNum < rightDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
        }
    }
}

