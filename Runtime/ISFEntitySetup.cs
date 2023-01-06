using Leopotam.EcsLite;

namespace SFramework.ECS.Runtime
{
    public interface ISFEntitySetup
    {
        bool DontDestroy { get; }
        void Setup(ref EcsWorld world, ref int entity, ref EcsPackedEntity packedEntity);
    }
}