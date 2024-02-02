using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.OrderModels
{
    public class OrderItemAddon : BaseModel
	{
        public long OrderItemAddonId { get; set; }
        public AddonSnapshot Addon { get; set; }
    }
}
