using AirSeaBattle.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class GizmoSystem:ComponentSystem
    {
        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, ref Translation translation, ref GizmoComponent gizmoComponent) =>
            {
                float halfH = gizmoComponent.Height/2.0f;
                float halfW = gizmoComponent.Width/2.0f;
                Vector3 a = translation.Value + new float3(halfW, halfH, 0);                
                Vector3 b = translation.Value + new float3(halfW, -halfH, 0);
                Vector3 c = translation.Value + new float3(-halfW, halfH, 0);                
                Vector3 d = translation.Value - new float3(halfW, halfH, 0);

                Debug.DrawLine(a,b, gizmoComponent.Color);
                Debug.DrawLine(a,c, gizmoComponent.Color);
                Debug.DrawLine(b,d, gizmoComponent.Color);
                Debug.DrawLine(c,d, gizmoComponent.Color);
            });
        }
    }
}