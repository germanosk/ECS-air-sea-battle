using AirSeaBattle.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    public class MovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            MovementJob job = new MovementJob()
            {
                DeltaTime = Time.DeltaTime
            };
            return job.Schedule(this,inputDeps);
        }
        
        [BurstCompile]
        private struct MovementJob : IJobForEachWithEntity<MovableComponent, Translation, IsMoving>
        {
            public float DeltaTime;
            
            public void Execute(Entity entity,
                int index,
                ref MovableComponent movable,
                ref Translation translation,
                [ReadOnly] ref IsMoving aliveComponent)
            {
                translation.Value +=movable.Direction * movable.Speed * DeltaTime;
            }
        }
    }
}