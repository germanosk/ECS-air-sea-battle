using Unity.Entities;
using UnityEngine;

namespace AirSeaBattle.Components
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class GunAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float GunHeigth;
        public float GunWidth;
        public float GunCooldown;
        [Tooltip("The point of origin to all bullets. The red cross gizmo inform its position.")]
        public Vector2 GunBulletOrigin;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity,
                new GunComponent()
                {
                    Height = GunHeigth,
                    Width = GunWidth,
                    BulletOrigin = GunBulletOrigin,
                    Cooldown = GunCooldown
                });
            dstManager.AddComponent<WaitingForSetup>(entity);
        }
        
        
        private void OnDrawGizmosSelected()
        {
            float gizmoSize = 0.1f;
            
            Vector2 bulletOffest = transform.position;
            bulletOffest += GunBulletOrigin;
            
            Vector3 a =  new Vector3(bulletOffest.x, bulletOffest.y + gizmoSize, 0);                
            Vector3 b =  new Vector3(bulletOffest.x, bulletOffest.y - gizmoSize, 0);
            Vector3 c =  new Vector3(bulletOffest.x-gizmoSize, bulletOffest.y, 0);                
            Vector3 d =  new Vector3(bulletOffest.x+gizmoSize, bulletOffest.y, 0);

            Debug.DrawLine(a,b, Color.red);
            Debug.DrawLine(c,d, Color.red);
        }
    }
}