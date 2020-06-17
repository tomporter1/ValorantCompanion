﻿using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentClassesPage.xaml
    /// </summary>
    public partial class AgentClassesPage : Page
    {
        MainPage _mainPage;
        MainWindow _window;

        public AgentClassesPage(MainWindow window, MainPage page)
        {
            InitializeComponent();
            _mainPage = page;
            _window = window;
            PopulateTypes();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {TypesListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    new AgentTypeManager().RemoveAgentType(TypesListBox.SelectedItem);
                    PopulateTypes();
                }
            }
            else
            {
                MessageBox.Show("Select a class to remove first.");
            }
        }

        internal void PopulateTypes()
        {
            TypesListBox.ItemsSource = null;
            TypesListBox.SelectedIndex = -1;
            NameTextBox.Text = "";
            TypesListBox.ItemsSource = new AgentTypeManager().GetAllTypes();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentType addAgentType = new AddAgentType(this);
            addAgentType.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = _mainPage;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                NameTextBox.Text = new AgentTypeManager().GetTypeData(TypesListBox.SelectedItem, AgentTypeManager.Fields.Name);
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                new AgentTypeManager().UpdateAgentType(TypesListBox.SelectedItem, NameTextBox.Text.Trim());
                PopulateTypes();
            }
            else
            {
                MessageBox.Show("Select a class to edit first.");
            }
        }
    }
}