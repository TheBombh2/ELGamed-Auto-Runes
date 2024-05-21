using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Data;
using ELGamedAutoRunes.Models;
using System.Net;
using System.Security.Policy;

namespace ELGamedAutoRunes
{
	public class uggScrapper
	{
		private HtmlWeb web;
		private string LINK = "https://u.gg/lol/champions/";

		public uggScrapper()
		{
			if(!checkUGGWebsite())
			{
				MessageBox.Show("Please check U.GG Website");
				Environment.Exit(0);
			}

			web = new HtmlWeb();
		}

		private bool checkUGGWebsite()
		{
			var ping = new System.Net.NetworkInformation.Ping();
			try
			{
				var result = ping.Send("www.u.gg");

				if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public championData getChampionInformation(string championName)
		{
            //Load specific chamption DOM
            HtmlAgilityPack.HtmlDocument document = loadDocument(championName.ToLower(), "build");

			//get tags thats has a lot of needed information
			HtmlNode roleParentDiv = document.DocumentNode.QuerySelector("div.role-value");
			HtmlNode tierDiv = document.DocumentNode.QuerySelector("div.tier-header");
			HtmlNode winRateDiv = document.DocumentNode.QuerySelector("div.win-rate");
			HtmlNode winRateRankDiv = document.DocumentNode.QuerySelector("div.rank-option");

			List<HtmlNode> buildTabs = (List<HtmlNode>)document.DocumentNode.QuerySelectorAll("a.build-tab");

			//check if everything is found
			if (roleParentDiv == null || tierDiv == null || winRateDiv == null || winRateRankDiv == null ||
				buildTabs.Count <= 0)
			{
				return new championData() { isFound = false };
			}

			//Access childs here to make sure its parent div is not null
			HtmlNode roleDiv = roleParentDiv.ChildNodes[1];
			HtmlNode winRateRankImg = winRateRankDiv.FirstChild;

			//create object and insert values
			championData foundChampion = new championData();

			foundChampion.name = char.ToUpper(championName[0]) + championName.Substring(1).ToLower();
			foundChampion.role = roleDiv.InnerText;
			foundChampion.tier = tierDiv.InnerText;
			foundChampion.winRate = winRateDiv.ChildNodes[0].InnerText;
			foundChampion.winRateRank = winRateRankImg.GetAttributeValue("alt", "None");


			
			foundChampion.builds.Add(GetChampionBuild(document));


			if (buildTabs.Count > 1)
			{
				string buildLink = buildTabs[1].GetAttributeValue("href", "None");
				buildLink = "https://u.gg/lol" + buildLink;
				if (buildLink != "None")
				{
					HtmlAgilityPack.HtmlDocument newBuildTab = web.Load(buildLink);
					foundChampion.builds.Add(GetChampionBuild(newBuildTab));
				}
			}

			

			////Handle getting role and winrate
			//string firstSpan = paragraph.ChildNodes[0].InnerText.Trim();
			////Console.WriteLine(firstSpan);
			////pattern to extract them
			//string bigPattern = "([A-Za-z]+) ([A-Za-z]+) has a ([0-9]*\\.[0-9]+)% win rate in ([A-Za-z]+\\+) on Patch ([0-9]*\\.[0-9]+)";
			//Match match = Regex.Match(firstSpan, bigPattern);
			//if (match.Success)
			//{
			//	foundChampion.name = char.ToUpper(championName[0]) + championName.Substring(1);
			//	foundChampion.role = match.Groups[2].Value;
			//	foundChampion.winRate = match.Groups[3].Value;
			//	foundChampion.winRateRank = match.Groups[4].Value;

			//}

			//string tier = document.DocumentNode.QuerySelector("div.relative.left-[124px].top-[-92px].z-[-10].flex.h-[46px].w-[46px].items-center.justify-center.rounded-full.border.border-[#333333].bg-black").InnerText;

			//string tier = document.DocumentNode.QuerySelector("div[q:key='LN_3']").InnerText;
			//if (tier.IsNullOrEmpty())
			//{
			//	return new championData() { champFound = false };
			//}
			//else
			//{
			//	foundChampion.tier = tier;

			//}

			//string smallPattern = @"graded\s([A-Za-z]\-)";
			//match = Regex.Match(secondSpan, smallPattern);
			//if(match.Success)
			//{
			//	foundChampion.tier = match.Groups[1].Value;
			//}

			//HtmlNode PrimaryRunes = document.DocumentNode.QuerySelector("div[q:key='H1_5']").ChildNodes[0];



			//foreach (HtmlNode runeSpan in PrimaryRunes.ChildNodes)
			//{
			//	HtmlNode img = runeSpan.ChildNodes[0];
			//	if (!img.HasClass("grayscale"))
			//	{
			//		foundChampion.primaryRunes.Add(img.GetAttributeValue("alt", "None"));
			//	}
			//}




			foundChampion.isFound = true;
			return foundChampion;


		}

		
		public championBuild GetChampionBuild(HtmlAgilityPack.HtmlDocument document)
		{
			championBuild build = new championBuild();

			HtmlNode primaryRunesTreeDiv = document.DocumentNode.QuerySelector("div.primary-tree");
			HtmlNode secondaryRunesTreeDiv = document.DocumentNode.QuerySelector("div.secondary-tree");
			HtmlNode summonerSpellsDiv = document.DocumentNode.QuerySelector("div.summoner-spells");
			HtmlNode buildNameDiv = document.DocumentNode.QuerySelector("div.build-name");
			HtmlNode winRateMatchesDiv = document.DocumentNode.QuerySelector("div.wr-matches");



			//get winrate of this build
			build.buildWinRate = winRateMatchesDiv.ChildNodes[0].InnerText.Replace(" WR", "");
			build.buildName = buildNameDiv.InnerText;
			//Fetch KeyStone and primary runes
			build.primaryRuneTitle = primaryRunesTreeDiv.ChildNodes[0].ChildNodes[1].InnerText;
			build.primaryRuneTitle = System.Web.HttpUtility.HtmlDecode(build.primaryRuneTitle);
			switch (build.primaryRuneTitle)
			{
				case "Domination":
					build.primaryRuneId = 8100;
					break;
				case "Inspiration":
					build.primaryRuneId = 8300;
					break;
				case "Precision":
					build.primaryRuneId = 8000;
					break;
				case "Resolve":
					build.primaryRuneId = 8400;
					break;
				case "Sorcery":
					build.primaryRuneId = 8200;
					break;
				default:
					build.primaryRuneId = 8000;
					break;
			}

			foreach (HtmlNode rune in primaryRunesTreeDiv.ChildNodes[1].ChildNodes[1].ChildNodes)
			{
				if (rune.HasClass("perk-active"))
				{
					string runeName = rune.FirstChild.GetAttributeValue("alt", "None").Replace("The Keystone ", "");
					build.primaryRunes.Add(System.Web.HttpUtility.HtmlDecode(runeName));
				}
			}

			foreach (HtmlNode perk in primaryRunesTreeDiv.ChildNodes[2].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					string perkName = perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", "");
					build.primaryRunes.Add(System.Web.HttpUtility.HtmlDecode(perkName));
				}
			}

			foreach (HtmlNode perk in primaryRunesTreeDiv.ChildNodes[3].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					string perkName = perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", "");
					build.primaryRunes.Add(System.Web.HttpUtility.HtmlDecode(perkName));
				}
			}

			foreach (HtmlNode perk in primaryRunesTreeDiv.ChildNodes[4].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					string perkName = perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", "");
					build.primaryRunes.Add(System.Web.HttpUtility.HtmlDecode(perkName));
				}
			}

			//Fetch Secondary Runes

			build.secondaryRuneTitle = secondaryRunesTreeDiv.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;
			switch (build.secondaryRuneTitle)
			{
				case "Domination":
					build.secondaryRuneId = 8100;
					break;
				case "Inspiration":
					build.secondaryRuneId = 8300;
					break;
				case "Precision":
					build.secondaryRuneId = 8000;
					break;
				case "Resolve":
					build.secondaryRuneId = 8400;
					break;
				case "Sorcery":
					build.secondaryRuneId = 8200;
					break;
				default:
					build.secondaryRuneId = 8000;
					break;
			}

			foreach (HtmlNode perk in secondaryRunesTreeDiv.FirstChild.FirstChild.ChildNodes[1].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					build.secondaryRunes.Add(perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", ""));
				}
			}

			foreach (HtmlNode perk in secondaryRunesTreeDiv.FirstChild.FirstChild.ChildNodes[2].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					build.secondaryRunes.Add(perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", ""));
				}
			}

			foreach (HtmlNode perk in secondaryRunesTreeDiv.FirstChild.FirstChild.ChildNodes[3].ChildNodes[1].ChildNodes)
			{
				if (perk.HasClass("perk-active"))
				{
					build.secondaryRunes.Add(perk.FirstChild.GetAttributeValue("alt", "None").Replace("The Rune ", ""));
				}
			}


			//Fetch Stat Shards


			foreach (HtmlNode statShard in secondaryRunesTreeDiv.LastChild.FirstChild.ChildNodes[0].LastChild.ChildNodes)
			{
				if (statShard.HasClass("shard-active"))
				{
					build.statShards.Add(statShard.FirstChild.GetAttributeValue("alt", "None").Replace("The ", "").Replace(" Shard", ""));
				}
			}

			foreach (HtmlNode statShard in secondaryRunesTreeDiv.LastChild.FirstChild.ChildNodes[1].LastChild.ChildNodes)
			{
				if (statShard.HasClass("shard-active"))
				{
					build.statShards.Add(statShard.FirstChild.GetAttributeValue("alt", "None").Replace("The ", "").Replace(" Shard", ""));
				}
			}

			foreach (HtmlNode statShard in secondaryRunesTreeDiv.LastChild.FirstChild.ChildNodes[2].LastChild.ChildNodes)
			{
				if (statShard.HasClass("shard-active"))
				{
					build.statShards.Add(statShard.FirstChild.GetAttributeValue("alt", "None").Replace("The ", "").Replace(" Shard", ""));
				}
			}



			//Fetch summoner Spells
			build.firstSpell = summonerSpellsDiv.LastChild.FirstChild.GetAttributeValue("alt", "None").Replace("Summoner Spell ", "");
			build.firstSpellId = fetchSummonerSpellId(build.firstSpell);

			build.secondSpell = summonerSpellsDiv.LastChild.LastChild.GetAttributeValue("alt", "None").Replace("Summoner Spell ", "");
			build.secondSpellId = fetchSummonerSpellId(build.secondSpell);


			return build;
		}
		private HtmlAgilityPack.HtmlDocument loadDocument(string championName, string specificData)
		{
			string fullLink = LINK + championName + "/" + specificData + "/";
            HtmlAgilityPack.HtmlDocument document = web.Load(fullLink);
			return document;
		}

		private int fetchSummonerSpellId(string spellName)
		{
			switch (spellName)
			{
				case "Cleanse":
					 return 1;
				case "Flash":
					 return 4;
				case "Ghost":
					 return 6;
				case "Exhaust":
					 return 3;
				case "Heal":
					return 7;
				case "Smite":
					return 11;
				case "Teleport":
					return 12;
				case "Ignite":
					return 14;
				case "Barrier":
					return 21;
				case "Clarity":
					return 13;
				case "Mark":
					return 32;
				default:
					return 4;
			}
		}

		
	}

}

