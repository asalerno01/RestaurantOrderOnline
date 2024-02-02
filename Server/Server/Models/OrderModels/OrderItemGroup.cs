using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;
using System.Text.Json.Serialization;

namespace Server.Models.OrderModels
{
    public class OrderItemGroup : BaseModel
	{
        public long OrderItemGroupId { get; set; }
        public GroupOptionSnapshot GroupOption { get; set; }
    }
}
