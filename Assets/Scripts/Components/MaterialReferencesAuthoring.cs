using System;
using Unity.Entities;
using UnityEngine;

namespace AirSeaBattle.Components
{
    [DisallowMultipleComponent]
    public class MaterialReferencesAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Material IddleGun;
        public Material[] GunPositions;
 
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddSharedComponentData(entity, new MaterialReferences()
            {
                IddleGun = IddleGun,
                GunPositions = GunPositions
            });
        }
    }


    [Serializable]
    public struct MaterialReferences : ISharedComponentData, IEquatable<MaterialReferences>
    {
        public Material IddleGun;
        public Material[] GunPositions;

        public bool Equals(MaterialReferences other)
        {
            return IddleGun == other.IddleGun;
        }

        public override int GetHashCode()
        {
            return IddleGun.GetHashCode();
        }
    }
}