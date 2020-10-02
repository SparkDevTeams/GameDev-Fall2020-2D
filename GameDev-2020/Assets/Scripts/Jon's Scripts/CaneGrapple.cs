using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneGrapple : Attack, IHitboxResponder
{
    [SerializeField] private float rayCastDist = 50.0f;
    [SerializeField] private float grappleSpeed = 20.0f;

    private bool checkingTop = false;
    private bool checkingSide = false;
    private bool checkingBottom = false;
    private bool locked = false;
    private Vector3 throwDirection;
    private Vector3 grapplePoint;
    private DimensionManager dimension;
    private PlayerState playerState;
    private DimensionLayer currentLayer;
    [SerializeField] private Hitbox sideCheck;
    [SerializeField] private Hitbox topCheck;
    [SerializeField] private Hitbox BottomCheck;
    [SerializeField] private LayerMask positiveWallLayer;
    [SerializeField] private LayerMask negativeWallLayer;
    [SerializeField] private LayerMask nullWallLayer;

    private enum DimensionLayer 
    {
        POSITIVE,
        NEGATIVE,
        NULL
    }


    void Start()
    {
        dimension = GetComponent<DimensionManager>();
        playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        
    }

    public override bool CanAttack() 
    {
        return !playerState.IsDashing() && !playerState.IsInteracting();
    }

    public override void StartAttack() 
    {
        playerState.SetAttacking(true);
        attackInit = true;
        checkingTop = false;
        checkingSide = false;
        checkingBottom = false;
        locked = false;
        
    }

    public override void Break()
    {
    }

    public void CollideWith(Collider2D collision) 
    { }

    private void UpdateCurrentLayer()
    {
        int id = dimension.GetDimensionID();

        switch (id)
        {
            case 1:
                currentLayer = DimensionLayer.POSITIVE;
                break;
            case 2:
                currentLayer = DimensionLayer.NEGATIVE;
                break;
            default:
                currentLayer = DimensionLayer.NULL;
                break;
        }
    }
}
