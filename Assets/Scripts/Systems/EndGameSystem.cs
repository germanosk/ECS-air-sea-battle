using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AirSeaBattle.Systems
{
    public class EndGameSystem: ComponentSystem
    {
        // This may raise eyebrows from a Unity developer who hasn't used DOTS.
        // But a Component System only runs under specific conditions and this conditions are
        // only met once in this case. Which is having an Entity with all Components from the Query executed.
        protected override void OnUpdate()
        {
            if (GameManager.Instance.IsRunning)
            {
                return;
            }

            Entities.ForEach((Entity entity,  ref IsMoving tag) =>
            {
                PostUpdateCommands.AddComponent<WaitingForSetup>(entity);
                PostUpdateCommands.RemoveComponent<IsMoving>(entity);
            });
        }
        
    }
}