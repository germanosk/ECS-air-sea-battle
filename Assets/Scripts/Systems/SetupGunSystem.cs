using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    public class SetupGunSystem: ComponentSystem
    {
        private const float _screenWidthDivisor = 4.0f;
        private const float _gunHeightDivisor = 2.0f;
        
        // This may raise eyebrows from a Unity developer who hasn't used DOTS.
        // But a Component System only runs under specific conditions and this conditions are
        // only met once in this case. Which is having an Entity with all Components from the Query executed.
        protected override void OnUpdate()
        {
            Vector2 min = GameManager.Instance.MinScreenLimit;
            Vector2 max = GameManager.Instance.MaxScreenLimit;
            
            min.x += (max.x-min.x)  / _screenWidthDivisor;
            Vector3 point = min;
            
            
            Entities.ForEach((Entity entity, ref Translation translation, ref GunComponent gun, ref WaitingForSetup tag) =>
            {
                translation.Value = point;
                translation.Value.y += gun.Height / _gunHeightDivisor;
                PostUpdateCommands.SetComponent(entity, translation);
                PostUpdateCommands.RemoveComponent<WaitingForSetup>(entity);
            });
        }
    }
}