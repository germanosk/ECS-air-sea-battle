using System;
using Unity.Entities;
using UnityEngine;

namespace AirSeaBattle.Components
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class GizmoAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float W;
        public float H;
        public Color GizmoColor;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new GizmoComponent(){ Width = W, Height = H, Color = GizmoColor});
        }

        private void OnDrawGizmosSelected()
        {
            float halfH = H/2.0f;
            float halfW = W/2.0f;
            Vector3 pos = transform.position;
            Vector3 a = pos + new Vector3(halfW, halfH, 0);                
            Vector3 b = pos + new Vector3(halfW, -halfH, 0);
            Vector3 c = pos + new Vector3(-halfW, halfH, 0);                
            Vector3 d = pos - new Vector3(halfW, halfH, 0);

            Debug.DrawLine(a,b, GizmoColor);
            Debug.DrawLine(a,c, GizmoColor);
            Debug.DrawLine(b,d, GizmoColor);
            Debug.DrawLine(c,d, GizmoColor);
        }
    }
}