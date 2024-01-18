using UnityEngine;

namespace BreakTheBricks2D.GenericClass.MVC
{
    public abstract class Controller
    {
        
    }

    public abstract class Controller<V>: Controller where V: View
    {
        public V View { get; private set; }

        public Controller(V _view): base()
        {
            this.View = InstatiateView(_view);
        }

        public abstract V InstatiateView(V _view);
    }

    public abstract class Controller<M, V> : Controller<V> where M : Model where V: View
    {
        public M Model { get; private set; }

        public Controller(V _view): base(_view)
        {
            this.Model = CreateModel(_view);
        }

        public abstract M CreateModel(V _view);
    }

    public abstract class Controller<S, M, V> : Controller where S: ScriptableObject where M : Model where V : View
    {
        public V View { get; private set; }
        public M Model { get; private set; }

        public Controller(S _scriptableObject): base()
        {
            this.View = InstatiateView(_scriptableObject);
            this.Model = CreateModel(_scriptableObject);
        }

        public abstract M CreateModel(S _scriptableObject);
        public abstract V InstatiateView(S _scriptableObject);
    }
}
