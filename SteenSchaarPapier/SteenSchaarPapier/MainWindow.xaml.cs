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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SteenSchaarPapier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum Keuze
    {
        GeenKeuze,
        Papier,
        Steen,
        Schaar
    }
    
    public partial class MainWindow : Window
    {
        private DispatcherTimer countDownTimer = new DispatcherTimer();

        private Random rndGen = new Random();
        private BitmapImage paper;
        private BitmapImage rock;
        private BitmapImage scissors;

        private Keuze speler = Keuze.GeenKeuze;
        private Keuze computer = Keuze.GeenKeuze;

        private int countDown = 0;

        private int spelerScore = 0;
        private int computerScore = 0;

        public MainWindow()
        {
            InitializeComponent();

            PreloadPictures();

            countDownTimer.Tick += CountDownTimer_Tick;

            InitializeGame();
        }

        private void PreloadPictures()
        {
            paper = new BitmapImage();
            rock = new BitmapImage();
            scissors = new BitmapImage();

            paper.BeginInit();
            paper.UriSource = new Uri(@"img/paper-th.png", UriKind.Relative);
            paper.EndInit();
            rock.BeginInit();
            rock.UriSource = new Uri(@"img/rock-th.png", UriKind.Relative);
            rock.EndInit();
            scissors.BeginInit();
            scissors.UriSource = new Uri(@"img/scissors-th.png", UriKind.Relative);
            scissors.EndInit();
        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            countDown--;
            UpdateCountdown();
            

            if (countDown == 0)
            {
                StopCountdown();
                DisableSpelerButtons();
                battleButton.Visibility = Visibility.Visible;

                if (speler == Keuze.GeenKeuze)
                {
                    countDownLabel.Content = "Te laat!";
                }
                else
                {
                    computer = (Keuze)rndGen.Next(1, 4);

                    UpdateComputerPicture();

                    CheckWinner();
                }
            }
        }

        private void InitializeGame()
        {
            speler = Keuze.GeenKeuze;
            computer = Keuze.GeenKeuze ;

            DisableSpelerButtons();
            UpdateSpelerPicture();
            UpdateComputerPicture();
            StopCountdown();
        }

        private void StartGame()
        {
            InitializeGame();

            battleButton.Visibility = Visibility.Hidden;
            EnableSpelerButtons();

            ResetLayoutForNewGame();

            StartCountdown();
        }

        private void DisableSpelerButtons()
        {
            paperButton.Visibility = Visibility.Hidden;
            rockButton.Visibility = Visibility.Hidden;
            scissorsButton.Visibility = Visibility.Hidden;
        }

        private void EnableSpelerButtons()
        {
            paperButton.Visibility = Visibility.Visible;
            rockButton.Visibility = Visibility.Visible;
            scissorsButton.Visibility = Visibility.Visible;
        }

        private void UpdateSpelerPicture()
        {
            if (speler == Keuze.GeenKeuze)
            {
                spelerImage.Visibility = Visibility.Hidden;
            }
            if (speler == Keuze.Papier)
            {
                spelerImage.Visibility = Visibility.Visible;
                spelerImage.Source = paper;
            }
            else if (speler == Keuze.Steen)
            {
                spelerImage.Visibility = Visibility.Visible;
                spelerImage.Source = rock;
            }
            else if (speler == Keuze.Schaar)
            {
                spelerImage.Visibility = Visibility.Visible;
                spelerImage.Source = scissors;
            }
        }

        private void UpdateComputerPicture()
        {
            if (computer == Keuze.GeenKeuze)
            {
                computerImage.Visibility = Visibility.Hidden;
            }
            if (computer == Keuze.Papier)
            {
                computerImage.Visibility = Visibility.Visible;
                computerImage.Source = paper;
            }
            else if (computer == Keuze.Steen)
            {
                computerImage.Visibility = Visibility.Visible;
                computerImage.Source = rock;
            }
            else if (computer == Keuze.Schaar)
            {
                computerImage.Visibility = Visibility.Visible;
                computerImage.Source = scissors;
            }
        }

        void StartCountdown()
        {
            countDown = 3;
            UpdateCountdown();

            countDownTimer.Interval = new TimeSpan(0, 0, 1);
            countDownTimer.Start();
        }

        void StopCountdown()
        {
            countDown = 0;
            UpdateCountdown();

            countDownTimer.Stop();
        }

        void UpdateCountdown()
        {
            if (countDown == 0)
            {
                countDownLabel.Content = "";
            }
            else
            {
                countDownLabel.Content = countDown.ToString();
            }
        }

        void ResetLayoutForNewGame()
        {
            spelerNaamLabel.Background = Brushes.White;
            computerNaamLabel.Background = Brushes.White;
        }

        void AddSpelerScore()
        {
            spelerScore++;
            spelerPuntenLabel.Content = spelerScore.ToString();
            spelerNaamLabel.Background = Brushes.Green;
        }

        void AddComputerScore()
        {
            computerScore++;
            computerPuntenLabel.Content = computerScore.ToString();
            computerNaamLabel.Background = Brushes.Green;
        }

        void CheckWinner()
        {
            if (speler == computer)
            {
                AddSpelerScore();
                AddComputerScore();
            }
            else
            {
                if (speler == Keuze.Steen)
                {
                    if (computer == Keuze.Schaar)
                    {
                        AddSpelerScore();
                    }
                    else
                    {
                        AddComputerScore();
                    }
                }
                else if (speler == Keuze.Schaar)
                {
                    if (computer == Keuze.Steen)
                    {
                        AddComputerScore();
                    }
                    else
                    {
                        AddSpelerScore();
                    }
                }
                else
                {
                    if (computer == Keuze.Schaar)
                    {
                        AddComputerScore();
                    }
                    else
                    {
                        AddSpelerScore();
                    }
                }
            }
        }

        private void paperButton_Click(object sender, RoutedEventArgs e)
        {
            speler = Keuze.Papier;
            UpdateSpelerPicture();
        }

        private void rockButton_Click(object sender, RoutedEventArgs e)
        {
            speler = Keuze.Steen;
            UpdateSpelerPicture();
        }

        private void scissorsButton_Click(object sender, RoutedEventArgs e)
        {
            speler = Keuze.Schaar;
            UpdateSpelerPicture();
        }

        private void battleButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
