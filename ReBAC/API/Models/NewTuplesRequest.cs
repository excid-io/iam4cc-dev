using OpenFga.Sdk.Client.Model;

namespace Excid.Pdp.OpenFGA.Models
{
    public class NewTuplesRequest
    {
        public required List<ClientTupleKey> Tuples{ get; set; }

    }
}
