using AirSeaBattle.Components;
using Unity.Entities;

namespace AirSeaBattle.Systems
{
    public class SpawnerSystem : ComponentSystem
    {
        // This may raise eyebrows from a Unity developer who hasn't used DOTS.
        // But a Component System only runs under specific conditions and this conditions are
        // only met once in this case. Which is having an Entity with all Components from the Query executed.
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, ref PrefabReferencesComponent referencesComponent, ref WaitingForSpawn waitingTag) =>
            {
                for (int i = 0; i < referencesComponent.AirplaneInstances; i++)
                {
                    EntityManager.Instantiate(referencesComponent.AirplanePrefabEntity);
                }
                
                for (int i = 0; i < referencesComponent.BulletInstances; i++)
                {
                    EntityManager.Instantiate(referencesComponent.BulletPrefabEntity);
                }
                
                // Removing the WaitingForSpawn to avoid this run again to this entity.
                PostUpdateCommands.RemoveComponent<WaitingForSpawn>(entity);
            });
        }
    }
}