using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Action<int> Callback;

    public void OnAnimationEvent(int state)
    {
        Callback?.Invoke(state);
    }

    public void SetSmokeWalkLeft()
    {
        bool isActive = true;
        //MainCharacterController.Instance.SetSmokeWalk(isActive);
    }    
    
    public void SetSmokeWalkRight()
    {
        bool isActive = false;
        //MainCharacterController.Instance.SetSmokeWalk(isActive);
    }    

}
