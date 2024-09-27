## 简答题

**游戏对象（GameObjects） 和 资源（Assets）的区别与联系：**

- **游戏对象 (GameObjects)：** 游戏对象是场景中存在的实体，它们具有位置、旋转和缩放属性，主要通过 `Transform` 组件管理。游戏对象是可以在场景中被实例化的对象，它们往往通过组件（`Component`）来赋予功能，如物理特性、行为脚本等。
- **资源 (Assets)：** 资源是指构成游戏世界的文件，如模型、纹理、音频、脚本等，它们存储在 `Assets` 文件夹下。资源本身不能直接用于游戏，需要通过被关联到游戏对象上使用。
- **联系：** 游戏对象往往由资源组成。例如，一个 3D 模型文件（Asset）会被挂载到一个 `GameObject` 上，成为场景中的可视对象。资源也可以通过预制体（Prefab）被实例化为游戏对象。

**资源与对象组织结构总结：**

- 下载不同的游戏项目后，常见的资源组织结构包括以下：
  - **Assets 文件夹** 中的资源分类可能为 `Models`, `Textures`, `Materials`, `Scripts` 等，便于管理。
  - 游戏对象树结构（Hierarchy）按场景逻辑组织，通常包括根节点下的多个子对象。例如，一个 "Character" 对象下可以挂载 "Head", "Body", "Arm" 等子对象。

**验证 MonoBehaviour 基本行为或事件的代码示例：**

```csharp
using UnityEngine;

public class MonoBehaviourTest : MonoBehaviour
{
    void Awake() {
        Debug.Log("Awake called");
    }

    void Start() {
        Debug.Log("Start called");
    }

    void Update() {
        Debug.Log("Update called");
    }

    void FixedUpdate() {
        Debug.Log("FixedUpdate called");
    }

    void LateUpdate() {
        Debug.Log("LateUpdate called");
    }

    void OnGUI() {
        Debug.Log("OnGUI called");
    }

    void OnEnable() {
        Debug.Log("OnEnable called");
    }

    void OnDisable() {
        Debug.Log("OnDisable called");
    }
}

```

**GameObject，Transform，Component 对象的官方描述翻译：**

- **GameObject：** GameObject 是 Unity 场景中所有实体的基本构建块。它们不一定有视觉表现或物理属性，但可以通过添加组件获得不同的功能。
- **Transform：** Transform 是每个 GameObject 自带的组件，负责物体在 3D 空间中的位置、旋转和缩放。它定义了对象的坐标系。
- **Component：** Component 是所有行为的基础，任何功能（如渲染、物理）都通过附加到 GameObject 的组件来实现。

**table 对象的属性：**

- **table 的 GameObject：** 包含基本的 activeSelf 属性，用于控制对象是否激活。
- **Transform 属性：** 位置、旋转、缩放（`Position`, `Rotation`, `Scale`）。
- **table 的组件：** 可能包含 `MeshRenderer`, `Collider`, `Rigidbody` 等组件，定义了外观、碰撞检测和物理行为。

**资源预设（Prefabs）与 对象克隆 (clone)：**

- **预设的好处：** 预设允许你创建一个模板对象，该对象可以在多个场景中重复使用并保持一致。它还可以加速开发和节省内存。
- **预设与对象克隆的关系：** 预设是一个资源，它可以通过 `Instantiate()` 方法克隆成游戏对象。克隆的对象具有与预设相同的属性和组件。

**代码示例：实例化 table 预制资源：**

```csharp
using UnityEngine;

public class TableSpawner : MonoBehaviour
{
    public GameObject tablePrefab;  // 预制资源

    void Start()
    {
        // 实例化 table 预制体
        Instantiate(tablePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

```



## 编程实践：简单计算器

运行效果：

![image-20240927133438092](assets/image-20240927133438092.png)