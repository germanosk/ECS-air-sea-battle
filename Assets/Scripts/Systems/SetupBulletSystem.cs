using AirSeaBattle.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    [UpdateAfter(typeof(SetupGunSystem))]
    public class SetupBulletSystem: ComponentSystem
    {
        // This may raise eyebrows from a Unity developer who hasn't used DOTS.
        // But a Component System only runs under specific conditions and this conditions are
        // only met once in this case. Which is having an Entity with all Components from the Query executed.
        protected override void OnUpdate()
        {
            Entity gunEntity = GetSingletonEntity<GunComponent>();
            float3 gunPosition = EntityManager.GetComponentData<Translation>(gunEntity).Value;
            GunComponent gunComponent = EntityManager.GetComponentData<GunComponent>(gunEntity);
            // Adjust gun position to the right position.
            gunPosition.x += gunComponent.BulletOrigin.x;
            gunPosition.y += gunComponent.BulletOrigin.y;
            
            Entities.ForEach((Entity entity, ref Translation translation, ref BulletComponent bullet, ref WaitingForSetup tag) =>
            {
                translation.Value = gunPosition;
                PostUpdateCommands.RemoveComponent<WaitingForSetup>(entity);
            });
        }
    }
}