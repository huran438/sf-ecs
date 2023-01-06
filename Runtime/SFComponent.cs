using System;
using Leopotam.EcsLite;
using SFramework.Core.Runtime;
using UnityEngine;

namespace SFramework.ECS.Runtime
{
    [Serializable]
    public abstract class SFComponent<T> : MonoBehaviour, ISFComponentInspector where T : struct
    {
        [SerializeField]
        private T _value;

        private EcsPackedEntity _packedEntity;
        private EcsWorld _world;

        public ref T value => ref _value;
        protected EcsPackedEntity PackedEntity => _packedEntity;
        protected EcsWorld World => _world;

        public bool DontDestroy => false;

        public void Setup(ref EcsWorld world, ref int entity, ref EcsPackedEntity packedEntity)
        {
            _world = world;
            _packedEntity = packedEntity;

            if (_value is IEcsAutoInit<T> value)
            {
                value.AutoInit(ref _value);
            }

            world.GetPool<T>().Add(entity) = _value;
        }
    }
}