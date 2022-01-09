using System.Security.Cryptography;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using UnityEngine;
using Unity.Physics;


[UpdateAfter(typeof(DeleteEntitySystem))]
public class CubeSpawner : SystemBase
{
    private EntityManager m_EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    private BeginSimulationEntityCommandBufferSystem m_bufferSystem;
    private Entity m_CubePrefab;
    private EntityCommandBuffer m_CommandBuffer;
    private Entity m_CubeWinPrefab = Entity.Null;
    private int winCondition;
    private float m_CubeSpeed;
    protected override void OnCreate()
    {
        
        var cubeEntityPrefab = GetEntityQuery(ComponentType.ReadOnly<CubePrefabData>());
        m_bufferSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();

        m_CubeSpeed = GameManager.Instance.CubeSpeed;
        
        RequireForUpdate(cubeEntityPrefab);
    }

    protected override void OnUpdate()
    {
        m_CommandBuffer = m_bufferSystem.CreateCommandBuffer();
        if(m_CubeWinPrefab == Entity.Null)
        {
            m_CubeWinPrefab = GetSingleton<WinCubePrefabData>().WinCube;
            return;
        }

        if(m_CubePrefab == Entity.Null)
        {
            m_CubePrefab =  GetSingleton<CubePrefabData>().Prefab;
            
            return;
        }

        //Have to add because ECS
        var cubePrafab = m_CubePrefab;
        

        var cubeCount = GetEntityQuery(ComponentType.ReadOnly<RotationSpeedData>()).CalculateEntityCount();
        if(cubeCount < 5) {
            Translation randomPosition = new Translation{
          Value = new float3(randomInt(),0.5f,randomInt()),
      };

      Entity newCube = m_EntityManager.Instantiate(m_CubePrefab);
      m_EntityManager.AddComponentData(newCube,randomPosition);
        } 
    }

    private void SpawnWinCubes()
    {
        
        
    }

    private float randomInt()
    {
        return (UnityEngine.Random.Range(-5f,5f));
    }
}
