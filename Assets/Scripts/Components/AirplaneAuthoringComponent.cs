using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace AirSeaBattle.Components
{
    public class AirplaneAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float AirplaneWidth;
        public float AirplaneHeight;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new AirplaneComponent(){ Width = AirplaneWidth, Height = AirplaneHeight, Speed = GameManager.Instance.AiplaneSpeed});
            dstManager.AddComponentData(entity, new GizmoComponent(){ Width = AirplaneWidth, Height = AirplaneHeight, Color = Color.green});
            dstManager.AddComponentData(entity, new MovableComponent(){Direction = new float3(1, 0, 0), Speed = GameManager.Instance.AiplaneSpeed});
            dstManager.AddComponentData(entity, new WaitingForSetup());
        }
    }
}