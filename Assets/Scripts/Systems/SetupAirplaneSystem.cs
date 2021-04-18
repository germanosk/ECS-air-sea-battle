using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    public class SetupAirplaneSystem: ComponentSystem
    {
        protected override void OnUpdate()
        {
            Vector2 min = GameManager.Instance.MinScreenLimit;
            
            Entities.ForEach((Entity entity, ref Translation translation, ref AirplaneComponent aiplane, ref WaitingForSetup tag) =>
            {
                translation.Value = new float3(min.x - aiplane.Width/2.0f, 0 , 0);
                PostUpdateCommands.SetComponent(entity, translation);
                PostUpdateCommands.RemoveComponent<WaitingForSetup>(entity);
            });
        }
    }
}