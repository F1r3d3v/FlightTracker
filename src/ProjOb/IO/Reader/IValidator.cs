namespace ProjOb.IO
{
    internal interface IValidator
    {
        void Validate(out Dictionary<String, String[]> dict);
    }
}
