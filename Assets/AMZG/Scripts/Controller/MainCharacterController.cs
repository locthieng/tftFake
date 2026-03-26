using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AnimState
{
    isIdle,
    isRunning,
    isShoveling,
    isMining,
    isFrozen,
    isIdle1,
    isIdle2,
    isWalking,
    isRunningCold,
    isIdleCold,
    isAction
}

public class MainCharacterController : Character
{
    public static MainCharacterController Instance { get; set; }
    [SerializeField] private LayerMask snowLayer;
    [SerializeField] private LayerMask miningLayer;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField] private bool isShoveling;
    [SerializeField] private bool isMining;

    RaycastHit raycastHitSnow;
    [SerializeField] private float checkDistance = 1;
    [SerializeField] private float miningToolRange = 1;
    [SerializeField] private SphereCollider toolSphereChecker;
    [SerializeField] private float minMoveThreshold = 0.01f;
    [SerializeField] private Transform vision;
    [SerializeField] private float snowShovelRadius = 0.3f;
    [SerializeField] private GameObject[] toolObjects;
    [SerializeField] private Transform resourcePoint;
    [SerializeField] private GameObject[] pickaxeSkins;
    //[SerializeField] private GroundGuide guide;
    public int CurrentToolLevel = 1;
    [SerializeField]
    private Material runtimeMaterial;

    private void Awake()
    {
        Instance = this;
        //runtimeMaterial.renderQueue = 4100;
    }

    public void SetUp()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        controller.enabled = true;
        toolSphereChecker.radius = miningToolRange;// MiningToolData.UpgradeData[Data.PickaxeLevel].Range;
        for (int i = 0; i < toolObjects.Length; i++)
        {
            toolObjects[i].SetActive(false);
        }
        animEvent.Callback = OnAnimEvent;
    }

    public void HandleControl(Vector2 movement)
    {
        if (CurrentState == CharacterState.Idle || CurrentState == CharacterState.Move)
        {
            moveDirection = new Vector3(movement.normalized.x, 0, movement.normalized.y);

            if (moveDirection.magnitude > minMoveThreshold)
            {
                if (CurrentState != CharacterState.Move)
                {
                    StartMoving();
                }
            }
        }
    }

    internal void OnRelease()
    {
        CurrentVelocity = Vector3.zero;
        CurrentState = CharacterState.Idle;
        moveDirection = Vector3.zero;
        MoveSpeedScale = 1;
        isShoveling = false;
        SetAnimState(AnimState.isIdle);
    }

    protected override void SetAnimState(AnimState state)
    {
        if (state == AnimState.isIdleCold)
        {
            state = AnimState.isIdle;
        }
        if (state == AnimState.isRunningCold)
        {
            state = AnimState.isRunning;
        }
        base.SetAnimState(state);
    }

    private int currentToolID = -1;

    public void OnAnimEvent(int value)
    {
        switch (value)
        {
            case 0: // mining hit
                SetAnimState(AnimState.isIdle);
                break;
            case 1: // mining end
                if (CurrentState == CharacterState.Idle && isMining)
                {
                    isMining = false;
                }
                break;
            default:
                break;
        }
    }

    // Thêm vào trong MainCharacterController.cs
    public void MoveToPositionFromClick(Vector3 targetPos)
    {
        // Sử dụng hàm MoveToPos đã có sẵn ở class Character
        // Callback là null nếu sếp không cần làm gì khi đến nơi
        agent.enabled = true;
        base.MoveToPos(targetPos, () =>
        {
            if (CurrentState == CharacterState.Move)
            {
                CurrentState = CharacterState.Idle;
                SetAnimState(AnimState.isIdle);
            }
            else
            {
                agent.enabled = false;
                CurrentState = CharacterState.Idle;
                SetAnimState(AnimState.isIdle);
            }    
        });
        // Đảm bảo cập nhật đúng Animation chạy
        SetAnimState(AnimState.isRunning);
    }

    public void StatusMoveToMapEnemy()
    {
        agent.enabled = false;
        CurrentState = CharacterState.Idle;
        SetAnimState(AnimState.isIdle);
    }

    public override void StartMoving()
    {
        CurrentState = CharacterState.Move;
        SetAnimState(isShoveling ? AnimState.isShoveling : AnimState.isRunning);
        // Stop mining if the player starts moving
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
