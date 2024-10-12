using Excid.Pdp.OpenFGA.Models;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Model;

namespace Excid.Pdp.OpenFGA
{
    public interface IOpenFGA
    {
        public Task<ClientWriteResponse?> NewTuples(NewTuplesRequest newTuples);
        public Task<CreateStoreResponse?> NewStore(NewStoreRequest newStore);
        public Task<WriteAuthorizationModelResponse?> NewModel(NewAuthorizationModelRequest newModel);
        public Task<CheckResponse?> Check(ClientCheckRequest clientCheckRequest);

    }
}
