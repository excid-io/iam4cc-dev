using System.Runtime.InteropServices.JavaScript;

namespace Excid.Pdp.OpenFGA.Models
{
    public class NewAuthorizationModel
    {
        public string StoreId { get; set; } = string.Empty;
        public required OpenFga.Sdk.Client.Model.ClientWriteAuthorizationModelRequest  Model { get; set; }
    }
}
