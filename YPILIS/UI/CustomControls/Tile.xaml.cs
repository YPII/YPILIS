using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ComponentModel;

namespace YellowstonePathology.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(Tile));        

        public event PropertyChangedEventHandler PropertyChanged;        

        private SolidColorBrush m_NotActiveBorderBrush = new SolidColorBrush(Colors.Gray);
        private SolidColorBrush m_ActiveBorderBrush = new SolidColorBrush(Colors.GreenYellow);                

        public Tile()
        {                    
            InitializeComponent();
            this.BorderTile.BorderBrush = this.m_NotActiveBorderBrush;
            this.DataContext = this;            
        }        

        public string Title
        {
            get { return base.GetValue(Tile.TitleProperty) as string; }
            set { base.SetValue(Tile.TitleProperty, value); }
        }                                                     

        private void Tile_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BorderTile.BorderBrush = this.m_ActiveBorderBrush;
        }

        private void Tile_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BorderTile.BorderBrush = this.m_NotActiveBorderBrush;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
