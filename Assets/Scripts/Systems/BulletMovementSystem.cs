using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AirSeaBattle.Systems
{
    [UpdateAfter(typeof(MovementSystem))]
    public class BulletMovementSystem : ComponentSystem
    {
        private float2 _screenMaxLimits;

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            _screenMaxLimits = GameManager.Instance.MaxScreenLimit;
        }
        // This may raise eyebrows from a Unity developer who hasn't used DOTS.
        // But a Component System only runs under specific conditions and this conditions are
        // only met once in this case. Which is having an Entity with all Components from the Query executed.
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity,ref Translation translation, ref BulletComponent bullet, ref IsMoving tag) =>
            {
                float3 bulletLimits = translation.Value;
                bulletLimits.x -= bullet.Size * 0.5f;
                bulletLimits.y -= bullet.Size * 0.5f;
                if (bulletLimits.x >= _screenMaxLimits.x || bulletLimits.y >= _screenMaxLimits.y)
                {
                    PostUpdateCommands.AddComponent<WaitingForSetup>(entity);
                    PostUpdateCommands.RemoveComponent<IsMoving>(entity);
                }
            });
        }
    }
}