using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    public float checkRadius;

    [Header("状态")]
    public bool isGround;

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
    }

    public void GroundCheck() {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
