namespace ProjOb
{
    internal abstract class Parser
    {
        public abstract Object? Parse(String[] props);

        protected static bool Populate(String[] props, Action<String[]> del)
        {
            try
            {
                del(props);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to parse the object: {e.Message}");
                return false;
            }

            return true;
        }
    }
}
