using ProjOb.Exceptions;

namespace ProjOb.IO
{
    internal class FTRValidator : IValidator
    {
        private readonly Dictionary<string, int> entryLenght = new()
        {
            { "C",  8},
            { "P",  8},
            { "CA", 5},
            { "CP", 6},
            { "PP", 8},
            { "AI", 8},
            { "FL", 12},
        };

        public void Validate(Dictionary<String, String[]> dict)
        {
            foreach (var rec in dict)
            {

                if (entryLenght.TryGetValue(rec.Value[0], out int len))
                {
                    if (rec.Value.Length != len)
                    {
                        throw new DataIntegrityException("Invalid amount of parameters.");
                    }
                }    
                else
                {
                    throw new DataIntegrityException($"Unknown type: {rec.Value[0]}");
                }


                if (rec.Value[0] == "FL")
                {
                    String[]? originAirport;
                    String[]? targetAirport;
                    if (dict.TryGetValue(rec.Value[2], out originAirport) && dict.TryGetValue(rec.Value[3], out targetAirport))
                    {
                        if (originAirport[0] != "AI" || targetAirport[0] != "AI")
                        {
                            throw new DataIntegrityException("The object referenced by the Flight object should be an Airport.");
                        }

                    }
                    else
                    {
                        throw new MissingReferenceException("Orphan ID in the Flight object.");
                    }


                    String[]? plane;
                    if (dict.TryGetValue(rec.Value[2], out plane))
                    {
                        if (plane[0] != "CP" || targetAirport[0] != "PP")
                        {
                            throw new DataIntegrityException("The object referenced by the Flight object should be either Cargo Plane or Passenger Plane.");
                        }
                    }
                    else
                    {
                        throw new MissingReferenceException("Orphan ID in the Flight object.");
                    }


                    String[] crews = rec.Value[9][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    foreach (String id in crews)
                    {
                        String[]? crew;
                        if (dict.TryGetValue(id, out crew))
                        {
                            if (crew[0] != "C")
                            {
                                throw new DataIntegrityException("The object referenced by the Flight object should be either Cargo Plane or Passenger Plane.");
                            }
                        }
                        else
                        {
                            throw new MissingReferenceException("Orphan ID in the Flight object.");
                        }
                    }

                    String[] loads = rec.Value[10][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    foreach (String id in loads)
                    {
                        String[]? load;
                        if (dict.TryGetValue(id, out load))
                        {
                            if (load[0] != "C" || load[0] != "P")
                            {
                                throw new DataIntegrityException("The object referenced by the Flight object should be either Cargo or Passenger.");
                            }
                        }
                        else
                        {
                            throw new MissingReferenceException("Orphan ID in the Flight object.");
                        }
                    }
                }
            }
        }
    }
}
