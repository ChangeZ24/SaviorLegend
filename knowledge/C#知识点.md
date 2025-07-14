# C# 知识点总结

## 知识点梳理

### 1. 表达式体属性 (Expression-bodied Property)

#### 语法
```csharp
private float walkSpeed => speed / 2.5f;
```

#### 功能
- 简化只读属性的写法
- 等同于传统的 `{ get { return speed / 2.5f; } }` 写法

#### 用法
- 当属性只需要一个简单的返回值时使用
- 每次访问都会实时计算并返回结果
- 适合计算属性，不适合需要保存值的场景

#### 示例
```csharp
// 表达式体属性 - 实时计算
private float walkSpeed => speed / 2.5f;

// 传统写法
private float walkSpeed 
{ 
    get { return speed / 2.5f; } 
}
```

---

### 2. 事件订阅 (Event Subscription)

#### 语法
```csharp
inputControl.Gameplay.Jump.started += Jump;
```

#### 功能
- C# 中的委托和事件机制
- `+=` 是事件订阅操作符
- 实现观察者模式

#### 用法
- 将方法注册到事件上
- 当事件触发时，自动调用注册的方法
- 用于处理用户输入、系统事件等

#### 示例
```csharp
// 事件订阅
inputControl.Gameplay.Jump.started += Jump;

// 事件处理方法
public void Jump(InputAction.CallbackContext context) {
    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
}
```

#### 相关概念
- **委托 (Delegate)**：方法的类型，可以存储对方法的引用
- **观察者模式 (Observer Pattern)**：一种设计模式，定义了对象间的一对多依赖关系
- **事件 (Event)**：基于委托的机制，提供发布-订阅功能

---

### 3. Lambda 表达式 (Lambda Expression)

#### 语法
```csharp
ctx => {
    if (physicsCheck.isGround) speed = walkSpeed;
}
```

#### 功能
- C# 中的匿名函数写法
- 创建内联方法，无需单独定义方法名
- 直接在事件订阅时定义处理逻辑

#### 用法
- 当事件处理逻辑简单且只使用一次时使用
- `ctx` 是参数名（这里是 `InputAction.CallbackContext`）
- `=>` 后面是函数体

#### 示例
```csharp
// Lambda 表达式
inputControl.Gameplay.WalkButton.performed += ctx => {
    if (physicsCheck.isGround) speed = walkSpeed;
};

// 等价于传统方法
private void OnWalkButtonPerformed(InputAction.CallbackContext ctx) {
    if (physicsCheck.isGround) speed = walkSpeed;
}
inputControl.Gameplay.WalkButton.performed += OnWalkButtonPerformed;
```

#### 相关概念
- **匿名方法 (Anonymous Method)**：Lambda 表达式的前身，使用 `delegate` 关键字
- **函数式编程 (Functional Programming)**：Lambda 表达式支持函数式编程范式
- **闭包 (Closure)**：Lambda 表达式可以捕获外部变量

---

### 4. 变量赋值与值传递

#### 语法
```csharp
runSpeed = speed;
```

#### 功能
- 将 `speed` 的值复制给 `runSpeed`
- 创建独立副本，两者独立

#### 用法
- 保存变量的初始值
- 避免后续修改影响保存的值
- 适用于值类型（如 `float`、`int` 等）

#### 示例
```csharp
// 保存初始值
private float runSpeed;
runSpeed = speed;  // 保存 speed 的副本

// 后续 speed 改变不影响 runSpeed
speed = 5f;  // runSpeed 仍然是初始值
```

#### 相关概念
- **值类型 (Value Type)**：直接存储数据，如 `int`、`float`、`struct`
- **引用类型 (Reference Type)**：存储对数据的引用，如 `class`、`interface`
- **深拷贝 vs 浅拷贝**：深拷贝复制所有数据，浅拷贝只复制引用

---

### 5. 代码折叠指令 (#region)

#### 语法
```csharp
#region 强制走路
// 代码块
#endregion
```

#### 功能
- C# 预处理指令，用于在 IDE 中折叠代码块
- 将一段代码标记为一个可折叠的区域

#### 用法
- 在大型文件中组织代码结构
- 让代码层次更清晰，便于导航和维护
- 配合 `#endregion` 使用

#### 示例
```csharp
#region 强制走路
runSpeed = speed;
inputControl.Gameplay.WalkButton.performed += ctx => {
    if (physicsCheck.isGround) speed = walkSpeed;
};
#endregion
```

---

### 6. 访问修饰符 (Access Modifiers)

#### 语法
```csharp
public float speed;        // 公共，任何地方都可访问
private float runSpeed;    // 私有，只能在类内部访问
```

#### 功能
- 控制类成员的访问权限
- 实现封装和信息隐藏

#### 用法
- `public`：公共访问，任何地方都可访问
- `private`：私有访问，只能在类内部访问
- `protected`：受保护，只能在类内部和子类中访问
- `internal`：内部访问，只能在同一个程序集中访问

#### 示例
```csharp
public class PlayerController : MonoBehaviour
{
    public float speed;        // 可在 Inspector 中设置
    private float runSpeed;    // 只能在类内部使用
    private Rigidbody2D rb;   // 私有组件引用
}
```

---

### 7. 泛型 (Generics)

#### 语法
```csharp
GetComponent<Rigidbody2D>();
ReadValue<Vector2>();
```

#### 功能
- 提供类型安全的代码重用
- 避免类型转换，提高性能

#### 用法
- `GetComponent<T>()`：获取指定类型的组件
- `ReadValue<T>()`：读取指定类型的输入值
- 尖括号 `<>` 中指定具体类型

#### 示例
```csharp
// 获取组件
private Rigidbody2D rb = GetComponent<Rigidbody2D>();
private SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();

// 读取输入值
Vector2 moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
```

#### 相关概念
- **类型参数 (Type Parameter)**：`<T>` 中的 T 是类型参数
- **类型安全 (Type Safety)**：编译时检查类型，避免运行时错误
- **装箱/拆箱 (Boxing/Unboxing)**：泛型避免值类型和引用类型之间的转换

---

### 8. 命名空间 (Namespace)

#### 语法
```csharp
using UnityEngine;
using UnityEngine.InputSystem;
```

#### 功能
- 组织和管理代码结构
- 避免命名冲突
- 提供代码的层次结构

#### 用法
- `using` 语句引入命名空间
- 可以直接使用该命名空间中的类型
- 减少代码的冗长度

#### 示例
```csharp
using UnityEngine;           // Unity 核心功能
using UnityEngine.InputSystem; // 输入系统
using System.Collections;    // 集合类

public class PlayerController : MonoBehaviour
{
    // 可以直接使用 Vector2, Rigidbody2D 等类型
}
```

#### 相关概念
- **程序集 (Assembly)**：编译后的代码单元，包含命名空间
- **全局命名空间 (Global Namespace)**：默认命名空间，不指定时使用
- **命名空间别名 (Namespace Alias)**：使用 `using` 创建别名

---

### 9. 属性 (Property)

#### 语法
```csharp
[Header("基础属性")]
public float speed;
```

#### 功能
- 提供字段的访问控制
- 支持 getter 和 setter
- 可以在 Inspector 中显示和编辑

#### 用法
- `[Header]` 特性：在 Inspector 中添加标题
- `public` 属性：可在 Inspector 中查看和修改
- `private` 属性：只能在代码中访问

#### 示例
```csharp
[Header("基础属性")]
public float speed = 5f;        // 可在 Inspector 中设置
public float jumpForce = 10f;   // 可在 Inspector 中设置

private float runSpeed;         // 私有，不在 Inspector 中显示
```

#### 相关概念
- **特性 (Attribute)**：`[Header]` 是特性，用于元数据
- **反射 (Reflection)**：运行时获取类型信息，Inspector 使用反射
- **序列化 (Serialization)**：将对象转换为可存储的格式

---

### 10. 方法重载 (Method Overloading)

#### 语法
```csharp
public void Move() { }
public void Jump(InputAction.CallbackContext context) { }
```

#### 功能
- 同一个方法名可以有多个不同的参数列表
- 根据参数类型和数量自动选择合适的方法

#### 用法
- 提供不同的方法实现
- 简化 API 设计
- 提高代码的可读性

#### 示例
```csharp
// 无参数方法
public void Move() {
    rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
}

// 带参数方法
public void Jump(InputAction.CallbackContext context) {
    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
}
```

#### 相关概念
- **方法签名 (Method Signature)**：方法名 + 参数类型列表，用于区分重载
- **编译时多态 (Compile-time Polymorphism)**：重载在编译时确定调用哪个方法
- **参数匹配 (Parameter Matching)**：编译器根据参数类型选择最匹配的方法

---

## 易混淆概念详解

### 1.值传递 vs 引用传递

#### 值传递
```csharp
// 值类型 - 值传递
int a = 10;
int b = a;  // b 获得 a 的副本
a = 20;     // b 仍然是 10

// 值类型参数传递
void ModifyValue(int value) {
    value = 100;  // 只修改副本，不影响原值
}
int x = 5;
ModifyValue(x);  // x 仍然是 5
```

#### 引用传递
```csharp
// 引用类型 - 引用传递
class Person {
    public string name;
}
Person p1 = new Person { name = "Alice" };
Person p2 = p1;  // p2 指向同一个对象
p1.name = "Bob"; // p2.name 也变成 "Bob"

// 引用类型参数传递
void ModifyObject(Person person) {
    person.name = "Charlie";  // 修改原对象
}
Person p = new Person { name = "David" };
ModifyObject(p);  // p.name 变成 "Charlie"
```

### 2.表达式体属性 vs 变量赋值

#### 表达式体属性（实时计算）
```csharp
private float speed = 10f;
private float walkSpeed => speed / 2f;  // 实时计算

speed = 20f;  // walkSpeed 现在返回 10f
speed = 6f;   // walkSpeed 现在返回 3f
```

#### 变量赋值（保存副本）
```csharp
private float speed = 10f;
private float runSpeed;
runSpeed = speed;  // 保存副本

speed = 20f;  // runSpeed 仍然是 10f
speed = 6f;   // runSpeed 仍然是 10f
```

---

## 设计模式

### 1.观察者模式 (Observer Pattern)

#### 概念
- 定义对象间的一对多依赖关系
- 当一个对象状态改变时，所有依赖者都会得到通知并自动更新

#### 功能
- 实现松耦合的设计
- 支持广播通信
- 便于添加和删除观察者

#### 用法
- 事件系统（如 Unity 的输入系统）
- UI 更新
- 游戏状态管理

#### 示例
```csharp
// 发布者（被观察者）
public class GameManager : MonoBehaviour
{
    public static event System.Action OnGameStart;
    public static event System.Action OnGameOver;
    
    public void StartGame()
    {
        OnGameStart?.Invoke();  // 通知所有观察者
    }
    
    public void GameOver()
    {
        OnGameOver?.Invoke();   // 通知所有观察者
    }
}

// 观察者
public class UI : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStarted;
        GameManager.OnGameOver += OnGameEnded;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStarted;
        GameManager.OnGameOver -= OnGameEnded;
    }
    
    private void OnGameStarted()
    {
        Debug.Log("游戏开始！");
    }
    
    private void OnGameEnded()
    {
        Debug.Log("游戏结束！");
    }
}
```

### 2.发布-订阅模式 (Publish-Subscribe Pattern)

#### 概念
- 发布者不直接与订阅者通信
- 通过事件通道进行通信
- 发布者和订阅者完全解耦

#### 功能
- 实现完全解耦的通信
- 支持一对多、多对多通信
- 便于扩展和维护

#### 用法
- 消息系统
- 插件架构
- 模块间通信

#### 示例
```csharp
// 事件管理器
public static class EventManager
{
    public static event System.Action<Vector2> OnPlayerMove;
    public static event System.Action OnPlayerJump;
    
    public static void PublishPlayerMove(Vector2 direction)
    {
        OnPlayerMove?.Invoke(direction);
    }
    
    public static void PublishPlayerJump()
    {
        OnPlayerJump?.Invoke();
    }
}

// 发布者
public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        Vector2 moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        if (moveDirection != Vector2.zero)
        {
            EventManager.PublishPlayerMove(moveDirection);
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        EventManager.PublishPlayerJump();
    }
}

// 订阅者
public class AudioManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnPlayerMove += PlayFootstepSound;
        EventManager.OnPlayerJump += PlayJumpSound;
    }
    
    private void OnDisable()
    {
        EventManager.OnPlayerMove -= PlayFootstepSound;
        EventManager.OnPlayerJump -= PlayJumpSound;
    }
    
    private void PlayFootstepSound(Vector2 direction)
    {
        // 播放脚步声
    }
    
    private void PlayJumpSound()
    {
        // 播放跳跃声
    }
}
``` 