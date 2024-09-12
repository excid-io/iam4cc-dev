using OpenFga.Sdk.Client;

namespace Excid.Pdp.OpenFGA
{
    public interface IOpenFGA
    {
        public OpenFgaClient Client { get; set; }

    }
}
