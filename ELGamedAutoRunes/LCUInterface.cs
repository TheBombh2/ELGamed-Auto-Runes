using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ELGamedAutoRunes.Models;
using LCUSharp;
using LCUSharp.Websocket;
using Newtonsoft.Json;


namespace ELGamedAutoRunes
{
    public class LCUInterface
	{
		private Main mainUI;

		private LeagueClientApi? leagueClient;
		private uggScrapper scrapper = new uggScrapper();
		private List<runeModel>? runeModels;
		List<championModel>? championModels;




		public event EventHandler<LeagueEvent>? gameFlowChanged;
		public event EventHandler<LeagueEvent>? champSelectChanged;

		private bool championLocked = false;


		public LCUInterface(Main ui)
        {
			//make a get request and recive data in put it in runeModels
			initlizeData();
			mainUI = ui;
        }

        public async void connectToLC()
		{
			
			leagueClient = await LeagueClientApi.ConnectAsync();
			mainUI.updateConnectionLBL("League is connected!");
			mainUI.updateUIState("None");
			await subscribeToEvents();
		}

		public async Task subscribeToEvents()
		{
			if(leagueClient == null)
			{
				return;
			}

			gameFlowChanged += onGameFlowChanged;
			leagueClient.EventHandler.Subscribe("/lol-gameflow/v1/gameflow-phase", gameFlowChanged);

			champSelectChanged += onChampSelected;
			leagueClient.EventHandler.Subscribe("/lol-champ-select/v1/current-champion", champSelectChanged);

			Console.WriteLine("Subscribed to events");
		}


		
		public async void getCurrentChamp()
		{
			var json = await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Get,"lol-summoner/v1/current-summoner");
			var goodData = Newtonsoft.Json.Linq.JObject.Parse(json);
			await Console.Out.WriteLineAsync(goodData.ToString());
		}
		
		public async void setUserProfileIcon(int iconId)
		{
			var body = new
			{
				profileIconId = iconId
			};

			var queryParameters = Enumerable.Empty<string>();
			try
			{
				var json = await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Put, "/lol-summoner/v1/current-summoner/icon",queryParameters, body);

				Console.WriteLine($"Icon {iconId} was inserted.");
				
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
		}

		public championData prepareChampionSelect(string championName)
		{
			if (string.IsNullOrEmpty(championName))
			{
				Console.WriteLine("Please enter a champion name.");
				return new championData { isFound = false};
			}

			championData foundChampionData = scrapper.getChampionInformation(championName);

			if (!foundChampionData.isFound)
			{
				Console.WriteLine("Champion was not found.");
				return new championData { isFound = false };
			}

			return foundChampionData;
		}

		public async void setCurrentPageRune(championBuild foundChampBuild,string foundChampName)
		{


			if (runeModels.Count <= 0)
			{
				Console.WriteLine("Error in get request");
				return;
			}

			List<int> primaryRunesIds = getRunesIds(foundChampBuild.primaryRunes);
			List<int> secondaryRunesIds = getRunesIds(foundChampBuild.secondaryRunes);
			List<int> statShardsIds = getRunesIds(foundChampBuild.statShards);
			//foundChampionData.printInformation();
			//Get current rune page id
			try
			{
				var data = await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Get, "/lol-perks/v1/currentpage");
				var DataButJsonDeserialized = Newtonsoft.Json.Linq.JObject.Parse(data);
				var id = DataButJsonDeserialized["id"];
				//Remove current rune page by id
				await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Delete, $"/lol-perks/v1/pages/{id}");

			}
			catch
			{
                await Console.Out.WriteLineAsync("No page to delete");
            }
				//Create rune page and put it
			var body = new
			{
				name = "ELGamed : " + foundChampName,
				primaryStyleId = foundChampBuild.primaryRuneId,
				subStyleId = foundChampBuild.secondaryRuneId,
				selectedPerkIds = primaryRunesIds.Concat(secondaryRunesIds).Concat(statShardsIds),
				current = true

			};

			var queryParameters = Enumerable.Empty<string>();
			await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-perks/v1/pages",queryParameters,body);
			await Console.Out.WriteLineAsync("Applied runes to the current runes page");



		}

		public async void setSummonerSpells(championBuild foundChampBuild)
		{
			//Get the normal order if spells
			int? firstSpell = foundChampBuild.firstSpellId;
			int? secondSpell = foundChampBuild.secondSpellId;

			//switch them if the checkbox is checked
			if (mainUI.switchSpellsCB.Checked) {
				var temp = secondSpell;
				secondSpell = firstSpell;
				firstSpell = temp;
			}
			
			var body = new
			{
				spell1Id = firstSpell,
				spell2Id = secondSpell,
			};

			var queryParameters = Enumerable.Empty<string>();
			await leagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Patch, "/lol-champ-select/v1/session/my-selection",queryParameters ,body);
			await Console.Out.WriteLineAsync("Spells set");
		}

		private List<int> getRunesIds(List<string> runesNames)
		{
			List<int> runesById = new List<int>();


			foreach (string runeName in runesNames)
			{
				foreach (var runeObj in runeModels)
				{
					if (runeName.Equals(runeObj.Name))
					{
						runesById.Add(runeObj.Id);
					}
				}
			}
			return runesById;

		}

		private async void initlizeData()
		{
			using (HttpClient client = new())
			{
				try
				{
					await Console.Out.WriteLineAsync("Getting runes data...");
					HttpResponseMessage response = await client.GetAsync("https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/perks.json");

					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						runeModels = JsonConvert.DeserializeObject<List<runeModel>>(json);
                        await Console.Out.WriteLineAsync("Runes data gatherd.");
                    }
				}
				catch 
				{
					MessageBox.Show("Couldn't get the runes data.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Environment.Exit(0);
				}
			}
			try
			{
                await Console.Out.WriteLineAsync("Reading Runes data from json file.");
                string jsonContent = File.ReadAllText("data/championsById.json");
				try
				{
					championModels = JsonConvert.DeserializeObject<List<championModel>>(jsonContent);
					await Console.Out.WriteLineAsync("Runes read.");
				}
				catch
				{
					MessageBox.Show("", "Error in json file",MessageBoxButtons.OK, MessageBoxIcon.Error);
					Environment.Exit(0);
                }
			}

			
			catch
			{
				MessageBox.Show("\"championsById.json\" was not found in data folder!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(0);
			}
		}


		private void onGameFlowChanged(object sender,LeagueEvent e)
		{
			var result = e.Data.ToString();
			Console.WriteLine(result);
			mainUI.updateUIState(result);
			if(result != "ChampSelect")
			{
				championLocked = false;
				mainUI.updateChampInformation("None");

			}
		}

		private void onChampSelected(object sender, LeagueEvent e)
		{
			if (mainUI.currentState == "ChampSelect" && !championLocked)
			{
				championLocked = true;
                Console.WriteLine("Champion LOCKED");
                var championId = e.Data.ToString();
				string name = convertIdToName(championId);
				Console.WriteLine(name);
				mainUI.updateChampInformation(name);


			}
		}

		private string convertIdToName(string key)
		{
			
			foreach(championModel model in championModels)
			{
				if(model.key == key)
				{
					return model.id;
				}
			}

			return "noChampSelected";
		}

	}
}
