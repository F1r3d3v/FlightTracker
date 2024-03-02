namespace ProjOb
{
    internal class CargoPlaneParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var cargoPlaneDTO = new CargoPlaneDTO();

            void del(String[] props)
            {
                cargoPlaneDTO.Type = props[0];
                cargoPlaneDTO.ID = UInt64.Parse(props[1]);
                cargoPlaneDTO.Serial = props[2];
                cargoPlaneDTO.Country = props[3];
                cargoPlaneDTO.Model = props[4];
                cargoPlaneDTO.MaxLoad = Single.Parse(props[5]);
            }

            if (!Populate(props, del)) return null;

            return new CargoPlane(cargoPlaneDTO);
        }
    }
}
