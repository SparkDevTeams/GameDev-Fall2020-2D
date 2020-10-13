using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = Vector2.one;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private bool useCircle = false;
    [SerializeField] private bool showEditorHitbox = true;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Color hitBoxClosedColor = new Color(191, 0, 0, 90);
    [SerializeField] private Color hitBoxOpenColor = new Color(191, 177, 0, 90);
    [SerializeField] private Color hitboxCollidingColor = new Color(0, 191, 0, 90);
    private IHitboxResponder responder = null;
    private ColliderState currentState = ColliderState.Closed;

    public enum ColliderState 
    {
        Open,
        Closed,
        Colliding
    }

    public void HitboxUpdate()
    {
        if (currentState != ColliderState.Closed)
        {
            Collider2D[] colliders;

            if (!useCircle)
            {
                colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, transform.eulerAngles.z, hitLayer);
            }
            else 
            {
                colliders = Physics2D.OverlapCircleAll(transform.position, radius, hitLayer);
            }


            for (int i = 0; i < colliders.Length; i++)
            {
                if (responder != null)
                {
                    responder.CollideWith(colliders[i]);
                }
            }

            currentState = colliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
        }


    }

    public void HitboxUpdate(LayerMask customLayer)
    {
        if (currentState != ColliderState.Closed)
        {
            Collider2D[] colliders;

            if (!useCircle)
            {
                colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, transform.eulerAngles.z, customLayer);
            }
            else
            {
                colliders = Physics2D.OverlapCircleAll(transform.position, radius, customLayer);
            }


            for (int i = 0; i < colliders.Length; i++)
            {
                if (responder != null)
                {
                    responder.CollideWith(colliders[i]);
                }
            }

            currentState = colliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
        }
    }

    public void SetResponder(IHitboxResponder newResponder) 
    {
        responder = newResponder;
    }

    public void StartCheckingCollisions()
    {
        currentState = ColliderState.Open;
    }

    public void StopCheckingCollisions() 
    {
        currentState = ColliderState.Closed;
    }

    private void CheckGizmoColor()
    {
        switch (currentState)
        {

            case ColliderState.Open:
                Gizmos.color = hitBoxOpenColor;
                break;
            case ColliderState.Closed:
                Gizmos.color = hitBoxClosedColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = hitboxCollidingColor;
                break;
            default:
                break;
        }

    }

    private void OnDrawGizmos()
    {
        if (showEditorHitbox)
        {
            CheckGizmoColor();
            int halfExtentMultiplier = 2;

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

            if (!useCircle)
            {
                Gizmos.DrawCube(Vector3.zero, new Vector2(boxSize.x * halfExtentMultiplier, boxSize.y * halfExtentMultiplier));
            }
            else
            {
                Gizmos.DrawSphere(Vector3.zero, radius);
            }
        }
    }
}
