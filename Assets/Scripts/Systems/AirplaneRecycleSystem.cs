using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Entities;

namespace AirSeaBattle.Systems
{
    public class AirplaneRecycleSystem: ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, ref IsMoving isMovingTag, ref AirplaneComponent airplane) =>
            {
                if (airplane.IsDead)
                {
                    GameManager.Instance.PlaneDestroyed();
                    airplane.IsDead = false;
                    PostUpdateCommands.AddComponent<WaitingForSetup>(entity);
                    PostUpdateCommands.RemoveComponent<IsMoving>(entity);
                }
            });
        }
        
    }
}