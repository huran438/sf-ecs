using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace SFramework.ECS.Runtime
{
    public static class SFEntityMappingService
    {
        private static readonly Dictionary<int, EcsPackedEntity> _packedEntities;

        static SFEntityMappingService()
        {
            _packedEntities = new Dictionary<int, EcsPackedEntity>();
        }

        internal static void AddMapping(in GameObject gameObject, ref EcsWorld world, ref int entity)
        {
            _packedEntities[gameObject.GetInstanceID()] = world.PackEntity(entity);
        }

        internal static void AddMapping(in GameObject gameObject, ref EcsPackedEntity entity)
        {
            _packedEntities[gameObject.GetInstanceID()] = entity;
        }

        internal static void RemoveMapping(in GameObject gameObject)
        {
            var instanceId = gameObject.GetInstanceID();
            if (_packedEntities.ContainsKey(instanceId))
            {
                _packedEntities.Remove(instanceId);
            }
        }

        public static bool GetEntity(in GameObject gameObject, in EcsWorld world, out int entity)
        {
            var instanceId = gameObject.GetInstanceID();
            if (_packedEntities.ContainsKey(instanceId)) return _packedEntities[instanceId].Unpack(world, out entity);
            entity = default;
            return false;
        }

        public static bool GetEntityPacked(in GameObject gameObject, in EcsWorld world,
            out EcsPackedEntity packedEntity)
        {
            var instanceId = gameObject.GetInstanceID();

            if (_packedEntities.ContainsKey(instanceId))
            {
                packedEntity = _packedEntities[instanceId];
                return true;
            }

            packedEntity = default;
            return false;
        }

        public static void Clean()
        {
            _packedEntities.Clear();
        }
    }
}