using Leopotam.EcsLite;

namespace SFramework.ECS.Runtime
{
    public interface ISFEntity
    {
        EcsPackedEntityWithWorld EcsPackedEntity { get; }
        void Activate();
        void Deactivate();
    }
}