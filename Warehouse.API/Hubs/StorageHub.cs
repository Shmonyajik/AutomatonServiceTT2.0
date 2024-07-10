using Microsoft.AspNetCore.SignalR;

namespace Warehouse.API.Hubs;

public class StorageHub : Hub
{
    public async Task SendQuantityChanged(long storageId, long productId, int quantityDiff)
    {
        await Clients.Others.SendAsync("ReceiveQuantityChanged", storageId, productId, quantityDiff);
    }

    public async Task SendProductAdded(long storageId, long productId)
    {
        await Clients.Others.SendAsync("ReceiveProductAdded", storageId, productId);
    }
}