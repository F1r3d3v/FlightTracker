namespace ProjOb
{
    internal class CargoParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var cargoDTO = new CargoDTO();

            void del(String[] props)
            {
                cargoDTO.Type = props[0];
                cargoDTO.ID = UInt64.Parse(props[1]);
                cargoDTO.Weight = Single.Parse(props[2]);
                cargoDTO.Code = props[3];
                cargoDTO.Description = props[4];
            }

            if (!Populate(props, del)) return null;

            return new Cargo(cargoDTO);
        }
    }
}
