using SalernoServer.Models.ModelHelpers;

namespace SalernoServer.Models
{
    public class OrderItemHelper
    {
        public string ItemId { get; set; }
        public int Count { get; set; }
        public List<GroupOptionHelper> GroupOptions { get; set; }
        public List<AddonHelper> Addons { get; set; }
        public List<NoOptionHelper> NoOptions { get; set; }
    }
}
