namespace BreakTheBricks2D.GenericClass.MVC
{
    public abstract class Service<C> where C : Controller
    {
        public abstract Controller CreateController();
    }
}