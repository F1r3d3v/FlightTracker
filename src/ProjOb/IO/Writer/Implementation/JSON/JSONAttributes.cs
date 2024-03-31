namespace ProjOb.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonOnlyIDAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class JsonOnlyDictValAttribute : Attribute { }
}
