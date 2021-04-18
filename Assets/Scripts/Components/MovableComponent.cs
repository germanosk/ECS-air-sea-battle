using Unity.Entities;
using Unity.Mathematics;

namespace AirSeaBattle.Components
{
    public struct MovableComponent : IComponentData
    {
        public float3 Direction;
        public float Speed;
    }
}