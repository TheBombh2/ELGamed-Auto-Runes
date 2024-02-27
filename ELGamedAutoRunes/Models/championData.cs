using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGamedAutoRunes.Models
{
    public class championData
    {
		public bool isFound { get; set; }

		public string? name { get; set; }
		public string? role { get; set; }
		public string? tier { get; set; }
		public string? winRate { get; set; }
		public string? winRateRank { get; set; }

		public List<championBuild> builds = new List<championBuild>();

		//public void printInformation()
		//{

		//	Console.WriteLine("------------------------------");
		//	Console.WriteLine("Name: " + name);
		//	Console.WriteLine("Role: " + role);
		//	Console.WriteLine("Win Rate: " + winRate +"%");
		//	Console.WriteLine("Win Rate Rank: " + winRateRank);
		//	Console.WriteLine("Tier: " + tier);			
		//	Console.WriteLine("------------------------------");
		//	Console.WriteLine("Primary Rune Family: " + primaryRuneTitle);
		//	Console.WriteLine("Primary Runes: ");
		//	foreach(var  rune in primaryRunes) Console.WriteLine(rune);
  //          Console.WriteLine("------------------------------");
		//	Console.WriteLine("Secondary Rune Family: " + secondaryRuneTitle);
		//	Console.WriteLine("Secondary Runes: ");
		//	foreach (var rune in secondaryRunes) Console.WriteLine(rune);
		//	Console.WriteLine("------------------------------");
		//	Console.WriteLine("Stat Shards: ");
		//	foreach (var stat in statShards) Console.WriteLine(stat);
		//	Console.WriteLine("------------------------------");
		//	Console.WriteLine("First Spell: " + firstSpell);
		//	Console.WriteLine("Second Spell: " + secondSpell);
		//}
	}


    
}
