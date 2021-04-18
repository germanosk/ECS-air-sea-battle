using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AirSeaBattle.Systems
{
    [AlwaysUpdateSystem]
    public class SetupWavesSystem: ComponentSystem
    {
        private const float _verticalOffset = 1.5f;
        protected override void OnUpdate()
        {
            EntityQueryDesc query = new EntityQueryDesc
            {
                All = new ComponentType[] {ComponentType.ReadOnly<AirplaneComponent>(), ComponentType.ReadOnly<IsMoving>()}
            };
            EntityQuery movingAirplanesQuery = GetEntityQuery(query);
            if (GameManager.Instance.IsRunning && movingAirplanesQuery.IsEmptyIgnoreFilter)
            {
                int aiplanesPerWave = Random.Range(GameManager.Instance.MinAiplanesPerWave,
                    GameManager.Instance.MaxAiplanesPerWave+1);
                float2 position  = float2.zero;
                position.x = GameManager.Instance.MinScreenLimit.x;
                position.y = GameManager.Instance.MaxScreenLimit.y - _verticalOffset;
                
                EntityQueryDesc queryDesc = new EntityQueryDesc
                {
                    All = new ComponentType[] {ComponentType.ReadOnly<AirplaneComponent>(), ComponentType.ReadOnly<MovableComponent>()}
                };
                EntityQuery airplaneQuery = GetEntityQuery(queryDesc);
                
                NativeArray<Entity> entities = airplaneQuery.ToEntityArray(Allocator.Persistent);
                for (int i = 0; i < aiplanesPerWave; i++)
                {
                    Entity entity = entities[i];
                    AirplaneComponent aiplane = EntityManager.GetComponentData<AirplaneComponent>(entity);
                    position.y -= aiplane.Height * 4;
                    Translation translation = EntityManager.GetComponentData<Translation>(entity);
                    translation.Value = new float3(position.x - aiplane.Width/2.0f, position.y, 0);
                    PostUpdateCommands.SetComponent(entity, translation);
                    PostUpdateCommands.AddComponent<IsMoving>(entity);
                }
                entities.Dispose();
            }
        }
    }
}