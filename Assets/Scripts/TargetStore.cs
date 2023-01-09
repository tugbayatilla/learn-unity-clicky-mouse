using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetStore : MonoBehaviour
{
    public event Action<Target> TargetDestroyed;

    public void OnTargetDestroyed(Target target)
    {
        TargetDestroyed?.Invoke(target);
    }
}