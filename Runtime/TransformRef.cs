using UnityEngine;

namespace SFramework.ECS.Runtime
{
    public struct TransformRef : ISFComponent
    {
        public Transform reference;
        public Vector3 initialPosition;
        public Quaternion initialRotation;
        public Vector3 initialScale;
    }
}