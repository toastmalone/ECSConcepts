using System.Diagnostics;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;
using System;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public Boolean won = false;
    public int CubesPerFrame;
    public int WinCondition;
    public float CubeSpeed;
    public static GameManager Instance;
    public Text ScoreText;
    public GameObject BallPrefab;
    public GameObject CubeWinPrefab;
    private Entity m_CubeWinEntity;
    private Entity m_ballEntityPrefab;
    private int m_CurrentScore;
    private EntityManager m_entityManager;
    private BlobAssetStore m_blobAssetStore;


    private EntityCommandBuffer m_CommandBuffer;
    private Translation cubeTranslation = new Translation(){
            Value = new float3(0,0,0)
        };

    private void Awake() {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        EntityCommandBufferSystem CommandBufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        m_CommandBuffer = CommandBufferSystem.CreateCommandBuffer();
        m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        m_blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, m_blobAssetStore);
        m_ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(BallPrefab, settings);

        m_CubeWinEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(CubeWinPrefab,settings);
    }

    private void Update() {
        
    }

    public void IncreaseScore()
    {
        m_CurrentScore += 1;
        DisplayScore();
        if(m_CurrentScore>=WinCondition)
        {
            won = true;
            ScoreText.text = "YOU WON!!!";
        }
    }

    private void SpawnCube()
    {
        
    }

    private void OnDestroy() {
        m_blobAssetStore.Dispose();
    }

    private void Start() {
        m_CurrentScore = 0;
        DisplayScore();
        SpawnBall();
    }

    private void SpawnBall()
    {
        Entity newBallEntity = m_entityManager.Instantiate(m_ballEntityPrefab);

        Translation ballTranslation = new Translation{
            Value = new float3(0f,0.5f,0f)
        };

        m_entityManager.AddComponentData(newBallEntity, ballTranslation);
        CameraFollow.Instance.BallEntity = newBallEntity;
    }

    private void DisplayScore()
    {
        ScoreText.text = "Score: " + m_CurrentScore;
    }


}
