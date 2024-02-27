using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGamedAutoRunes.Models
{
	public class rune
	{
		public int id { get; set; }
		public string? key { get; set; }
		public string? icon { get; set; }
		public string? name { get; set; }
		public string? shortDesc { get; set; }
		public string? longDesc { get; set; }
	}

	public class slot
	{
		public List<rune>? runes { get; set; }
	}

	public class perkObject
	{
		public int? id { get; set; }
		public string? key { get; set; }
		public string? icon { get; set; }
		public string? name { get; set; }
		public List<slot>? slots { get; set; }
	}

	

}
