using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : Attack
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float attackBuffer = 0.1f;
    [SerializeField] private float attackRadius = 1.0f;
    [SerializeField] private float hitboxOffsetX = 2.0f;
    [SerializeField] private float hitboxOffsetY = 0.0f;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Color editorHitColor = Color.green;
    [SerializeField] private Color editorDefaultColor = Color.red;

    private PlayerState playerState;
    private float bufferTimer = 0.0f;
    private bool hit = false;

    void Start()
    {
        attackInit = false;
        bufferTimer = 0.0f;
        playerState = GetComponent<PlayerState>();
        Gizmos.color = editorDefaultColor;
    }

    void Update()
    {
        if(attackInit)
        {
            Swing();
            bufferTimer = attackBuffer;
            attackInit = false;
        }
        else if(bufferTimer > 0.0f)
        {
            bufferTimer -= Time.deltaTime;
            if(bufferTimer <= 0.0f)
            {
                bufferTimer = 0.0f;
                playerState.SetAttacking(false);
                hit = false;
            }
        }
    }
    public override bool CanAttack()
    {
        return !playerState.IsDashing() && !playerState.IsGrounded();
    }

    public override void StartAttack()
    {
        playerState.SetAttacking(true);
        attackInit = true;
    }

    public override void Break()
    {
        attackInit = false;
        hit = false;
        bufferTimer = 0.0f;
        playerState.SetAttacking(false);
    }

    private bool IsFacingLeft()
    {
        return transform.rotation.y > 0.0f;
    }

    private void Swing()
    {
        Debug.Log("SWING!");
        Collider2D[] results;

        if (!IsFacingLeft())
        {
            results = Physics2D.OverlapCircleAll(new Vector2(transform.position.x
                + hitboxOffsetX, transform.position.y + hitboxOffsetY), attackRadius, attackLayer);
        }
        else
        {
            results = Physics2D.OverlapCircleAll(new Vector2(transform.position.x
                - hitboxOffsetX, transform.position.y + hitboxOffsetY), attackRadius, attackLayer);
        }

        if (results.Length > 0)
        {
            Debug.Log("HIT!");
            hit = true;

            foreach (Collider2D hit in results)
            {
                IHitable hitOptions = hit.GetComponentInParent<IHitable>();

                if (hitOptions != null)
                {
                    hitOptions.Hit(damage);
                }
            }
        }
        else
        {
            Debug.Log("MISS!");
            hit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        const float POINT_DIST = .005f;
        const float LINE_THICKNESS = .015f;

        if (hit)
        {
            Gizmos.color = editorHitColor;
        }
        else
        {
            Gizmos.color = editorDefaultColor;
        }

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        if (!IsFacingLeft())
        {
            pos.x += hitboxOffsetX;
            pos.y += hitboxOffsetY;
        }
        else
        {
            pos.x -= hitboxOffsetX;
            pos.y -= hitboxOffsetY;
        }

        float theta = 0.0f;
        Vector2 startPos = pos + new Vector2(attackRadius * Mathf.Cos(theta), attackRadius * Mathf.Sin(theta));
        Gizmos.DrawLine(startPos, startPos);

        for (theta = POINT_DIST; theta < (Mathf.PI * 2.0f); theta += POINT_DIST)
        {
            float xOffset = attackRadius * Mathf.Cos(theta);
            float yOffset = attackRadius * Mathf.Sin(theta);
            Vector2 newPos = pos + new Vector2(xOffset, yOffset);
            Gizmos.DrawLine(newPos - new Vector2(LINE_THICKNESS, LINE_THICKNESS), newPos);
        }
    }
}
