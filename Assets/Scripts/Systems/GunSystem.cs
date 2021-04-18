using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    [AlwaysUpdateSystem]
    public class GunSystem : ComponentSystem
    {
        private double _cooldownTimer;
        protected override void OnCreate()
        {
            base.OnCreate();
        }
        protected override void OnUpdate()
        {
            
            Entity gunEntity = GetSingletonEntity<GunComponent>();
            
            int pos = 1;

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                pos --;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                pos ++;
            }

            if (Input.GetKey(KeyCode.Space) && Time.ElapsedTime >= _cooldownTimer)
            {
                GunComponent gunComponent = EntityManager.GetComponentData<GunComponent>(gunEntity);
                _cooldownTimer = Time.ElapsedTime + gunComponent.Cooldown;
                
                EntityQueryDesc query = new EntityQueryDesc
                {
                    None = new ComponentType[] {ComponentType.ReadOnly<IsMoving>()},
                    All = new ComponentType[] {typeof(Translation),typeof(MovableComponent), ComponentType.ReadOnly<BulletComponent>()}
                };
                EntityQuery entityQuery = GetEntityQuery(query);
                if (!entityQuery.IsEmptyIgnoreFilter)
                {
                    NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Persistent);
                    Entity entity = entities[0];
                    float3 direction = Quaternion.AngleAxis(60 - 30 * pos, Vector3.back) * Vector3.up;
                    MovableComponent component = EntityManager.GetComponentData<MovableComponent>(entity);
                    component.Direction = direction;
                    PostUpdateCommands.AddComponent(entity, new IsMoving());
                    PostUpdateCommands.SetComponent(entity, component);
                    entities.Dispose();
                    GameManager.Instance.ShotFired();
                }
            }
        
            var materialReferencesEntity = GetSingletonEntity<MaterialReferences>();
            var materialReferences = EntityManager.GetSharedComponentData<MaterialReferences>(materialReferencesEntity);
        
            RenderMesh renderMesh = EntityManager.GetSharedComponentData<RenderMesh>(gunEntity);
            Material newMaterial = materialReferences.GunPositions[pos];
            if (newMaterial != renderMesh.material)
            {
                renderMesh.material = newMaterial;
                PostUpdateCommands.SetSharedComponent(gunEntity, renderMesh);
            }
            
        }
    
    }
}
