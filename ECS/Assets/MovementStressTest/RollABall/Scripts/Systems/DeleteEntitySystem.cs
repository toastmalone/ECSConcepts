using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using Unity.Entities;
using Unity.Collections;

[AlwaysSynchronizeSystem]
[UpdateAfter(typeof(PickupSystem))]
public class DeleteEntitySystem : JobComponentSystem
{

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

        Entities
            .WithAll<DeleteTag>()
            .WithoutBurst()
            .ForEach((Entity entity) =>
                {
                    GameManager.Instance.IncreaseScore();
                    entityCommandBuffer.DestroyEntity(entity);
                }).Run();

        entityCommandBuffer.Playback(EntityManager);
        entityCommandBuffer.Dispose();

        return default;
    }
}
