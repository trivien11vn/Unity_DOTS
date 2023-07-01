using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public partial class PlayerSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery playerEntityQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag));
        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        EntityCommandBuffer entityCommandBuffer = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        int spawnAmount = 20;
        if(playerEntityQuery.CalculateEntityCount() < spawnAmount){
            Entity spawnedEntity = entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
            entityCommandBuffer.SetComponent(spawnedEntity,new Speed{
                value = randomComponent.ValueRW.random.NextFloat(1F,5F),
            });
        }
    }
}
