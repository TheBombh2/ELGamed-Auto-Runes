using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGamedAutoRunes.Models
{
	public class championBuild
	{
		public string? buildName;

		public string? buildWinRate;

		public string? primaryRuneTitle;
		public int? primaryRuneId;
		public List<string> primaryRunes = new List<string>();

		public string? secondaryRuneTitle;
		public int? secondaryRuneId;
		public List<string> secondaryRunes = new List<string>();

		public List<string> statShards = new List<string>();

		public string? firstSpell;
		public int? firstSpellId;

		public string? secondSpell;
		public int? secondSpellId;

	}
}
