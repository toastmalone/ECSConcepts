using System.Globalization;
using System.Diagnostics;
using System;
using Unity.Entities;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;

public class PickupSystem : JobComponentSystem
{
    private BeginInitializationEntityCommandBufferSystem bufferSystem;
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        bufferSystem = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
        buildPhysicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetExistingSystem<StepPhysicsWorld>();
        base.OnCreate();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        TriggerJob triggerJob = new TriggerJob
        {
            speedEntities = GetComponentDataFromEntity<SpeedData>(),
            entitiesToDelete = GetComponentDataFromEntity<DeleteTag>(),
            RotationSpeed = GetComponentDataFromEntity<RotationSpeedData>(),
            commandBuffer = bufferSystem.CreateCommandBuffer()
            
        };
        triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps).Complete();
        return default;

    }

    private struct TriggerJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<SpeedData> speedEntities;
        [ReadOnly] public ComponentDataFromEntity<DeleteTag> entitiesToDelete;
        public ComponentDataFromEntity<RotationSpeedData> RotationSpeed;
        public EntityCommandBuffer commandBuffer;
        public void Execute(TriggerEvent triggerEvent)
        {
            TestEntityTrigger(triggerEvent.EntityA, triggerEvent.EntityB);
        }

        private bool TestEntityTrigger(Entity entity1, Entity entity2)
        {
            if (entitiesToDelete.HasComponent(entity2) || entitiesToDelete.HasComponent(entity1)) { return false; }
            
            if(RotationSpeed.HasComponent(entity2) && speedEntities.HasComponent(entity1))
            {
                commandBuffer.AddComponent(entity2, new DeleteTag());
                return true;
            }
            return false;
        }
    }
}
