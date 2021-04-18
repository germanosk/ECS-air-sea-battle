using AirSeaBattle.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using TriggerEvent = Unity.Physics.TriggerEvent;

namespace AirSeaBattle.Systems
{
    public class BulletCollisionSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorld;
        private StepPhysicsWorld _stepPhysicsWorld;

        protected override void OnCreate()
        {
            base.OnCreate();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            CollisionJob job = new CollisionJob()
            {
                bulletsGroup = GetComponentDataFromEntity<BulletComponent>(),
                airplanesGroup = GetComponentDataFromEntity<AirplaneComponent>()
            };
            return job.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld,  inputDeps);
        }
        
        [BurstCompile]
        private struct CollisionJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<BulletComponent> bulletsGroup;
            public ComponentDataFromEntity<AirplaneComponent> airplanesGroup;
            public void Execute(Unity.Physics.TriggerEvent triggerEvent)
            {
                if (bulletsGroup.HasComponent(triggerEvent.EntityA) &&
                    airplanesGroup.HasComponent(triggerEvent.EntityB))
                {
                    BulletComponent bullet = bulletsGroup[triggerEvent.EntityA];
                    bullet.Reset = true;
                    bulletsGroup[triggerEvent.EntityA] = bullet;
                    AirplaneComponent airplane = airplanesGroup[triggerEvent.EntityB];
                    airplane.IsDead = true;
                    airplanesGroup[triggerEvent.EntityB] = airplane;
                }
                
                if (bulletsGroup.HasComponent(triggerEvent.EntityB) &&
                    airplanesGroup.HasComponent(triggerEvent.EntityA))
                {
                    BulletComponent bullet = bulletsGroup[triggerEvent.EntityB];
                    bullet.Reset = true;
                    bulletsGroup[triggerEvent.EntityB] = bullet;
                    AirplaneComponent airplane = airplanesGroup[triggerEvent.EntityA];
                    airplane.IsDead = true;
                    airplanesGroup[triggerEvent.EntityA] = airplane;

                }
            }
        }
    }
}