using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour, ISceneController, IUserAction
{
    int round = 0;
    int max_round = 5;
    float timer = 0.5f;
    GameObject disk;
    DiskFactory factory ;
    public CCActionManager actionManager;
    public ScoreRecorder scoreController;
    public UserGUI userGUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (userGUI.mode == 0)
            return;
        GetHit();
        gameOver();
        if (round > max_round) {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0 && actionManager.RemainActionCount() == 0) {
            //从工厂中得到10个飞碟，为其加上动作
            for (int i = 0; i < 10; ++i) {
                disk = factory.GetDisk(round);
                actionManager.MoveDisk(disk);
            }
            round += 1;
            if (round <= max_round) {
                userGUI.round = round;
            }
            timer = 4.0f;
        }
        
    }
    void Awake() {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadSource();
        gameObject.AddComponent<UserGUI>();
        gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<ScoreRecorder>();
        gameObject.AddComponent<DiskFactory>();
        factory = Singleton<DiskFactory>.Instance;
        userGUI = gameObject.GetComponent<UserGUI>();
        
    }

    public void LoadSource() 
    {

    }

    public void gameOver() 
    {
        if (round > max_round && actionManager.RemainActionCount() == 0){
            int score = userGUI.score;
            userGUI.gameMessage = "游戏结束！:)\n您的分数为: "+score;
        }
        
    }

    public void GetHit() {
        if (Input.GetButtonDown("Fire1")) {
			//create ray, origin is camera, and direction to mousepoint
			Camera ca = Camera.main;
			Ray ray = ca.ScreenPointToRay(Input.mousePosition);

			//Return the ray's hit
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
                scoreController.Record(hit.transform.gameObject);
                
                hit.transform.gameObject.SetActive(false);
			}
		}
    }
}
