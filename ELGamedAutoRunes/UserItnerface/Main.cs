

using ELGamedAutoRunes.Models;

namespace ELGamedAutoRunes
{
	public partial class Main : Form
	{
		private LCUInterface? leagueInterface;
		//private bool connected = false;
		public string? currentState;

		private string lockedInChamp = "None";
		private championData? lockedInChampData;


		public Main()
		{
			InitializeComponent();
			updateUIState("Disconnected.");
		}

		public void Main_Load(object sender, EventArgs e)
		{
			leagueInterface = new LCUInterface(this);

			leagueInterface.connectToLC();


		}

		public void updateUIState(string newState)
		{
			string previousState = currentState;
			currentState = newState;

			if (currentState == "ChampSelect")
			{
				toggleChampView();

			}
			else if (previousState == "ChampSelect")
			{
				clearTreeViews();
				toggleChampView();
				toggleApplyBtns(false);
				lockedInChampData = null;
				lockedInChamp = "None";
				updateSelectedChampLBLS(lockedInChamp, "No Role");
			}

			updateStateLBL(currentState);
		}

		public void updateChampInformation(string name)
		{
			if (currentState == "ChampSelect")
			{
				lockedInChamp = name;
				updateSelectedChampLBLS(lockedInChamp, "No Role");

				if (lockedInChamp != "None")
				{

					lockedInChampData = leagueInterface.prepareChampionSelect(lockedInChamp);
					updateSelectedChampLBLS(lockedInChamp, lockedInChampData.role);

					updateRuneTV(firstRunesTV, lockedInChampData.builds[0]);
					updateWinRateLBL(firstWinRateLBL, lockedInChampData.builds[0].buildWinRate);

					if (lockedInChampData.builds.Count > 1)
					{
						updateRuneTV(secondRunesTV, lockedInChampData.builds[1]);
						updateWinRateLBL(secondWinRateLBL, lockedInChampData.builds[1].buildWinRate);
					}

					toggleApplyBtns(lockedInChampData.builds.Count > 1);
					toggleRandomizeSkinBTN();
				}
			}
		}

		private void updateSelectedChampLBLS(string? name, string? role)
		{

			if (InvokeRequired)
			{
				//called from another thread
				selectedChampLBL.Invoke(new Action<string, string>(updateSelectedChampLBLS), name, role);
			}
			else
			{

				selectedChampLBL.Text = "Selected Champ: " + name;
				champRoleLBL.Text = "Role: " + role;
			}
		}

		public void updateConnectionLBL(string text)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<string>(updateConnectionLBL), text);
			}
			else
			{
				connectionStatusLBL.Text = text;
			}
		}

		private void updateStateLBL(string newState)
		{

			if (currentStateLBL.InvokeRequired)
			{
				//called from another thread
				currentStateLBL.Invoke(new Action<string>(updateStateLBL), newState);
			}
			else
			{


				if (newState == "None")
				{
					newState = "Menu";
				}


				currentStateLBL.Text = "Current State: " + newState;
			}
		}

		private void toggleChampView()
		{
			if (selectedChampLBL.InvokeRequired && firstRunesTV.InvokeRequired &&
				secondRunesTV.InvokeRequired && firstRuneApplyBTN.InvokeRequired &&
				secondRuneApplyBTN.InvokeRequired)
			{
				selectedChampLBL.Invoke(new Action(toggleChampView));
			}
			else
			{
				selectedChampLBL.Visible = !selectedChampLBL.Visible;
				firstRunesTV.Visible = !firstRunesTV.Visible;
				secondRunesTV.Visible = !secondRunesTV.Visible;
				switchSpellsCB.Visible = !switchSpellsCB.Visible;
				firstRuneApplyBTN.Visible = !firstRuneApplyBTN.Visible;
				secondRuneApplyBTN.Visible = !secondRuneApplyBTN.Visible;
				champRoleLBL.Visible = !champRoleLBL.Visible;
				getRandomSkinBTN.Visible = !getRandomSkinBTN.Visible;

			}
		}

		private void toggleApplyBtns(bool secondBuildExist)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<bool>(toggleApplyBtns), secondBuildExist);
			}
			else
			{
				firstRuneApplyBTN.Enabled = !firstRuneApplyBTN.Enabled;
				firstWinRateLBL.Visible = !firstWinRateLBL.Visible;

				secondRuneApplyBTN.Enabled = secondBuildExist;
				secondWinRateLBL.Visible = secondBuildExist;


			}
		}

		private void toggleRandomizeSkinBTN()
		{
			if(InvokeRequired)
			{
				Invoke(new Action(toggleRandomizeSkinBTN), null);
			}
			else
			{
				getRandomSkinBTN.Enabled = !getRandomSkinBTN.Enabled;
			}
		}

		private void firstRuneApplyBTN_Click(object sender, EventArgs e)
		{
			if (lockedInChamp != "None" && lockedInChampData != null)
			{
				leagueInterface.setCurrentPageRune(lockedInChampData.builds[0], lockedInChampData.name);
				leagueInterface.setSummonerSpells(lockedInChampData.builds[0]);
			}
		}

		private void secondRuneApplyBTN_Click(object sender, EventArgs e)
		{
			if (lockedInChamp != "None" && lockedInChampData != null)
			{
				leagueInterface.setCurrentPageRune(lockedInChampData.builds[1], lockedInChampData.name);
				leagueInterface.setSummonerSpells(lockedInChampData.builds[1]);
			}
		}

		private void updateRuneTV(TreeView treeView, championBuild championBuild)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<TreeView, championBuild>(updateRuneTV), treeView, championBuild);
			}
			else
			{

				treeView.BeginUpdate();
				treeView.Nodes.Add("Runes");
				treeView.Nodes[0].Nodes.Add(championBuild.primaryRuneTitle);
				foreach (var rune in championBuild.primaryRunes)
				{
					treeView.Nodes[0].Nodes[0].Nodes.Add(rune);
				}

				treeView.Nodes[0].Nodes.Add(championBuild.secondaryRuneTitle);
				foreach (var rune in championBuild.secondaryRunes)
				{
					treeView.Nodes[0].Nodes[1].Nodes.Add(rune);
				}

				treeView.Nodes[0].Nodes.Add("Stat Shards");
				foreach (var shard in championBuild.statShards)
				{
					treeView.Nodes[0].Nodes[2].Nodes.Add(shard);
				}

				treeView.Nodes.Add("Summoner Spells");
				treeView.Nodes[1].Nodes.Add(championBuild.firstSpell);
				treeView.Nodes[1].Nodes.Add(championBuild.secondSpell);


				treeView.EndUpdate();

				treeView.ExpandAll();
			}
		}

		private void clearTreeViews()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(clearTreeViews));
			}
			else
			{
				firstRunesTV.Nodes.Clear();
				secondRunesTV.Nodes.Clear();
			}
		}

		private void updateWinRateLBL(Label winRateLBL, string? winRate)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<Label, string>(updateWinRateLBL), winRateLBL, winRate);
			}
			else
			{
				winRateLBL.Text = "Win Rate: " + winRate;
			}
		}

		private void getRandomSkinBTN_Click(object sender, EventArgs e)
		{
			try {
				leagueInterface.rerollSelectedSkin();
			}
			catch
			{

			}			
		}
	}
}
