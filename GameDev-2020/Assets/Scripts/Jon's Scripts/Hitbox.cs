using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = Vector2.one;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private bool useCircle = false;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Color hitBoxClosedColor = Color.red;
    [SerializeField] private Color hitBoxOpenColor = Color.yellow;
    [SerializeField] private Color hitboxCollidingColor = Color.green;
    private ColliderState currentState = ColliderState.Closed;

    public enum ColliderState 
    {
        Open,
        Closed,
        Colliding
    }

    void Update()
    {
        
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

    private void OnDrawGizmosSelected()
    {
        CheckGizmoColor();
        int halfExtentMultiplier = 2;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        Gizmos.DrawCube(Vector3.zero, new Vector2(boxSize.x * halfExtentMultiplier, boxSize.y * halfExtentMultiplier));
    }
}
