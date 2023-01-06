using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace SFramework.ECS.Runtime
{
    public class SFWorldsService : ISFWorldsService
    {
        private readonly Dictionary<Guid, EcsWorld> _ecsWorlds = new();
        private readonly Dictionary<Guid, bool> _ecsWorldsStates = new();

        public SFWorldsService()
        {
            Default = new EcsWorld();
            _ecsWorlds[Guid.Empty] = Default;
            _ecsWorldsStates[Guid.Empty] = true;
        }

        public EcsWorld Default { get; private set; }

        public EcsWorld GetWorld(Guid guid)
        {
            return _ecsWorlds.ContainsKey(guid) ? _ecsWorlds[guid] : _ecsWorlds[Guid.Empty];
        }
        
        public EcsWorld CreateWorld(out Guid guid, bool started = true)
        {
            var ecsWorld = new EcsWorld();
            guid = new Guid();
            _ecsWorlds[guid] = ecsWorld;
            _ecsWorldsStates[guid] = started;
            return ecsWorld;
        }
        
        public void DestroyWorld(Guid guid)
        {
            if (guid == Guid.Empty) return;
            if (!_ecsWorldsStates.ContainsKey(guid)) return;
            _ecsWorlds[guid].Destroy();
            _ecsWorlds.Remove(guid);
            _ecsWorldsStates.Remove(guid);
        }

        public void ResetDefaultWorld()
        {
            Default.Destroy();
            Default = new EcsWorld();
        }
    }
}