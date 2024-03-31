namespace ProjOb.Media
{
    public class NewsGenerator
    {
        private readonly IEnumerator<KeyValuePair<IMedia, IReportable>> _it;

        public NewsGenerator(List<IMedia> mediaList, List<IReportable> newsSubjects)
        {
            _it = (from m in mediaList
                   from s in newsSubjects
                   select new KeyValuePair<IMedia, IReportable>(m, s)).GetEnumerator();
        }

        public string? GenerateNextNews()
        {
            if (!_it.MoveNext()) return null;
            var curr = _it.Current;
            return curr.Value.Apply(curr.Key);
        }
    }
}
