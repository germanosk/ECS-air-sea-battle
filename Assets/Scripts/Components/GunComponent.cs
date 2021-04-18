using Unity.Entities;
using Unity.Mathematics;

namespace AirSeaBattle.Components
{
    public struct GunComponent : IComponentData
    {
        public float Height;
        public float Width;
        public float Cooldown;
        public float2 BulletOrigin;
    }
    
    // This component is used as tag to filter entities.
    public struct WaitingForSetup : IComponentData
    {
    
    }
}