using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class CargoAccessor : ObjectAccessor
    {
        public CargoAccessor(Cargo cargo) : base(cargo)
        {
            _getValueMap.Add("Weight", cargo.Weight.ToString);
            _setValueMap.Add("Weight", (String value) => cargo.Weight = Single.Parse(value));

            _getValueMap.Add("Code", () => cargo.Code);
            _setValueMap.Add("Code", (String value) => cargo.Code = value);

            _getValueMap.Add("Description", () => cargo.Description);
            _setValueMap.Add("Description", (String value) => cargo.Description = value);
        }
    }
}
