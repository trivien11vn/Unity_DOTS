using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Burst;
[BurstCompile]
public partial struct MovingISystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state){
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state){

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state){
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        float deltaTime = SystemAPI.Time.DeltaTime;
        JobHandle jobHandel = new MoveJob{
            deltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);

        jobHandel.Complete();
        new TestReachedTargetPositionJob{
            randomComponent = randomComponent,
        }.Run();
    }
}
[BurstCompile]
public partial struct MoveJob: IJobEntity {
    public float deltaTime;
    public void Execute(MovingPositionAspect movingPositionAspect){
        movingPositionAspect.Move(deltaTime);
    }
}

[BurstCompile]
public partial struct TestReachedTargetPositionJob: IJobEntity {
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent ;

    public void Execute(MovingPositionAspect movingPositionAspect){
        movingPositionAspect.TestReachedTargetPosition(randomComponent);
    }
}