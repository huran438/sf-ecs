using Leopotam.EcsLite;
using SFramework.Core.Runtime;
using UnityEngine;

namespace SFramework.ECS.Runtime
{
    [SFHideScriptField]
    [DisallowMultipleComponent]
    public sealed class SFEntity : SFView, ISFEntity
    {
        public EcsWorld World => _world;
        public EcsPackedEntity EcsPackedEntity => _ecsPackedEntity;
        public int EcsEntity => _ecsUnpackedEntity;

        [SFInject]
        private ISFWorldsService _worldsService;

        private EcsPackedEntity _ecsPackedEntity;
        private int _ecsUnpackedEntity;
        private EcsWorld _world;
        private bool _injected;

        private ISFEntitySetup[] _cachedComponents;
        private bool _activated;

        protected override void Init()
        {
            if (_injected) return;
            _world = _worldsService.Default;
            _cachedComponents = GetComponents<ISFEntitySetup>();
            Activate();
            _injected = true;
        }

        public void Activate()
        {
            if (_activated) return;

            gameObject.SetActive(true);

            var _entity = _world.NewEntity();
            _ecsPackedEntity = _world.PackEntity(_entity);
            _ecsUnpackedEntity = _entity;

            SFEntityMappingService.AddMapping(gameObject, ref _ecsPackedEntity);

            _world.GetPool<GameObjectRef>().Add(_entity) = new GameObjectRef
            {
                reference = gameObject,
                entity = this
            };

            var _transform = transform;

            _world.GetPool<TransformRef>().Add(_entity) = new TransformRef
            {
                reference = _transform,
                initialPosition = _transform.position,
                initialRotation = _transform.rotation,
                initialScale = _transform.localScale
            };

            foreach (var entitySetup in _cachedComponents)
            {
                entitySetup.Setup(ref _world, ref _ecsUnpackedEntity, ref _ecsPackedEntity);
            }

            _activated = true;
        }

        public void Deactivate()
        {
            SFEntityMappingService.RemoveMapping(gameObject);

            if (_ecsPackedEntity.Unpack(_world, out var _entity))
                _world.DelEntity(_entity);

            gameObject.SetActive(false);
            _activated = false;
        }

        private void OnDestroy()
        {
            Deactivate();
        }
    }
}