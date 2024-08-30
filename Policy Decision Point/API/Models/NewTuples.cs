using OpenFga.Sdk.Client.Model;

namespace Excid.Pdp.OpenFGA.Models
{
    public class NewTuples
    {
        public string StoreId { get; set; } = string.Empty;
        public string AuthorizationModelId { get; set; } = string.Empty;
        public required List<ClientTupleKey> Tuples{ get; set; }

    }
}
