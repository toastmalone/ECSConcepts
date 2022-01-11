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
    private Entity m_CubePrefab = Entity.Null;
    private float m_CubeSpeed;

    protected override void OnUpdate()
    {
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

    private float randomInt()
    {
        return (UnityEngine.Random.Range(-5f,5f));
    }
}
