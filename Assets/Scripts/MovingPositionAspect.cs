using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public readonly partial struct MovingPositionAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<LocalTransform> tf;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetPosition> target;
    
    public void Move(float deltaTime){
        float3 direction = math.normalize(target.ValueRW.value - tf.ValueRW.Position);
        tf.ValueRW.Position += direction * speed.ValueRO.value * deltaTime;
    }
    public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent){
        float reachedTargetposition = .5f;
        if(math.distance(tf.ValueRW.Position, target.ValueRW.value)<reachedTargetposition){
            // Generate new random target position
            target.ValueRW.value = GetRandomPosition(randomComponent);
        }
    }
    public float3 GetRandomPosition(RefRW<RandomComponent> randomComponent){
        return new float3(
            randomComponent.ValueRW.random.NextFloat(0f,10f),
            0,
            randomComponent.ValueRW.random.NextFloat(0f,10f)
        );
    }
}
