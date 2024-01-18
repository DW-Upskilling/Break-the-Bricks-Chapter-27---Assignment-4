using UnityEngine;

namespace BreakTheBricks2D.GenericClass.MVC
{
    public abstract class View : MonoBehaviour
    {
        public Controller Controller { get; protected set; }

        public abstract void SetController(Controller _controller);
    }
}
