﻿using LocationServices.Locations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DbModel.Location.Authorizations;

namespace LocationServer.Windows
{
    /// <summary>
    /// CardRoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CardRoleWindow : Window
    {
        public CardRoleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ITagRoleService service = new TagRoleService();
            DataGrid1.ItemsSource=service.GetList();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var role = DataGrid1.SelectedItem as CardRole;
            if (role == null) return;
            var win = new AreaAuthorizationWindow(role);
            win.Show();
        }
    }
}