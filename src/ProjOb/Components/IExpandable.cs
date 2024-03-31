namespace ProjOb
{
    public interface IExpandable<TResult>
    {
        TResult Apply(IComponent<TResult> component);
    }

    public interface IExpandable
    {
        void Apply(IComponent component);
    }
}
