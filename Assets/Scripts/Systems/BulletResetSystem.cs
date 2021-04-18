using AirSeaBattle.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AirSeaBattle.Systems
{
    public class BulletResetSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entity gunEntity = GetSingletonEntity<GunComponent>();
            float3 gunPosition = EntityManager.GetComponentData<Translation>(gunEntity).Value;
            GunComponent gunComponent = EntityManager.GetComponentData<GunComponent>(gunEntity);
            // Adjust gun position to the right position.
            gunPosition.x += gunComponent.BulletOrigin.x;
            gunPosition.y += gunComponent.BulletOrigin.y;
                
            Entities.ForEach((Entity entity, ref Translation translation, ref IsMoving isMovingTag, ref BulletComponent bullet) =>
            {
                if (bullet.Reset)
                {
                    translation.Value = gunPosition;
                    bullet.Reset = false;
                    PostUpdateCommands.RemoveComponent<IsMoving>(entity);
                }
            });
        }
    }
}