# Unity 知识点总结

## 知识点梳理

### 1. MonoBehaviour 生命周期方法

#### 语法
```csharp
private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    playerRenderer = GetComponent<SpriteRenderer>();
}
```

#### 功能
- 生命周期最早的方法之一
- 脚本实例被加载时调用（场景刚加载、对象刚创建时）
- 适合做变量初始化、获取组件等操作

#### 用法
- 获取组件引用
- 初始化变量
- 设置初始状态

#### 示例
```csharp
private void Awake()
{
    // 获取组件
    rb = GetComponent<Rigidbody2D>();
    playerRenderer = GetComponent<SpriteRenderer>();
    physicsCheck = GetComponent<PhysicsCheck>();
    
    // 初始化输入系统
    inputControl = new PlayerInputControl();
}
```

---

### 2. OnEnable() 和 OnDisable()

#### 语法
```csharp
private void OnEnable()
{
    inputControl.Enable();
}

private void OnDisable()
{
    inputControl.Disable();
}
```

#### 功能
- `OnEnable()`：当对象被激活（SetActive为true）或场景加载时调用
- `OnDisable()`：当对象被禁用（SetActive为false）或场景卸载时调用

#### 用法
- `OnEnable()`：适合做输入、事件等的注册或启用
- `OnDisable()`：适合做输入、事件等的注销或禁用，防止内存泄漏

#### 示例
```csharp
private void OnEnable()
{
    inputControl.Enable();  // 启用输入系统
}

private void OnDisable()
{
    inputControl.Disable(); // 禁用输入系统，防止内存泄漏
}
```

---

### 3. Update() 和 FixedUpdate()

#### 语法
```csharp
private void Update() {
    moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
}

private void FixedUpdate() {
    Move();
}
```

#### 功能
- `Update()`：每一帧都会执行一次
- `FixedUpdate()`：每固定时间间隔执行一次（和物理引擎同步）

#### 用法
- `Update()`：适合处理玩家输入、动画、非物理相关的逻辑
- `FixedUpdate()`：适合处理物理相关的逻辑，比如刚体移动
- 注意：帧率不稳定时，Update执行频率也会变化，但FixedUpdate间隔是固定的

#### 示例
```csharp
private void Update() {
    // 处理输入 - 每帧执行
    moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
}

private void FixedUpdate() {
    // 处理物理移动 - 固定时间间隔执行
    Move();
}
```

---

### 4. GetComponent<T>()

#### 语法
```csharp
private Rigidbody2D rb = GetComponent<Rigidbody2D>();
private SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
```

#### 功能
- 获取当前GameObject上指定类型的组件
- 泛型方法，提供类型安全

#### 用法
- 在Awake()或Start()中获取组件引用
- 避免在Update()中重复调用，提高性能
- 如果组件不存在，返回null

#### 示例
```csharp
private void Awake()
{
    // 获取物理组件
    rb = GetComponent<Rigidbody2D>();
    
    // 获取渲染组件
    playerRenderer = GetComponent<SpriteRenderer>();
    
    // 获取自定义组件
    physicsCheck = GetComponent<PhysicsCheck>();
}
```

---

### 5. Transform 组件

#### 语法
```csharp
transform.up * jumpForce
transform.localScale = new Vector3(faceDir, 1, 1);
```

#### 功能
- 控制GameObject的位置、旋转和缩放
- 提供世界坐标和本地坐标的转换

#### 用法
- `transform.up`：获取向上的方向向量
- `transform.localScale`：设置本地缩放
- `transform.position`：设置世界位置

#### 示例
```csharp
// 使用transform.up作为跳跃方向
rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

// 通过缩放实现角色翻转
int faceDir = moveDirection.x > 0 ? 1 : -1;
transform.localScale = new Vector3(faceDir, 1, 1);
```

---

### 6. Rigidbody2D 物理组件

#### 语法
```csharp
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
```

#### 功能
- 2D刚体组件，处理物理模拟
- 提供速度、力、重力等物理属性

#### 用法
- `velocity`：设置或获取刚体的速度
- `AddForce()`：给刚体施加力
- `ForceMode2D.Impulse`：瞬间力，适合跳跃

#### 示例
```csharp
// 设置水平速度，保持垂直速度不变
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);

// 施加跳跃力
rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
```

---

### 7. SpriteRenderer 渲染组件

#### 语法
```csharp
playerRenderer.flipX = moveDirection.x < 0;
```

#### 功能
- 渲染2D精灵图像
- 控制精灵的显示属性

#### 用法
- `flipX`：水平翻转精灵
- `sprite`：设置精灵图像
- `color`：设置精灵颜色

#### 示例
```csharp
// 根据移动方向翻转角色
playerRenderer.flipX = moveDirection.x < 0;

// 设置精灵颜色
playerRenderer.color = Color.red;
```

---

### 8. Vector2 向量

#### 语法
```csharp
public Vector2 moveDirection;
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
```

#### 功能
- 表示2D向量，包含x和y分量
- 用于位置、速度、方向等

#### 用法
- 存储2D坐标或方向
- 进行向量运算
- 与物理系统配合使用

#### 示例
```csharp
// 存储移动方向
public Vector2 moveDirection;

// 设置刚体速度
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);

// 向量运算
Vector2 direction = (target.position - transform.position).normalized;
```

---

### 9. Time.deltaTime

#### 语法
```csharp
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
```

#### 功能
- 获取上一帧到当前帧的时间间隔（秒）
- 使移动速度与帧率无关

#### 用法
- 在Update()中用于平滑移动
- 确保不同帧率下移动速度一致
- 避免高速帧率下移动过快

#### 示例
```csharp
// 帧率无关的移动
rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);

// 平滑旋转
transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
```

---

### 10. Header 特性

#### 语法
```csharp
[Header("基础属性")]
public float speed;
```

#### 功能
- 在Inspector面板中添加标题
- 组织Inspector中的属性显示

#### 用法
- 将相关属性分组显示
- 提高Inspector的可读性
- 便于调试和参数调整

#### 示例
```csharp
[Header("基础属性")]
public float speed = 5f;
public float jumpForce = 10f;

[Header("组件引用")]
public Transform groundCheck;
public LayerMask groundLayer;
```

---

### 11. 输入系统 (Input System)

#### 语法
```csharp
moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
inputControl.Gameplay.Jump.started += Jump;
```

#### 功能
- Unity的新输入系统
- 提供跨平台输入支持
- 支持键盘、手柄、触摸等多种输入方式

#### 用法
- `ReadValue<T>()`：读取输入值
- `started`：输入开始事件
- `performed`：输入执行事件
- `canceled`：输入取消事件

#### 示例
```csharp
// 读取移动输入
moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

// 订阅跳跃事件
inputControl.Gameplay.Jump.started += Jump;

// 订阅走路按钮事件
inputControl.Gameplay.WalkButton.performed += ctx => {
    if (physicsCheck.isGround) speed = walkSpeed;
};
```

---

### 12. 组件引用

#### 语法
```csharp
private PhysicsCheck physicsCheck;
physicsCheck = GetComponent<PhysicsCheck>();
```

#### 功能
- 引用自定义组件
- 访问组件的属性和方法

#### 用法
- 在Awake()中获取组件引用
- 通过引用访问组件的公共成员
- 避免重复调用GetComponent()

#### 示例
```csharp
private PhysicsCheck physicsCheck;

private void Awake()
{
    physicsCheck = GetComponent<PhysicsCheck>();
}

private void Update()
{
    // 使用组件引用
    if (physicsCheck.isGround) {
        // 在地面上时的逻辑
    }
}
```

---

## 易混淆概念详解

### 1. Update() vs FixedUpdate()

#### Update() 特点
```csharp
private void Update() {
    // 每帧执行，频率不固定
    // 适合：输入处理、动画、非物理逻辑
    moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
}
```

#### FixedUpdate() 特点
```csharp
private void FixedUpdate() {
    // 固定时间间隔执行（默认0.02秒）
    // 适合：物理计算、刚体移动
    Move();
}
```

#### 关键区别
- **执行频率**：Update 每帧执行，FixedUpdate 固定间隔执行
- **适用场景**：Update 适合输入，FixedUpdate 适合物理
- **性能影响**：Update 受帧率影响，FixedUpdate 不受帧率影响

---

### 2. Transform 坐标系

#### 世界坐标 vs 本地坐标
```csharp
// 世界坐标 - 相对于世界原点
transform.position = new Vector3(10, 5, 0);

// 本地坐标 - 相对于父对象
transform.localPosition = new Vector3(2, 1, 0);
```

#### 关键区别
- **世界坐标**：相对于世界原点(0,0,0)的位置
- **本地坐标**：相对于父对象的位置
- **使用场景**：世界坐标用于绝对位置，本地坐标用于相对位置

---

## 高级概念

*当前项目暂未使用高级概念，后续根据项目需要添加* 