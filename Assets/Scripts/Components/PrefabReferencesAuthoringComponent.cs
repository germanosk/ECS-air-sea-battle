using System.Collections.Generic;
using AirSeaBattle.Controllers;
using AirSeaBattle.Util;
using Unity.Entities;
using UnityEngine;

namespace AirSeaBattle.Components
{
    public class PrefabReferencesAuthoringComponent: MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject AirplanePrefab;
        public GameObject BulletPrefab;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            // Avoiding add components if null references.
            if (HaveNullPrefabs())
            {
                return;
            }

            dstManager.AddComponentData(entity,
                new PrefabReferencesComponent()
                {
                    AirplanePrefabEntity = conversionSystem.GetPrimaryEntity(AirplanePrefab),
                    BulletPrefabEntity = conversionSystem.GetPrimaryEntity(BulletPrefab),
                    AirplaneInstances = GameManager.Instance.MaxAiplanesPerWave,
                    BulletInstances = GameManager.Instance.MaxBulletsOnScreen
                });
            dstManager.AddComponent<WaitingForSpawn>(entity);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            // Avoiding add components if null references.
            if (HaveNullPrefabs())
            {
                return;
            }
            referencedPrefabs.Add(AirplanePrefab);
            referencedPrefabs.Add(BulletPrefab);
        }

        private bool HaveNullPrefabs()
        {
            bool isNull = ReferenceUtil.TestNullReferences(AirplanePrefab, "Airplane Prefab", "PrefabReferencesAuthoringComponent");
            isNull |= ReferenceUtil.TestNullReferences(BulletPrefab, "Bullet Prefab", "PrefabReferencesAuthoringComponent");
            return isNull;
        }
    }
}