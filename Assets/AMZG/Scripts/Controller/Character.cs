using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum CharacterState
{
    Idle,
    Move,
    FreeFall,
    Win,
    Lose,
    Attack,
    Stunned,
    Jump,
    Die,
}

public class Character : MonoBehaviour
{
    public CharacterState CurrentState;
    public float MoveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 20f;
    public Vector3 CurrentVelocity;
    [SerializeField] protected Vector3 moveDirection;
    public CharacterController controller;
    [SerializeField] protected Animator anim;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SkinnedMeshRenderer skinRenderer;
    [SerializeField] protected AnimationEvent animEvent;
    [SerializeField] private Transform moveTarget;
    private Action onMoveCallback;

    public bool IsInCampfireRange
    {
        /*get
        {
            if (MapController.Instance.Buildings[BuildingType.Base].CurrentLevel > 0 &&
                Vector3.Distance(MapController.Instance.Buildings[BuildingType.Base].transform.position, transform.position) <=
                MapController.Instance.Buildings[BuildingType.Base].CurrentLevelData.SnowMeltingRadius)
            {
                return true;
            }
            return false;
        }*/
        get { return false; }
    }

    protected float moveSpeedScale = 3f;
    public float MoveSpeedScale;
    protected bool isOnGround;

    protected virtual void OnAnimCallback(int state)
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            default:
                break;
        }
    }

    public virtual void StartMoving()
    {
        CurrentState = CharacterState.Move;
    }

    public void LookAt(Transform target)
    {
        moveDirection = target.position - transform.position;
        moveDirection = moveDirection.normalized;
    }

    public void LookAt(Vector3 targetPos)
    {
        moveDirection = targetPos - transform.position;
        moveDirection = moveDirection.normalized;
    }

    //public void MoveToPos(Vector3 targetPos, Action callback)
    //{
    //    CurrentState = CharacterState.Move;
    //    moveTarget = new GameObject(name + " MoveTarget").transform;
    //    moveTarget.position = targetPos;
    //    onMoveCallback = callback;
    //    SetAnimState(AnimState.isRunning);
    //}

    public void RevertMoveSpeedScale()
    {
        MoveSpeedScale = moveSpeedScale;
    }

    protected float speedUpRatio = 1;

    protected virtual void FixedUpdate()
    {
        if (controller != null && controller.enabled)
        {
            if (moveTarget != null)
            {
                if (Vector3.Distance(moveTarget.position, transform.position) <= Constants.EPSILON)
                {
                    moveTarget = null;
                    onMoveCallback?.Invoke();
                }
                else
                {
                    moveDirection = (moveTarget.position - transform.position).normalized;
                    moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                }
            }

            isOnGround = controller.isGrounded;
            if (isOnGround && CurrentVelocity.y < 0)
            {
                CurrentVelocity.y = 0f;
            }
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            if (CurrentState == CharacterState.Move)
            {
                CurrentVelocity = MoveSpeed * transform.forward * MoveSpeedScale * speedUpRatio;
            }
            controller.Move(CurrentVelocity * Time.deltaTime);
        }
        else if (agent != null && agent.enabled && CurrentState == CharacterState.Move &&
                        agent.remainingDistance <= agent.stoppingDistance && !isPosUpdated && !agent.pathPending)
        {
            isPosUpdated = true;
            onMoveCallback?.Invoke();
            onMoveCallback = null;
        }
    }

    public void Jump(float height)
    {
        CurrentVelocity.y += Mathf.Sqrt(height * -3.0f * Physics.gravity.y);
    }

    protected bool isPosUpdated;
    protected virtual void MoveToPos(Vector3 v, Action callback)
    {
        if (agent != null && agent.enabled)
        {
            agent.SetDestination(v);
            agent.isStopped = false;
            agent.speed = MoveSpeed * speedUpRatio * MoveSpeedScale;
            isPosUpdated = false;
            onMoveCallback = callback;
        }
        if (CurrentState != CharacterState.Move)
        {
            CurrentState = CharacterState.Move;
            SetAnimState(AnimState.isRunning);
        }
    }

    public virtual void Die()
    {
        if (controller != null)
        {
            controller.enabled = false;
        }
    }

    protected AnimState currentAnimState;
    protected virtual void SetAnimState(AnimState state)
    {
        if (state == currentAnimState) return;
        anim.SetBool(currentAnimState.ToString(), false);
        currentAnimState = state;
        anim.SetBool(currentAnimState.ToString(), true);
    }
}
