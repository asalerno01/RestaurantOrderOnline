namespace SalernoServer.Models.ItemModels
{
    // Hide what client shouldn't see
    public class ItemModifiersDTO
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public List<Modifier> Modifiers { get; set; }
    }
}
