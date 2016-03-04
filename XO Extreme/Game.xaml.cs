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
        private MiniMax CompuPlayer;

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
            CompuPlayer = new MiniMax();
        }

        private void AiAdapter()
        {
            char[,] Table = new char[3,3];
            Table[0, 0] = detectCell(Cell00);
            Table[0, 1] = detectCell(Cell01);
            Table[0, 2] = detectCell(Cell02);
            Table[1, 0] = detectCell(Cell10);
            Table[1, 1] = detectCell(Cell11);
            Table[1, 2] = detectCell(Cell12);
            Table[2, 0] = detectCell(Cell20);
            Table[2, 1] = detectCell(Cell21);
            Table[2, 2] = detectCell(Cell22);
            MiniMax.Player = 'o';
            CompuPlayer.Ai(Table, 'o', 0);
            if (CompuPlayer.move_x == 0)
            {
                if (CompuPlayer.move_y == 0)
                {
                    Cell00.Source = O_Player.Source;
                }
                else if (CompuPlayer.move_y == 1)
                {
                    Cell01.Source = O_Player.Source;
                }
                else
                {
                    Cell02.Source = O_Player.Source;
                }
            }
            else if (CompuPlayer.move_x == 1)
            {
                if (CompuPlayer.move_y == 0)
                {
                    Cell10.Source = O_Player.Source;
                }
                else if (CompuPlayer.move_y == 1)
                {
                    Cell11.Source = O_Player.Source;
                }
                else
                {
                    Cell12.Source = O_Player.Source;
                }
            }
            else
            {
                if (CompuPlayer.move_y == 0)
                {
                    Cell20.Source = O_Player.Source;
                }
                else if (CompuPlayer.move_y == 1)
                {
                    Cell21.Source = O_Player.Source;
                }
                else
                {
                    Cell22.Source = O_Player.Source;
                }
            }
        }

        private char detectCell(Image Cell)
        {
            if (Cell.Source == X_Player.Source) return 'x';
            else if (Cell.Source == O_Player.Source) return 'o';
            else return ' ';
        }

        private void Move_Played(Image Cell)
        {
            if (Cell.Source != X_Player.Source && Cell.Source != O_Player.Source && !GameEnded)
            {
                Win_Box.Text = " ";
                if (MainPage.GameType == 1)
                {
                    bool play = true;
                    while (play)
                    {
                        Spaces--;
                        if (Player_Now == 1)
                        {
                            Cell.Source = X_Player.Source;
                            Checker(X_Player.Source);
                            if (Win_Box.Text != " ") play = false;
                            Player_Now = 2;
                        }
                        else
                        {
                            AiAdapter();
                            Checker(O_Player.Source);
                            if (Win_Box.Text != " ") play = false;
                            Player_Now = 1;
                            play = false;
                        }
                    }
                }
                else
                {
                    Spaces--;
                    if (Player_Now == 1)
                    {
                        Cell.Source = X_Player.Source;
                        Checker(X_Player.Source);
                        Player_Now = 2;
                    }
                    else
                    {
                        Cell.Source = O_Player.Source;
                        Checker(O_Player.Source);
                        Player_Now = 1;
                    }
                }
            }
        }

        private void Checker(ImageSource Source)
        {
            bool Checker;
            Checker = Check(Source);
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
                if (Player_Now == 1)
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

        private void Cell_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Move_Played((Image)sender);
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
            else
            {
                Win_Box.Text = "Player O Start";
                if (MainPage.GameType == 1)
                {
                    List<Image> Cells = new List<Image>(new Image[] { Cell00, Cell01, Cell02, Cell10, Cell11, Cell12, Cell20, Cell21, Cell22 });
                    Random random = new Random();
                    Image Cell = Cells[random.Next(0, 8)];
                    Cell.Source = O_Player.Source;
                    Player_Now = 1;
                    Spaces--;
                }
            }
        }
    }
}
