using Leopotam.EcsLite;

namespace SFramework.ECS.Runtime
{
    public interface ISFEntity
    {
        EcsWorld World { get; }
        EcsPackedEntity EcsPackedEntity { get; }

        void Activate();
        void Deactivate();
    }
}