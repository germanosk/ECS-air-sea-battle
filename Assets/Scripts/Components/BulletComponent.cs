using Unity.Entities;

namespace AirSeaBattle.Components
{
    public struct BulletComponent : IComponentData
    {
        public float Size;
        public bool Reset;
    }
}