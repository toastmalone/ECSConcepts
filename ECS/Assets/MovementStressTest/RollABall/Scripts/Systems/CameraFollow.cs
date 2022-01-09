using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class CameraFollowSystem : JobComponentSystem
{
    Entity camera;

    protected override void OnCreate()
    {
        camera = GetSingletonEntity<DeleteTag>();
        base.OnCreate();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return default;
    }
}

