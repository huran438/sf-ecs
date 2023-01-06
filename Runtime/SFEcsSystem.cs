using Leopotam.EcsLite;
using SFramework.Core.Runtime;

namespace SFramework.ECS.Runtime
{
    public abstract class SFEcsSystem : IEcsInitSystem, IEcsRunSystem ,ISFInjectable
    {
        public void Init(IEcsSystems systems)
        {
            var container = systems.GetShared<ISFContainer>();
            container.Inject(this);
            Setup(systems, container);
        }

        public void Run(IEcsSystems systems)
        {
            Tick(ref systems);
        }

        protected virtual void Setup(IEcsSystems systems, ISFContainer container)
        {
        }
        

        protected virtual void Tick(ref IEcsSystems systems)
        {
        }
    }
}