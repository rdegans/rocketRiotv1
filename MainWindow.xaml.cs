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
using System.Drawing;

namespace rocketRiotv1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Some problems with restrictions on height
        Rectangle sprite = new Rectangle();
        Rectangle coins = new Rectangle();
        Rectangle background = new Rectangle();
        Rectangle[] rectangles = new Rectangle[10];
        Random rand = new Random();
        ImageBrush spritefill;//Image for the player
        BitmapImage bitmapImage;//Image file to use
        double xValue = 0;
        double yValue = 0;
        double xSpeed = 3;
        double ySpeed = 0;
        double level = 1;
        string lastKey = "";
        int counter = 0;
        bool secondRun = false;
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            //sprite.Fill = Brushes.Black;
            sprite.Height = 100;
            sprite.Width = 100;
            bitmapImage = new BitmapImage(new Uri("spriteFill2.png", UriKind.Relative));
            spritefill = new ImageBrush(bitmapImage);
            sprite.Fill = spritefill;
            canvas.Children.Add(sprite);

            background.Height = 660;
            background.Width = 1300;
            bitmapImage = new BitmapImage(new Uri("background.png", UriKind.Relative));
            spritefill = new ImageBrush(bitmapImage);
            background.Fill = spritefill;
            canvas.Children.Add(background);

            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);

            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i] = new Rectangle();
                /*int horVer = rand.Next(2);
                if (horVer == 1)
                {
                    rectangles[i].Height = 50;
                    rectangles[i].Width = rand.Next(4)*50 + 100;
                }
                else
                {
                    rectangles[i].Width = 50;
                    rectangles[i].Height = rand.Next(3) * 50 + 100;
                }
                rectangles[i].Fill = Brushes.Yellow;
                canvas.Children.Add(rectangles[i]);
                Canvas.SetTop(rectangles[i], rand.Next(600));
                Canvas.SetLeft(rectangles[i], rand.Next(1200));*/
            }
            MessageBox.Show("Resize the screen and exit when ready");
            gameTimer.Start();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            lblOutput.Content = "";
            if (Keyboard.IsKeyDown(Key.Up))
            {
                if (yValue >= 0)
                {
                    if (lastKey == "up")
                    {
                        ySpeed -= 0.15;
                    }
                    else
                    {
                        ySpeed = -1;
                        lastKey = "up";
                    }
                }
                else
                {
                    ySpeed = 1.6;
                }
                lblOutput.Content = "Going up";
                canvas.Children.Remove(sprite);
                counter++;
                if (counter < 5)
                {
                    bitmapImage = new BitmapImage(new Uri("spriteFill.png", UriKind.Relative));
                    spritefill = new ImageBrush(bitmapImage);
                    sprite.Fill = spritefill;
                    canvas.Children.Add(sprite);
                }
                else if (counter < 9)
                {
                    bitmapImage = new BitmapImage(new Uri("spriteFill2.png", UriKind.Relative));
                    spritefill = new ImageBrush(bitmapImage);
                    sprite.Fill = spritefill;
                    canvas.Children.Add(sprite);
                    if (counter == 8)
                    {
                        counter = 0;
                    }
                }
            }
            else
            {
                if (yValue <= 600)
                {
                    ySpeed += 0.8;
                    lblOutput.Content = "Going down";
                }
                else
                {
                    ySpeed = 0;
                }
                lastKey = "";
                canvas.Children.Remove(sprite);
                bitmapImage = new BitmapImage(new Uri("spriteFill3.png", UriKind.Relative));
                spritefill = new ImageBrush(bitmapImage);
                sprite.Fill = spritefill;
                canvas.Children.Add(sprite);
            }
            yValue += ySpeed;
            Canvas.SetTop(sprite, yValue);
            xSpeed = 8 + level * 0.5;
            xValue += xSpeed;
            Canvas.SetLeft(sprite, xValue);
            if (Canvas.GetLeft(sprite) >= 1249)
            {
                secondRun = true;
                xValue = 0;
                Canvas.SetLeft(sprite, xValue);
                level++;

                for (int i = 0; i < rectangles.Length; i++)
                {
                    canvas.Children.Remove(rectangles[i]);
                    int horVer = rand.Next(1);
                    if (horVer == 2)
                    {
                        rectangles[i].Height = 50;
                        rectangles[i].Width = rand.Next(3) * 50 + 100;
                    }
                    else
                    {
                        rectangles[i].Width = 200;
                        rectangles[i].Height = rand.Next(3) * 50 + 100;
                    }
                    bitmapImage = new BitmapImage(new Uri("zapper.png", UriKind.Relative));
                    spritefill = new ImageBrush(bitmapImage);
                    rectangles[i].Fill = spritefill;
                    canvas.Children.Add(rectangles[i]);
                    Canvas.SetTop(rectangles[i], rand.Next(600));
                    Canvas.SetLeft(rectangles[i], rand.Next(600) + 300);
                }
            }
            lblOutput.Content += "Top = " + Canvas.GetTop(sprite) + "Left = " + Canvas.GetLeft(sprite) + "Level = " + level + "Speed = " + xSpeed;
            if (secondRun)
            {
                for (int i = 0; i < rectangles.Length; i++)
                {
                    if (Canvas.GetTop(sprite) < Canvas.GetTop(rectangles[i]) + rectangles[i].ActualHeight && Canvas.GetTop(sprite) > Canvas.GetTop(rectangles[i]) && Canvas.GetLeft(sprite) < Canvas.GetLeft(rectangles[i]) + 25 && Canvas.GetLeft(sprite) > Canvas.GetLeft(rectangles[i]))
                    {
                        gameTimer.Stop();
                    }
                }
            }
        }
    }
}
