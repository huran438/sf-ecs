using System;
using Leopotam.EcsLite;
using SFramework.Core.Runtime;

namespace SFramework.ECS.Runtime
{
    public interface ISFWorldsService : ISFService
    {
        EcsWorld Default { get; }
        public EcsWorld GetWorld(Guid guid);
        public EcsWorld CreateWorld(out Guid guid, bool started = true);
        public void DestroyWorld(Guid guid);
        public void ResetDefaultWorld();
    }
}