using Unity.Entities;

namespace AirSeaBattle.Components
{
    public struct AirplaneComponent : IComponentData
    {
        public float Width;
        public float Height;
        public float Speed;

        public bool IsDead;
    }
}