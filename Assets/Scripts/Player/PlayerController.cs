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
    public float speed;

    // Awake：生命周期最早的方法之一。
    // 脚本实例被加载时调用（比如场景刚加载、对象刚创建时），适合做变量初始化、获取组件等操作。
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // OnEnable：当对象被激活（SetActive为true）或场景加载时调用。
    // 适合做输入、事件等的注册或启用。
    private void OnEnable()
    {
        inputControl.Enable();
    }

    // OnDisable：当对象被禁用（SetActive为false）或场景卸载时调用。
    // 适合做输入、事件等的注销或禁用，防止内存泄漏。
    private void OnDisable()
    {
        inputControl.Disable();
    }

    // Update：每一帧都会执行一次。
    // 适合处理玩家输入、动画、非物理相关的逻辑。
    // 注意：帧率不稳定时，Update执行频率也会变化。
    private void Update() {
        moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    // FixedUpdate：每固定时间间隔执行一次（和物理引擎同步）。
    // 适合处理物理相关的逻辑，比如刚体移动。
    // 即使帧率不稳定，FixedUpdate间隔是固定的。
    private void FixedUpdate() {
        Move();
    }

    // Move：自定义方法，用于处理玩家的移动和人物朝向。被FixedUpdate调用，实现具体的移动逻辑。
    public void Move() {
        rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, rb.velocity.y);
        // 人物翻转写法1
        // int faceDir = moveDirection.x > 0 ? 1 : -1;
        // transform.localScale = new Vector3(faceDir, 1, 1);
        // 人物翻转写法2
        playerRenderer.flipX = moveDirection.x < 0;
    }
}