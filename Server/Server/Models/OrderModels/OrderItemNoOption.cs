using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.OrderModels
{
    public class OrderItemNoOption : BaseModel
    {
        public long OrderItemNoOptionId { get; set; }
        public NoOptionSnapshot NoOption { get; set; }
    }
}
