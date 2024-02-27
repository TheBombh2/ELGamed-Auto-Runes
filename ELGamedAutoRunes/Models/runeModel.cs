using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGamedAutoRunes.Models
{
	public class runeModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? MajorChangePatchVersion { get; set; }
		public string? Tooltip { get; set; }
		public string? ShortDesc { get; set; }
		public string? LongDesc { get; set; }
		public string? RecommendationDescriptor { get; set; }
		public string? IconPath { get; set; }
		public List<string>? EndOfGameStatDescs { get; set; }
		public Dictionary<string, object>? RecommendationDescriptorAttributes { get; set; }
	}
}
