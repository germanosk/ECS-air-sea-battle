using Unity.Entities;

namespace AirSeaBattle.Components
{
    public struct PrefabReferencesComponent : IComponentData
    {
        public int AirplaneInstances;
        public int BulletInstances;
        public Entity AirplanePrefabEntity;
        public Entity BulletPrefabEntity;
    }

    // This component is used as tag to filter entities.
    public struct WaitingForSpawn : IComponentData
    {
    }
}