﻿using BussinessLayer;
using BussinessLayer.Args;
using BussinessLayer.Managers;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddGameLogWindow.xaml
    /// </summary>
    public partial class AddGameLogWindow : Window
    {
        private GameLogPage _gameLogPage;
        private GameModesManager _modesManager = new GameModesManager();
        public AddGameLogWindow(GameLogPage gameLogPage)
        {
            InitializeComponent();
            _gameLogPage = gameLogPage;

            ModeComboBox.ItemsSource = _modesManager.GetAllEntries();
            MapComboBox.ItemsSource = new MapManager().GetAllEntries();
            AgentComboBox.ItemsSource = new AgentManager().GetAllEntries();
            RankComboBox.ItemsSource = new RankManager().GetAllEntries();
            
            RankComboBox.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamScoreTextBox.Text.Trim() != "" && OpponentScoreTextBox.Text.Trim() != "" && MapComboBox.SelectedIndex >= 0 && AgentComboBox.SelectedIndex >= 0 && ModeComboBox.SelectedIndex >= 0)
            {
                GameLogArgs args = new GameLogArgs(
                    ModeComboBox.SelectedItem,
                    MapComboBox.SelectedItem,
                    AgentComboBox.SelectedItem,
                    int.Parse(TeamScoreTextBox.Text.Trim()),
                    int.Parse(OpponentScoreTextBox.Text.Trim()),
                    KillsTextBox.Text.Trim() == "" ? 0 : int.Parse(KillsTextBox.Text.Trim()),
                    DeathsTextBox.Text.Trim() == "" ? 0 : int.Parse(DeathsTextBox.Text.Trim()),
                    AssistsTextBox.Text.Trim() == "" ? 0 : int.Parse(AssistsTextBox.Text.Trim()),
                    ADRTextBox.Text.Trim() == "" ? 0 : int.Parse(ADRTextBox.Text.Trim()),
                    DateTime.Now,
                    _gameLogPage.GetCurrentSeason(),
                    _modesManager.IsRanked(ModeComboBox.SelectedItem) ? RankComboBox.SelectedItem : null);

                _gameLogPage.GameManager.AddNewEntry(args);

                _gameLogPage.PopulateGames(_gameLogPage.SeasonComboBox.SelectedItem.ToString());
                this.Close();
            }
            else
            {
                if (_modesManager.IsRanked(ModeComboBox.SelectedItem) && RankComboBox.SelectedIndex >= 0)
                {
                    RankLabel.Foreground = Brushes.Red;

                    MessageBox.Show("Please select a rank for this match");
                }
                if (TeamScoreTextBox.Text.Trim() == "")
                    teamScoreLabel.Foreground = Brushes.Red;
                if (OpponentScoreTextBox.Text.Trim() == "")
                    opponentScoreLabel.Foreground = Brushes.Red;
                if (MapComboBox.SelectedIndex < 0)
                    MapLabel.Foreground = Brushes.Red;
                if (AgentComboBox.SelectedIndex < 0)
                    AgentLabel.Foreground = Brushes.Red;
                if (ModeComboBox.SelectedIndex < 0)
                    ModeLabel.Foreground = Brushes.Red;

                MessageBox.Show("Please fill in the required fields");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModeSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_modesManager.IsRanked(ModeComboBox.SelectedItem))
                RankComboBox.IsEnabled = true;
            else
                RankComboBox.IsEnabled = false;
        }
    }
}