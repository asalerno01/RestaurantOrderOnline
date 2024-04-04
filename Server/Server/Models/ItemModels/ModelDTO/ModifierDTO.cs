using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ItemModels.ModelDTO
{
	public class ModifierDTO
	{
		public long ModifierId { get; set; }
		public List<AddonDTO> Addons { get; set; }
		public List<NoOptionDTO> NoOptions { get; set; }
		public List<GroupDTO> Groups { get; set; }

		public ModifierDTO(Modifier modifier)
		{
			ModifierId = modifier.ModifierId;
			Addons = modifier.Addons.Select(a => new AddonDTO(a)).ToList();
			NoOptions = modifier.NoOptions.Select(n => new NoOptionDTO(n)).ToList();
			Groups = modifier.Groups.Select(g => new GroupDTO(g)).ToList();
		}
	}
}
