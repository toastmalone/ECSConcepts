using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Mathematics;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class MovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        float2 currentInput = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Entities.ForEach((ref PhysicsVelocity velocity, in SpeedData speedData) => {
            float2 newVelocity = velocity.Linear.xz;

            newVelocity += currentInput * speedData.speed * deltaTime;

            velocity.Linear.xz = newVelocity;
        }).Run();

        return default;
    }
}

