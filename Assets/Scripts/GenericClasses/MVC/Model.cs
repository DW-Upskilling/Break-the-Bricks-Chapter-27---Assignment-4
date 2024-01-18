using UnityEngine;

namespace BreakTheBricks2D.GenericClass.MVC
{
    public abstract class Model
    {
        
    }

    public abstract class Model<T> : Model where T : ScriptableObject {
        public T ScriptableObject { get; private set; }

        public Model(T _scriptableObject): base()
        {
            this.ScriptableObject = ScriptableObject;
        }
    }
}
