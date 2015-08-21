using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace XO_Extreme
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private int X_Counter;
        private int O_Counter;
        private bool GameEnded;
        private int Spaces;
        private int Player_Now;
        public Game()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            X_Counter = 0;
            O_Counter = 0;
            GameEnded = false;
            Spaces = 9;
            Player_Now = 1;
            Win_Box.Text = "Player X Start";
        }

        private void Move_Played(Image Cell)
        {
            bool Checker;
            if (Cell.Source != X_Player.Source && Cell.Source != O_Player.Source && !GameEnded)
            {
                Win_Box.Text = " ";
                Spaces--;
                if (Player_Now == 1)
                {
                    Cell.Source = X_Player.Source;
                    Checker=Check(X_Player.Source);
                    Player_Now = 2;
                }
                else
                {
                    Cell.Source = O_Player.Source;
                    Checker=Check(O_Player.Source);
                    Player_Now = 1;
                }
                if (Spaces == 0)
                {
                    Win_Box.Text = "Draw";
                    GameEnded = true;
                    Play_Again.IsEnabled = true;
                }
                if (Checker)
                {
                    GameEnded = true;
                    Play_Again.IsEnabled = true;
                    if (Player_Now == 2)
                    {
                        Win_Box.Text = "Player X Wins";
                        X_Counter++;
                        X_Score.Text = X_Counter.ToString();
                        Player_Now = 1;
                    }
                    else
                    {
                        Win_Box.Text = "Player O Wins";
                        O_Counter++;
                        O_Score.Text = O_Counter.ToString();
                        Player_Now = 2;
                    }
                }
            }
        }

        private bool Check(ImageSource Piece)
        {
            if (raw(Piece) || col(Piece) || dio(Piece)) return true;
            return false;
        }

        private bool raw(ImageSource Piece)
        {
            if (Cell00.Source == Piece && Cell01.Source == Piece && Cell02.Source == Piece) return true;
            if (Cell10.Source == Piece && Cell11.Source == Piece && Cell12.Source == Piece) return true;
            if (Cell20.Source == Piece && Cell21.Source == Piece && Cell22.Source == Piece) return true;
            return false;
        }

        private bool col(ImageSource Piece)
        {
            if (Cell00.Source == Piece && Cell10.Source == Piece && Cell20.Source == Piece) return true;
            if (Cell01.Source == Piece && Cell11.Source == Piece && Cell21.Source == Piece) return true;
            if (Cell02.Source == Piece && Cell12.Source == Piece && Cell22.Source == Piece) return true;
            return false;
        }

        private bool dio(ImageSource Piece)
        {
            if (Cell00.Source == Piece && Cell11.Source == Piece && Cell22.Source == Piece) return true;
            if (Cell02.Source == Piece && Cell11.Source == Piece && Cell20.Source == Piece) return true;
            return false;
        }

        private void Cell00_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell00);
        }

        private void Cell01_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell01);
        }

        private void Cell10_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell10);
        }

        private void Cell02_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell02);
        }

        private void Cell11_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell11);
        }

        private void Cell12_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell12);
        }

        private void Cell20_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell20);
        }

        private void Cell21_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell21);
        }

        private void Cell22_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played(Cell22);
        }

        private void Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            X_Counter = 0;
            O_Counter = 0;
            X_Score.Text = X_Counter.ToString();
            O_Score.Text = O_Counter.ToString();
        }

        private void Play_Again_Click(object sender, RoutedEventArgs e)
        {
            Cell00.Source = EmptyCell.Source;
            Cell01.Source = EmptyCell.Source;
            Cell02.Source = EmptyCell.Source;
            Cell10.Source = EmptyCell.Source;
            Cell11.Source = EmptyCell.Source;
            Cell12.Source = EmptyCell.Source;
            Cell20.Source = EmptyCell.Source;
            Cell21.Source = EmptyCell.Source;
            Cell22.Source = EmptyCell.Source;
            Play_Again.IsEnabled = false;
            GameEnded = false;
            Spaces = 9;
            if (Player_Now == 1) Win_Box.Text = "Player X Start";
            else Win_Box.Text = "Player O Start";
        }
    }
}
