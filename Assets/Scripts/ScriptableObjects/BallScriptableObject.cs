using UnityEngine;

using BreakTheBricks2D.Component.Ball;

namespace BreakTheBricks2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallScriptableObject", menuName = "Scriptable Object/Ball/New")]
    public class BallScriptableObject : ScriptableObject
    {

        [SerializeField]
        private BallView _prefab;
        public BallView Prefab { get { return _prefab; } }

        [SerializeField]
        private PhysicsMaterial2D _physicsMaterial2D;
        public PhysicsMaterial2D PhysicsMaterial2D { get { return _physicsMaterial2D; } }
    }
}
