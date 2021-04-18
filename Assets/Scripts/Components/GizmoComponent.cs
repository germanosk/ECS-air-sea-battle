using Unity.Entities;
using UnityEngine;

namespace AirSeaBattle.Components
{
    public struct GizmoComponent : IComponentData
    {
        public float Width;
        public float Height;
        public Color Color;
    }
}