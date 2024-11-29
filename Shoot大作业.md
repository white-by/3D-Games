---
title: Shoot大作业
date: 2024-11-29
tags: SYSU
---

## 介绍

这是一个由 Unity 从制作的第一人称射箭游戏。

<!-- more -->

<br>

## 视频

<div style="text-align: center; margin: 20px 0;">
  <iframe 
    src="//player.bilibili.com/player.html?isOutside=true&aid=113565461644649&bvid=BV1FSzzYnEt9&cid=27090617213&p=1"
    scrolling="no"
    border="0"
    frameborder="no"
    framespacing="0"
    allowfullscreen="true"
    style="width: 100%; max-width: 720px; height: 405px;">
  </iframe>
</div>

<br/>

## 得分详解

- 已实现

  - 游戏场景（14分）

    -  地形（2分）：使用**地形组件**，上面有**山、路、草、树**；（可使用第三方资源改造）

      <img src="assets/image-20241129144943969.png" alt="image-20241129144943969" style="zoom: 67%;" />

    -  天空盒（2分）：使用**天空盒**，天空可随 玩家位置 或 时间变化 或 按特定按键**切换天空盒**；

      - 按左Shift切换天空盒

        ```cs
        public class ChangeSkyBox : MonoBehaviour
        {
            public Material[] mats;
            private int index=0;
            // 获取Directional Light组件
            private Light directionalLight;
            public Gradient lightColorGradient;
            public AnimationCurve lightIntensityCurve;
        
            // Start is called before the first frame update
            void Start()
            {
                mats = Resources.LoadAll<Material>("Materials/skyboxs");
                RenderSettings.skybox = mats[0];
                index ++;
                directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
            }
        
            // Update is called once per frame
            void Update()
            {
                if(Input.GetKeyDown(KeyCode.LeftShift)){
                    ChangeBox();
                }
                ChangeLight();
            }
            public void ChangeBox()
            {
                RenderSettings.skybox = mats[index];
                index++;
                index %= mats.Length;
            }
            public void ChangeLight(){
                float currentTime = index*6f;
                // 计算当前的光强度
                float intensity = lightIntensityCurve.Evaluate(currentTime);
                // 计算当前的光颜色
                Color color = lightColorGradient.Evaluate(currentTime / 24f);
                // 设置光源的方向（模拟太阳的移动）
                float rotationAngle = (currentTime - 5f) * 15f; // 每小时15度
                directionalLight.transform.rotation = Quaternion.Euler(rotationAngle, -85f, 0f);
        
                // 设置光源的强度和颜色
                // directionalLight.intensity = intensity;
                directionalLight.color = color;
            }
        }
        ```

    -  固定靶（2分）：使用**静态物体**，有一个以上固定的靶标；（注：射中后状态不会变化）

      - 5个固定靶，其中4个是复制的

        ```csharp
        // FirstController.cs
        public void LoadSource(){
            loadTargets = new LoadTargets[4];
            for (int i = 0; i < 4; ++i)
            {
                int scale=50;
                if(i==2)
                    scale = 80;
                else if(i==3)
                    scale = 70;
                loadTargets[i] = new LoadTargets();
                loadTargets[i].CreateFixedTarget(ObjectPosition.fixedT[i],ObjectPosition.fixedTr[i],scale);
            }
        }
        
        // LoadTargets.cs
        public class LoadTargets : MonoBehaviour
        {
            FixedTarget ftargetmodel;
        
            public void CreateFixedTarget(Vector3 position,Quaternion rotation ,int scale) {
                if (ftargetmodel != null) {
                    Object.Destroy(ftargetmodel.fixedTarget);
                }
                ftargetmodel = new FixedTarget(position, rotation, scale);
            }
        
            public FixedTarget GetFixedTModel() {
                return ftargetmodel;
            }
        }
        ```

    -  运动靶（2分）：使用**动画运动**，有一个以上运动靶标，运动轨迹，速度使用动画控制；（注：射中后需要有效果或自然落下）

      - 三个移动靶，运动方式分别为水平、上下、圆周运动

        <img src="assets/image-20241129144618423.png" alt="image-20241129144618423" style="zoom:80%;" />

        ![image-20241129144705710](assets/image-20241129144705710.png)

    -  射击位（2分）：地图上应标记若干射击位，仅在射击位附近或区域可以拉弓射击，每个位置有 n 次机会；

      - 在移动靶和固定靶前面均有一块射击台

        ```csharp
        public class ShootArea : MonoBehaviour
        {
            //是否可以射箭
            private bool canShoot;
            public FirstController firstController;
        
            void Start(){
                firstController = (FirstController)Director.getInstance().currentSceneController;
            }
         
            public void OnTriggerEnter(Collider collider)
            {
                if (collider.gameObject.tag == "Player")
                {
                    canShoot = true;
                    firstController.AreaCallBack(canShoot);
                }
            }
         
         
            private void OnTriggerExit(Collider collider)
            {
                if (collider.gameObject.tag == "Player")
                {
                    canShoot = false;
                    firstController.AreaCallBack(canShoot);
                }
            }
        }
        ```

        

    -  摄像机（2分）：使用**多摄像机**，制作 鸟瞰图 或 瞄准镜图 使得游戏更加易于操控；

      - 制作了一台俯瞰视角的跟随摄影机，固定在屏幕右上角

        ```csharp
        public class UIFollowCamera : MonoBehaviour
        {
            public Camera mainCamera; // 主摄像机
            public Vector3 offset = new Vector3(-0, -0, 0); // RawImage 在屏幕上的偏移量
        
            private RectTransform uiTransform;
        
            void Start()
            {
                // 获取 RawImage 的 RectTransform
                uiTransform = GetComponent<RectTransform>();
                if (mainCamera == null)
                {
                    mainCamera = Camera.main; // 自动获取主摄像机
                }
            }
        
            void Update()
            {
                // 获取主摄像机的屏幕右上角位置
                Vector3 screenPosition = mainCamera.ViewportToScreenPoint(new Vector3(1, 1, 0)); // 右上角
                uiTransform.position = screenPosition + offset;
            }
        }
        ```

        - 跟随玩家

        ```cs
        public class OverheadFollowCamera : MonoBehaviour
        {
            public Transform player; // 玩家对象
            public Vector3 offset = new Vector3(0, 10, -10); // 摄像机相对于玩家的偏移
            public float followSpeed = 5f; // 跟随平滑速度
        
            void LateUpdate()
            {
                if (player == null)
                {
                    Debug.LogWarning("Player 未绑定！");
                    return;
                }
        
                // 计算摄像机目标位置
                Vector3 targetPosition = player.position + offset;
        
                // 平滑移动摄像机
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        
                // 始终让摄像机朝向玩家
                transform.LookAt(player);
            }
        }
        ```

    -  声音（2分）：使用**声音组件**，播放背景音 与 箭射出的声效；

      - 有循环的bgm以及射击音效

        ```csharp
        public class AudioController : MonoBehaviour
        {
            [SerializeField] AudioSource BgmAudio;
            [SerializeField] AudioSource SfxAudio;
        
            public AudioClip bgm;
            public AudioClip shoot;
        
            private void Start()
            {
                BgmAudio.clip = bgm;
                BgmAudio.Play();
            }
        
            public void PlaySfx(AudioClip clip)
            {
                SfxAudio.PlayOneShot(clip);
            }
        }
        ```

  - 运动与物理与动画（8分）

    -  游走（2分）：使用**第一人称组件**，玩家的驽弓可在地图上游走，不能碰上树和靶标等障碍；（注：建议使用 [unity 官方案例](https://assetstore.unity.com/packages/essentials/starter-assets-firstperson-updates-in-new-charactercontroller-pa-196525)）

      - 具体效果请看视频演示

        <img src="assets/image-20241129150019629.png" alt="image-20241129150019629" style="zoom:80%;" />

        <img src="assets/image-20241129150119687.png" alt="image-20241129150119687" style="zoom: 67%;" />

        - 按Space跳跃功能

          ```csharp
          // PlayerController.cs
          public class PlayerController : MonoBehaviour
          {
              private CharacterController cc;
          
              public float moveSpeed = 80.0f;   // 移动速度
              public float jumpSpeed = 20.0f;   // 跳跃初速度
              public float gravity = 20.0f;     // 重力加速度
              public float fallMultiplier = 2.5f; // 加强下落加速度的倍数
          
              private float horizontalMove, verticalMove;
              private Vector3 dir;
              private Vector3 velocity;
          
              public Transform groundCheck;
              public float checkRedius = 0.2f;
              public LayerMask groundLayer;
              public bool isGround;
          
              private void Start()
              {
                  cc = GetComponent<CharacterController>();
                  Cursor.lockState = CursorLockMode.Locked;
                  Cursor.visible = false;
              }
          
              private void Update()
              {
                  isGround = Physics.CheckSphere(groundCheck.position, checkRedius, groundLayer);
          
                  if (isGround && velocity.y < 0)
                  {
                      velocity.y = -2f;  // 确保贴地
                  }
          
                  horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
                  verticalMove = Input.GetAxis("Vertical") * moveSpeed;
          
                  dir = transform.forward * verticalMove + transform.right * horizontalMove;
                  cc.Move(dir * Time.deltaTime);
          
                  if (Input.GetKeyDown(KeyCode.Space) && isGround)
                  {
                      velocity.y = jumpSpeed;  // 初始跳跃速度
                  }
          
                  // 重力处理
                  if (velocity.y < 0)
                  {
                      velocity.y -= gravity * fallMultiplier * Time.deltaTime;  // 更快地下落
                  }
                  else
                  {
                      velocity.y -= gravity * Time.deltaTime;  // 正常的上升重力
                  }
          
                  cc.Move(velocity * Time.deltaTime);
              }
          
              private void OnDrawGizmos()
              {
                  Gizmos.color = Color.red;
                  Gizmos.DrawWireSphere(groundCheck.position, checkRedius);
              }
          }
          ```

    -  射击效果（2分）：使用 **物理引擎** 或 **动画** 或 **粒子**，运动靶被射中后产生适当效果。

      - 具体效果请看视频演示

      - 射中后箭会留在靶上，没射中会掉在地上被风吹走

        <img src="assets/image-20241129152706001.png" alt="image-20241129152706001" style="zoom:80%;" />

      - 箭的物理效果

        <img src="assets/image-20241129152122983.png" alt="image-20241129152122983" style="zoom:80%;" />

      - 引入power模拟射击效果

        ```csharp
            public void ShootCallback(bool isShot, float power)
            {
                shotpower = power;
                shot = isShot;
            }
        ```

        ```csharp
         // 长按左键不断增加力量
         if (isMouseLongPressed && !isHolding)
         {
             power = Mathf.Min(power + Time.deltaTime, 1f);
         }
        ```

    -  碰撞与计分（2分）：使用 **计分类** 管理规则，在射击位射中靶标得相应分数，规则自定；（注：应具有现场修改游戏规则能力）

      - 具体效果请看视频演示

      - 靶子分为low、mid、high三个积分区域，分数由边缘到中心依次为2、4、6分

      - 计分类

        ```csharp
        public class ScoreRecorder : MonoBehaviour
        {
            int score;
            public FirstController firstController;
            public UserGUI userGUI;
            // Start is called before the first frame update
            void Start()
            {
                firstController = (FirstController)Director.getInstance().currentSceneController;
                firstController.scoreController = this;
                userGUI = this.gameObject.GetComponent<UserGUI>();
            }
        
            public void Record(int ringscore) {
                score += ringscore;
                userGUI.score = score;
            }
        }
        ```

    -  驽弓动画（2分）：使用 **动画机** 与 **动画融合**, 实现十字驽蓄力**半拉弓**，然后 **hold**，择机 **shoot**；

      - 具体效果请看视频演示

        <img src="assets/image-20241129164615842.png" alt="image-20241129164615842" style="zoom:80%;" />
      
        <img src="assets/image-20241129164602396.png" alt="image-20241129164602396" style="zoom:80%;" />
      
      - 半拉弓
      
        <img src="assets/image-20241129164750988.png" alt="image-20241129164750988" style="zoom:80%;" />
      
        
      
        ```csharp
        public class ShootControl : MonoBehaviour
        {
            public Animator animator;
            public FirstController firstController;
            public float power = 0.4f;
            private bool isHolding = false;
            private bool isMouseDown;
            private bool isMouseLongPressed;
        
            private float longPressDuration = 0.2f; // 长按的初始持续时间
            private float maxChargeTime = 1f; // 最大充能时间（1秒）
        
            // 引入 AudioController
            public AudioController audioController;
        
            void Start()
            {
                animator = GetComponent<Animator>();
                firstController = (FirstController)Director.getInstance().currentSceneController;
                audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
            }
        
            private void Update()
            {
                // 长按左键时逐渐增加力量，直到充满
                if (isMouseLongPressed && !isHolding)
                {
                    power = Mathf.Min(power + Time.deltaTime / maxChargeTime, 1f); // 限制充能最大值为 1
                }
        
                animator.SetFloat("power", power);
        
                if (firstController.GetArea() && firstController.arrowNum > 0)
                {
                    ClickCheck();
                }
            }
        
            public void ClickCheck()
            {
                // 按下左键
                if (Input.GetMouseButtonDown(0))
                {
                    if (!isHolding)
                    {
                        isMouseDown = true;
                        isMouseLongPressed = false;
        
                        // 开始协程检测长按
                        StartCoroutine(CheckLongPress());
        
                        // 触发start
                        animator.SetFloat("power", power);
                        animator.SetTrigger("start");
                    }
                    else
                    {
                        ShootAnimator();
                    }
                }
                else if (isMouseLongPressed && Input.GetMouseButtonDown(1)) // 右键按下
                {
                    isHolding = true;
        
                    // 停止协程
                    StopCoroutine(CheckLongPress());
                    isMouseLongPressed = false;
        
                    animator.SetFloat("hold power", power);
                    animator.SetTrigger("hold");
                }
                // 松开左键
                else if (isMouseDown && Input.GetMouseButtonUp(0))
                {
                    isMouseDown = false;
        
                    if (!isHolding)
                    {
                        isMouseLongPressed = false;
        
                        // 停止协程
                        StopCoroutine(CheckLongPress());
        
                        // 触发hold
                        animator.SetFloat("hold power", power);
                        animator.SetTrigger("hold");
        
                        // 触发shoot
                        ShootAnimator();
                    }
                }
            }
        
            private IEnumerator CheckLongPress()
            {
                yield return new WaitForSeconds(longPressDuration);
                // 如果鼠标处于按下状态，则表示长按
                if (isMouseDown)
                {
                    isMouseLongPressed = true;
                }
            }
        
            private void ShootAnimator()
            {
                animator.SetTrigger("shoot");
                firstController.ShootCallback(true, power);
                isHolding = false;
                power = 0.4f; // 重置力量为初始值
                audioController.PlaySfx(audioController.shoot);
            }
        }
        ```
      
        

