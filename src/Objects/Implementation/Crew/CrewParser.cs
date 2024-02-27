namespace ProjOb
{
    internal class CrewParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var crewDTO = new CrewDTO();

            void del(String[] props)
            {
                crewDTO.Type = props[0];
                crewDTO.ID = UInt64.Parse(props[1]);
                crewDTO.Name = props[2];
                crewDTO.Age = UInt64.Parse(props[3]);
                crewDTO.Phone = props[4];
                crewDTO.Email = props[5];
                crewDTO.Practice = UInt16.Parse(props[6]);
                crewDTO.Role = props[7];
            }

            if (!Populate(props, del)) return null;

            return new Crew(crewDTO);
        }
    }
}
