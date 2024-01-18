using BreakTheBricks2D.GenericClass.MVC;
using BreakTheBricks2D.ScriptableObjects;

namespace BreakTheBricks2D.Component.Ball
{
    public class BallService : Service<BallController>
    {
        private BallScriptableObject[] _ballScriptableObjects;

        public BallService(BallScriptableObject[] _ballScriptableObjects) {
            this._ballScriptableObjects = _ballScriptableObjects;
        }

        public override Controller CreateController() {

            BallScriptableObject ballScriptableObject = _ballScriptableObjects[0];

            return new BallController(ballScriptableObject);
        }
    }
}
