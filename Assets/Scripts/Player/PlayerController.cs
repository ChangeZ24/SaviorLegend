using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private PhysicsCheck physicsCheck;

    [Header("基础属性")]
    public float speed;

    // * 【C#】表达式体属性（Expression-bodied property）：
    // C# 6.0引入的语法糖，=> 是lambda表达式操作符
    // 功能：简化只读属性的写法，等同于 { get { return speed / 2f; } }
    // 用法：当属性只需要一个简单的返回值时，可以用 => 替代传统的get访问器写法，代码更简洁
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;

    // * 【Unity】Awake：生命周期最早的方法之一，脚本实例被加载时调用
    // 适合做变量初始化、获取组件等操作
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        
        // * 【C#】事件订阅：C#中的委托和事件机制，+= 是事件订阅操作符
        // 当玩家按下跳跃键时，会自动调用Jump方法，这是观察者模式的一种实现
        inputControl.Gameplay.Jump.started += Jump;
        
        // * 【C#】#region 代码折叠指令：C#预处理指令，用于在IDE中折叠代码块
        // 功能：将一段代码标记为一个可折叠的区域，配合#endregion使用
        // 用法：在大型文件中组织代码结构，让代码层次更清晰，便于导航和维护
        #region 强制走路
        
        // * 【C#】变量赋值与引用类型vs值类型：将speed的值复制给runSpeed，而不是引用
        // 功能：保存speed的初始值，避免后续speed被修改时影响runSpeed
        
        // ? 为什么不用 private float runSpeed => speed; 的表达式体属性写法：
        // 1. 表达式体属性是实时计算，每次访问都会返回speed的当前值
        // 2. 当speed被修改为walkSpeed时，runSpeed也会跟着变成walkSpeed的值
        // 3. 我们需要保存speed的初始值，所以用变量赋值来创建独立副本
        runSpeed = speed;
        
        // * 【C#】Lambda表达式（匿名函数）：C#中的内联函数写法，ctx => { ... } 是lambda表达式的语法
        // 功能：创建匿名方法，无需单独定义方法名，直接在事件订阅时定义处理逻辑
        // 用法：当事件处理逻辑简单且只使用一次时，用lambda表达式比单独定义方法更简洁
        // ctx 是参数名（这里是InputAction.CallbackContext），=> 后面是函数体
        inputControl.Gameplay.WalkButton.performed += ctx => {
            if (physicsCheck.isGround) speed = walkSpeed;
        };

        inputControl.Gameplay.WalkButton.canceled += ctx => {
            if (physicsCheck.isGround) speed = runSpeed;
        };
        #endregion
    }

    // * 【Unity】OnEnable：当对象被激活（SetActive为true）或场景加载时调用
    // 适合做输入、事件等的注册或启用
    private void OnEnable()
    {
        inputControl.Enable();
    }

    // * 【Unity】OnDisable：当对象被禁用（SetActive为false）或场景卸载时调用
    // 适合做输入、事件等的注销或禁用，防止内存泄漏
    private void OnDisable()
    {
        inputControl.Disable();
    }

    // * 【Unity】Update：每一帧都会执行一次
    // 适合处理玩家输入、动画、非物理相关的逻辑
    // 注意：帧率不稳定时，Update执行频率也会变化
    private void Update() {
        moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    // * 【Unity】FixedUpdate：每固定时间间隔执行一次（和物理引擎同步）
    // 适合处理物理相关的逻辑，比如刚体移动
    // 即使帧率不稳定，FixedUpdate间隔是固定的
    private void FixedUpdate() {
        Move();
    }

    // Move：自定义方法，用于处理玩家的移动和人物朝向
    // 被FixedUpdate调用，实现具体的移动逻辑
    public void Move() {
        rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
        // 人物翻转写法1
        // int faceDir = moveDirection.x > 0 ? 1 : -1;
        // transform.localScale = new Vector3(faceDir, 1, 1);
        // 人物翻转写法2
        playerRenderer.flipX = moveDirection.x < 0;
    }

    // * 【Unity】跳跃方法：检测地面状态，只有在地面上才能跳跃
    public void Jump(InputAction.CallbackContext context) {
        // * 【C#】条件判断：使用逻辑运算符&&进行多条件判断
        if (physicsCheck.isGround) {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}