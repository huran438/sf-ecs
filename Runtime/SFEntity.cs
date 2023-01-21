using Leopotam.EcsLite;
using SFramework.Core.Runtime;
using UnityEngine;

namespace SFramework.ECS.Runtime
{
    [SFHideScriptField]
    [DisallowMultipleComponent]
    public sealed class SFEntity : SFView, ISFEntity
    {
        public EcsPackedEntityWithWorld EcsPackedEntity => _ecsPackedEntity;

        [SFInject]
        private ISFWorldsService _worldsService;
        private EcsPackedEntityWithWorld _ecsPackedEntity;
        private bool _injected;
        private ISFEntitySetup[] _components;
        private bool _activated;

        protected override void Init()
        {
            if (_injected) return;
            _components = GetComponents<ISFEntitySetup>();
            Activate();
            _injected = true;
        }

        public void Activate()
        {
            if (_activated) return;

            gameObject.SetActive(true);

            var _entity = _worldsService.Default.NewEntity();
            _ecsPackedEntity = _worldsService.Default.PackEntityWithWorld(_entity);

            SFEntityMapping.AddMapping(gameObject, ref _ecsPackedEntity);

            _worldsService.Default.GetPool<GameObjectRef>().Add(_entity) = new GameObjectRef
            {
                value = gameObject
            };

            var _transform = transform;

            _worldsService.Default.GetPool<TransformRef>().Add(_entity) = new TransformRef
            {
                value = _transform
            };

            foreach (var entitySetup in _components)
            {
                entitySetup.Setup(ref _ecsPackedEntity);
            }

            _activated = true;
        }

        public void Deactivate()
        {
            SFEntityMapping.RemoveMapping(gameObject);

            if (_ecsPackedEntity.Unpack(out var _world, out var _entity))
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