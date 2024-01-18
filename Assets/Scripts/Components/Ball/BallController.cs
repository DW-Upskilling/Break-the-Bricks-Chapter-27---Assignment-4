using UnityEngine;

using BreakTheBricks2D.GenericClass.MVC;
using BreakTheBricks2D.ScriptableObjects;

namespace BreakTheBricks2D.Component.Ball
{
    public class BallController : Controller<BallScriptableObject, BallModel, BallView>
    {
        public BallController(BallScriptableObject _scriptableObject) : base(_scriptableObject)
        {

        }

        public void LaunchAt(Vector3 direction)
        {

        }

        public override BallModel CreateModel(BallScriptableObject _scriptableObject) => new BallModel(_scriptableObject);
        public override BallView InstatiateView(BallScriptableObject _scriptableObject) => GameObject.Instantiate<BallView>(_scriptableObject.Prefab, Vector3.zero, Quaternion.identity);
    }
}
