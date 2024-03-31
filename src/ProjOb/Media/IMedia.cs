namespace ProjOb.Media
{
    public interface IMedia : IComponent<string>
    {
        string Name { get; }
    }
}
