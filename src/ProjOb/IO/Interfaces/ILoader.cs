namespace ProjOb.IO
{
    internal interface ILoader
    {
        IReader reader { get; }
        IValidator validator { get; }
        IParser parser { get; }
        ILinker linker { get; }

        List<Object> Load();
    }
}
