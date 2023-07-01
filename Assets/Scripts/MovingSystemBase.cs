using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        foreach(MovingPositionAspect movingPositionAspect in SystemAPI.Query<MovingPositionAspect>()){
            movingPositionAspect.Move(SystemAPI.Time.DeltaTime);
            movingPositionAspect.TestReachedTargetPosition(randomComponent);
        }
    }
}
