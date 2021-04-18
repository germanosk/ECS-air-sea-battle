using AirSeaBattle.Components;
using AirSeaBattle.Controllers;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AirSeaBattle.Systems
{
    [UpdateAfter(typeof(MovementSystem))]
    public class AirplaneMovementSystem : JobComponentSystem
    {
        private float _screenMaxXPos;
        private float _screenMinXPos;

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            _screenMaxXPos = GameManager.Instance.MaxScreenLimit.x;
            _screenMinXPos = GameManager.Instance.MinScreenLimit.x;
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            AirplaneMovementJob job = new AirplaneMovementJob()
            {
                ScreenMaxXPos = _screenMaxXPos,
                ScreenMinXPos = _screenMinXPos
            };
            return job.Schedule(this,inputDeps);
        }
        private struct AirplaneMovementJob : IJobForEachWithEntity<AirplaneComponent, Translation, IsMoving>
        {
            public float ScreenMaxXPos;
            public float ScreenMinXPos;
            
            public void Execute(Entity entity,
                int index,
                ref AirplaneComponent airplane,
                ref Translation translation,
                [ReadOnly] ref IsMoving aliveComponent)
            {
                float airplaneHalfWidth = (airplane.Width / 2.0f);
                float airplaneLimit = translation.Value.x - airplaneHalfWidth;
                if (airplaneLimit >= ScreenMaxXPos)
                {
                    translation.Value.x = ScreenMinXPos - airplaneHalfWidth;
                }
            }
        }
    }
}