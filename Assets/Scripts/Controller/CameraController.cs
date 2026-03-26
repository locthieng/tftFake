using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private TransformFollow transformFollower;
    public Camera GameCamera;
    public Camera UICamera;
    private float transformFollowStickiness;

    void Start()
    {
        if (transformFollower != null)
        {
            transformFollowStickiness = transformFollower.Stickiness;
        }
        else
        {
            transformFollowStickiness = 0.5f;
        }
    }

    internal void ResetFollowStickiness()
    {
        transformFollower.Stickiness = transformFollowStickiness;
    }

    public void FollowTarget(ArenaController target)
    {
        transformFollower.Target = target.transform;
        MythicalController.Instance.CheckMythicalAvatar(target);
    } 
}
