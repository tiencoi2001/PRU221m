using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseEntity
{
    float BaseSpeed { get; set; }
    float SmoothTime { get; set; }
    public void Movement();
}
