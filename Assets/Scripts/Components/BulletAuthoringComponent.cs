using System.Drawing;
using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Color = UnityEngine.Color;

namespace AirSeaBattle.Components
{
    public class BulletAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float BulletSize;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new BulletComponent(){Size = BulletSize});
            dstManager.AddComponentData(entity, new WaitingForSetup());
            dstManager.AddComponentData(entity, new MovableComponent(){Direction = new float3(1, 0, 0), Speed = GameManager.Instance.BulletSpeed});
        }
        
        private void OnDrawGizmosSelected()
        {
            Color gizmoColor = Color.red;
            float halfSize =BulletSize/2.0f;
            Vector3 pos = transform.position;
            Vector3 a = pos + new Vector3(halfSize, halfSize, 0);                
            Vector3 b = pos + new Vector3(halfSize, -halfSize, 0);
            Vector3 c = pos + new Vector3(-halfSize, halfSize, 0);                
            Vector3 d = pos - new Vector3(halfSize, halfSize, 0);

            Debug.DrawLine(a,b, gizmoColor);
            Debug.DrawLine(a,c, gizmoColor);
            Debug.DrawLine(b,d, gizmoColor);
            Debug.DrawLine(c,d, gizmoColor);
        }
    }
}