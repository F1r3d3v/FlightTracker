namespace ProjOb.Query.Wrappers
{
    public class WorldPositionAccessor : BaseAccessor
    {
        public WorldPosition Position;

        public WorldPositionAccessor(WorldPosition pos)
        {
            Position = pos;
            _getValueTypeMap.Add("Lon", () => Position.Longitude.ToString());
            _setValueMap.Add("Lon", (String value) => Position.Longitude = double.Parse(value));

            _getValueTypeMap.Add("Lat", () => Position.Latitude.ToString());
            _setValueMap.Add("Lat", (String value) => Position.Latitude = double.Parse(value));
        }
    }
}
