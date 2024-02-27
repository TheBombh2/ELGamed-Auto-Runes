namespace ELGamedAutoRunes
{
	partial class Main
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			connectionStatusLBL = new Label();
			selectedChampLBL = new Label();
			currentStateLBL = new Label();
			firstRunesTV = new TreeView();
			secondRunesTV = new TreeView();
			firstRuneApplyBTN = new Button();
			secondRuneApplyBTN = new Button();
			switchSpellsCB = new CheckBox();
			champRoleLBL = new Label();
			firstWinRateLBL = new Label();
			secondWinRateLBL = new Label();
			SuspendLayout();
			// 
			// connectionStatusLBL
			// 
			connectionStatusLBL.AutoSize = true;
			connectionStatusLBL.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			connectionStatusLBL.Location = new Point(12, 9);
			connectionStatusLBL.Name = "connectionStatusLBL";
			connectionStatusLBL.Size = new Size(195, 21);
			connectionStatusLBL.TabIndex = 0;
			connectionStatusLBL.Text = "League is not connected";
			// 
			// selectedChampLBL
			// 
			selectedChampLBL.AutoSize = true;
			selectedChampLBL.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			selectedChampLBL.Location = new Point(12, 41);
			selectedChampLBL.Name = "selectedChampLBL";
			selectedChampLBL.Size = new Size(210, 28);
			selectedChampLBL.TabIndex = 5;
			selectedChampLBL.Text = "Selected Champ: None";
			selectedChampLBL.Visible = false;
			// 
			// currentStateLBL
			// 
			currentStateLBL.AutoSize = true;
			currentStateLBL.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			currentStateLBL.Location = new Point(212, 9);
			currentStateLBL.Name = "currentStateLBL";
			currentStateLBL.Size = new Size(53, 21);
			currentStateLBL.TabIndex = 6;
			currentStateLBL.Text = "State:";
			// 
			// firstRunesTV
			// 
			firstRunesTV.Location = new Point(12, 107);
			firstRunesTV.Name = "firstRunesTV";
			firstRunesTV.Size = new Size(200, 330);
			firstRunesTV.TabIndex = 7;
			firstRunesTV.Visible = false;
			// 
			// secondRunesTV
			// 
			secondRunesTV.Location = new Point(230, 107);
			secondRunesTV.Name = "secondRunesTV";
			secondRunesTV.Size = new Size(200, 330);
			secondRunesTV.TabIndex = 8;
			secondRunesTV.Visible = false;
			// 
			// firstRuneApplyBTN
			// 
			firstRuneApplyBTN.Enabled = false;
			firstRuneApplyBTN.Location = new Point(55, 481);
			firstRuneApplyBTN.Name = "firstRuneApplyBTN";
			firstRuneApplyBTN.Size = new Size(75, 23);
			firstRuneApplyBTN.TabIndex = 9;
			firstRuneApplyBTN.Text = "Apply";
			firstRuneApplyBTN.UseVisualStyleBackColor = true;
			firstRuneApplyBTN.Visible = false;
			firstRuneApplyBTN.Click += firstRuneApplyBTN_Click;
			// 
			// secondRuneApplyBTN
			// 
			secondRuneApplyBTN.Enabled = false;
			secondRuneApplyBTN.Location = new Point(295, 481);
			secondRuneApplyBTN.Name = "secondRuneApplyBTN";
			secondRuneApplyBTN.Size = new Size(75, 23);
			secondRuneApplyBTN.TabIndex = 10;
			secondRuneApplyBTN.Text = "Apply";
			secondRuneApplyBTN.UseVisualStyleBackColor = true;
			secondRuneApplyBTN.Visible = false;
			secondRuneApplyBTN.Click += secondRuneApplyBTN_Click;
			// 
			// switchSpellsCB
			// 
			switchSpellsCB.AutoSize = true;
			switchSpellsCB.Location = new Point(171, 485);
			switchSpellsCB.Name = "switchSpellsCB";
			switchSpellsCB.Size = new Size(94, 19);
			switchSpellsCB.TabIndex = 11;
			switchSpellsCB.Text = "Switch Spells";
			switchSpellsCB.UseVisualStyleBackColor = true;
			switchSpellsCB.Visible = false;
			// 
			// champRoleLBL
			// 
			champRoleLBL.AutoSize = true;
			champRoleLBL.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			champRoleLBL.Location = new Point(12, 76);
			champRoleLBL.Name = "champRoleLBL";
			champRoleLBL.Size = new Size(129, 28);
			champRoleLBL.TabIndex = 12;
			champRoleLBL.Text = "Role: No Role";
			champRoleLBL.Visible = false;
			// 
			// firstWinRateLBL
			// 
			firstWinRateLBL.AutoSize = true;
			firstWinRateLBL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			firstWinRateLBL.Location = new Point(12, 440);
			firstWinRateLBL.Name = "firstWinRateLBL";
			firstWinRateLBL.Size = new Size(76, 21);
			firstWinRateLBL.TabIndex = 13;
			firstWinRateLBL.Text = "Win Rate:";
			firstWinRateLBL.Visible = false;
			// 
			// secondWinRateLBL
			// 
			secondWinRateLBL.AutoSize = true;
			secondWinRateLBL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			secondWinRateLBL.Location = new Point(230, 440);
			secondWinRateLBL.Name = "secondWinRateLBL";
			secondWinRateLBL.Size = new Size(76, 21);
			secondWinRateLBL.TabIndex = 14;
			secondWinRateLBL.Text = "Win Rate:";
			secondWinRateLBL.Visible = false;
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(442, 516);
			Controls.Add(secondWinRateLBL);
			Controls.Add(firstWinRateLBL);
			Controls.Add(champRoleLBL);
			Controls.Add(switchSpellsCB);
			Controls.Add(secondRuneApplyBTN);
			Controls.Add(firstRuneApplyBTN);
			Controls.Add(secondRunesTV);
			Controls.Add(firstRunesTV);
			Controls.Add(currentStateLBL);
			Controls.Add(selectedChampLBL);
			Controls.Add(connectionStatusLBL);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "Main";
			Text = "ELGamed Auto Runes";
			Load += Main_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label connectionStatusLBL;
		private Label selectedChampLBL;
		private Label currentStateLBL;
		private TreeView firstRunesTV;
		private TreeView secondRunesTV;
		private Button firstRuneApplyBTN;
		private Button secondRuneApplyBTN;
		public CheckBox switchSpellsCB;
		private Label champRoleLBL;
		private Label firstWinRateLBL;
		private Label secondWinRateLBL;
	}
}
