using System;
using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;
[UpdateInGroup(typeof(SimulationSystemGroup))]
public class WinSystem : SystemBase
{
    private EntityCommandBuffer m_CommandBuffer;
    private Entity m_CubeWinPrefab = Entity.Null;
    private bool win = false;
    EndSimulationEntityCommandBufferSystem endBufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

    private bool stop = false;
    protected override void OnCreate()
    {

    }
    protected override void OnUpdate()
    {
        if(GameManager.Instance.won && win == false)
        {
            win = true;
            return;
        }
        if(m_CubeWinPrefab == Entity.Null)
        {
            m_CubeWinPrefab = GetSingleton<WinCubePrefabData>().WinCube;
            return;
        }
        var cubeWinPrefab = m_CubeWinPrefab;
      
      
        
        int count = 0;
        double lastTimeCubeAdded =0;
        if(win && Time.ElapsedTime > (lastTimeCubeAdded + 0.3)){
            m_CommandBuffer = endBufferSystem.CreateCommandBuffer();
            for(int i=0;i<2;i++)
            {
                Entity newCube = m_CommandBuffer.Instantiate(m_CubeWinPrefab);

        var direction = Vector3.up;
        var speed = direction * 1;

        PhysicsVelocity velocity = new PhysicsVelocity(){
            Linear = speed,
            Angular = float3.zero,
            
        };

        Translation pos = new Translation(){
            Value= new float3(0f,1f,0f)
        };
        m_CommandBuffer.AddComponent(newCube,pos);
        m_CommandBuffer.AddComponent(newCube,velocity);
            }
            win =false;
            lastTimeCubeAdded = Time.ElapsedTime;
        }
      
    }

    
}
