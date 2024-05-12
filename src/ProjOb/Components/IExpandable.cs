namespace ProjOb
{
    public interface IExpandable
    {
        void Apply(IComponent component);
        T Apply<T>(IComponent<T> component);
    }
}
