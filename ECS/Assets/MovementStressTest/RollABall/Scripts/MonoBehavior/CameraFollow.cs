using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float3 Offset;
    public static CameraFollow Instance;
   public Entity BallEntity;

    private GameObject m_BallPrefab;


   private EntityManager m_entityManager;

   private BlobAssetStore m_blobAssetStore;

   

   private void Awake() {
       Instance = this;
       m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
   }

   private void LateUpdate()    
   {
       Translation ballTranslation = m_entityManager.GetComponentData<Translation>(BallEntity);
       gameObject.transform.position = ballTranslation.Value + Offset;
   }
   
}
