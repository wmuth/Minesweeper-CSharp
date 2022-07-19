using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MineSweeper : Form
    {
        //Difficulty set by other form
        public static int difficulty = 0;

        //Size and diffivulty settings
        int width;
        int height;
        int amountOfBombs;

        //Know if we have generated bombs
        bool bombsGenerated = false;

        //Array for all our clickable buttons
        Button[,] buttonArray;

        //Used for timer later
        DateTime start;

        //Makes sure we don't go out of bounds on the left or top
        int start_Y = 90;
        int start_X = 24;

        public MineSweeper()
        {
            InitializeComponent();

            //Set our difficulty
            Form DifficultyForm = new DifficultyForm();
            DifficultyForm.ShowDialog();
            switch (difficulty)
            {
                case 1:
                    width = 9;
                    height = 9;
                    amountOfBombs = 10;
                    break;

                case 2:
                    width = 16;
                    height = 16;
                    amountOfBombs = 40;
                    break;

                case 3:
                    width = 30;
                    height = 16;
                    amountOfBombs = 99;
                    break;
            }

            //Change our program size to fit the number of buttons
            this.Width = width * 22 + start_X * 2 + 15;
            this.Height = height * 22 + 150;
            richTextBox1.Width = this.Width;

            //Change button and label position text to fit the new program size
            button1.Left = this.Width / 2 - button1.Width / 2;
            timer.Left = this.Width / 2 - FlagCount.Width / 2 + 50;
            FlagCount.Left = this.Width / 2 - timer.Width / 2 - 55;
            FlagCount.Text = amountOfBombs.ToString();
            button2.Left = this.Width / 2 - timer.Width / 2 - 100;

            //Create twodimensional array for all our buttons
            buttonArray = new Button[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int u = 0; u < width; u++)
                {
                    buttonArray[u, i] = GenerateButton((u * 22) + start_X, (i * 22) + start_Y);
                }
            }
        }

        //Generates all our buttons with the correct values
        public Button GenerateButton(int x, int y)
        {
            Button btn = new Button();

            btn.Size = new Size(22, 22);
            btn.Location = new Point(x, y);
            btn.Tag = "B";
            btn.Font = new Font(btn.Font, FontStyle.Bold);
            btn.MouseDown += btn_Click;

            Controls.Add(btn);
            return btn;
        }

        //Restart by clicking emoji
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void GenerateBombs(int clickedX, int clickedY)
        {
            Random rand = new Random();

            //Generate correct number of bombs
            for (var i = 0; i < amountOfBombs; i += 1)
            {
                //new x and y values
                int xrand = (rand.Next(start_X, (22 * width) + start_X));
                int yrand = (rand.Next(start_Y, (22 * height) + start_Y));

                //Make sure it isn't already a bomb
                if (GetChildAtPoint(new Point(xrand, yrand)).Tag.ToString().Contains("bomb") == false)
                {
                    //Make sure we don't put a bomb where the player clicked
                    if (GetChildAtPoint(new Point(xrand, yrand)) != GetChildAtPoint(new Point(clickedX, clickedY)))
                    {
                        GetChildAtPoint(new Point(xrand, yrand)).Tag += "bomb";
                    }
                    //Run loop one more time
                    else
                    {
                        i -= 1;
                    }
                }
                //Run loop one more time
                else
                {
                    i -= 1;
                }
            }
            //Make sure we only do this once
            bombsGenerated = true;

            //Start timer and set start time to now, works since this only runs once when game starts
            playtimeTimer.Enabled = true;
            start = DateTime.Now;
        }

        //When one of our big array of buttons is clicked
        private void btn_Click(object sender, MouseEventArgs e)
        {
            //If it was left click
            if (e.Button == MouseButtons.Left)
            {
                if (bombsGenerated == false)
                {
                    GenerateBombs(((Button)sender).Left, ((Button)sender).Top);
                }

                if (((Button)sender).Tag.ToString().Contains("bomb"))
                {
                    Lose();
                }

                //We need to know what we clicked
                int x = ((Button)sender).Left;
                int y = ((Button)sender).Top;
                //Depending on the difficulty the size changes so we need that to not go out of bounds
                int end_X = 22 * width + start_X;
                int end_Y = 22 * height + start_Y;
                //The start values need -1 since top left button starts at (start_X, start_Y), so that is still in bounds
                //And we find buttons by clicking on their top left most pixel, so remove -1 and it won't for 
                //Left most buttons
                FloodFill(x, y, start_X - 1, start_Y - 1, end_X, end_Y);
            }

            //If right click
            else if (e.Button == MouseButtons.Right)
            {
                //If the button already has a flag, remove it and add to count
                if (((Button)sender).Text == "F")
                {
                    ((Button)sender).Text = String.Empty;
                    FlagCount.Text = ((int.Parse(FlagCount.Text)) + 1).ToString();
                }
                else
                {
                    //Remove flag
                    int newFlagCount = (int.Parse(FlagCount.Text)) - 1;

                    //If we are at 0 flags, stay there
                    if (newFlagCount < 0){ }

                    //If we are at 0 flags then we wan't to know if we won
                    else if (newFlagCount == 0)
                    {
                        //Add flag to most recent button and update count
                        ((Button)sender).Text = "F";
                        FlagCount.Text = (newFlagCount).ToString();

                        //Check if we have won;
                        //1. For all our buttons, count every button that has a flag and is a bomb
                        int count = 0;
                        foreach (Button button in buttonArray)
                        {
                            if (button.Text == "F" && button.Tag.ToString().Contains("bomb"))
                            {
                                count += 1;
                            }
                        }

                        //2. If the number of flags on bombs is the same as number of bombs, then we win!
                        if (count == amountOfBombs)
                        {
                            //Stop play timer
                            playtimeTimer.Enabled = false;

                            //Tell player they won and ask them if they want to restart
                            DialogResult result = MessageBox.Show("RESTART?", "YOU WIN!", MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                Application.Restart();
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }

                    //We have flags available and want to add to the button we clicked
                    else
                    {
                        //Add flag to most recent button and update count
                        ((Button)sender).Text = "F";
                        FlagCount.Text = (newFlagCount).ToString();
                    }
                }
            }
        }

        private void Lose()
        {
            //Stop play timer
            playtimeTimer.Enabled = false;

            //Show the player all the bombs now that they've lost
            showAllBombs();

            //Tells the player they lost and asks if they would like to restart
            DialogResult result = MessageBox.Show("RESTART?", "YOU LOSE!", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }
        }

        //This is the main function of our minesweeper game, does all the work for the gameplay of opening blocks
        //It uses a flood fill algorithm to find points and then decides what to do with each point it collects
        //Changing colors to open, counting bombs, placing numbers and so on
        private void FloodFill(int x, int y, int minX, int minY, int maxX, int maxY)
        {
            //Create a stack of buttons and add the clicked button to it
            Stack<Point> buttons = new Stack<Point>();
            buttons.Push(new Point(x, y));

            //We add this tag to all the buttons we have checked so that we don't add a button
            //several times to our stack, that way we speed up the program a lot in comparison
            //to not having this simple tag addition and check
            GetChildAtPoint(new Point(x, y)).Tag += "checked";

            //Run while there are buttons in buttons stack
            while (buttons.Count > 0)
            {
                //Remove top button of the stack and use it as the current button
                Point point = buttons.Pop();

                //Make current button white to 'reveal' it
                GetChildAtPoint(point).BackColor = Color.White;

                //Count bombs around it
                //We create two loops one nested in the other
                //We start at -1 then go to 0 then +1
                //Multiply this by the size of each button, 22
                //Then we step through the loops, the first acting as x and the seond as y
                //So basically we get   -1, -1   -1, 0   -1, 1   as the x,y of our first 3 looped points
                int bombCount = 0;
                for (int i = -1; i < 2; i += 1)
                {
                    for (int j = -1; j < 2; j += 1)
                    {
                        int newX = point.X + (i * 22);
                        int newY = point.Y + (j * 22);

                        //Make sure we don't go out of bounds
                        if (newX < maxX && newX > minX && newY < maxY && newY > minY)
                        {
                            if (GetChildAtPoint(new Point(newX, newY)).Tag.ToString().Contains("bomb"))
                            {
                                bombCount += 1;
                            }
                        }
                    }
                }

                //If there are bombs, write the number on the current button
                if (bombCount > 0)
                {
                    GetChildAtPoint(point).Text = bombCount.ToString();
                }

                //If there are none around it, add all the other blocks around this block to the stack
                else
                {
                    //Same loop as above, same logic. But instead of counting bombs, we add to our stack
                    //and add checked to their tag so we don't add a button twice, which we check for here
                    //before adding a button to the stack
                    for (int i = -1; i < 2; i += 1)
                    {
                        for (int j = -1; j < 2; j += 1)
                        {
                            int newX = point.X + (i * 22);
                            int newY = point.Y + (j * 22);

                            //Make sure we don't go out of bounds
                            if (newX < maxX && newX > minX && newY < maxY && newY > minY)
                            {
                                if (GetChildAtPoint(new Point(newX, newY)).Tag.ToString().Contains("checked") == false)
                                {
                                    buttons.Push(new Point(newX, newY));
                                    GetChildAtPoint(new Point(newX, newY)).Tag += "checked";
                                }
                            }
                        }
                    }
                }
            }
        }

        //Hax button
        private void button2_Click(object sender, EventArgs e)
        {
            showAllBombs();
        }

        private void showAllBombs()
        {
            //If we haven't already generated bombs, generate them
            //If we click hax before any button
            if (bombsGenerated == false)
            {
                //Just send some button, doesn't matter
                GenerateBombs(buttonArray[0, 0].Left, buttonArray[0, 0].Top);
            }

            //For all buttons that are bombs, make them purple
            foreach (Button button in buttonArray)
            {
                if (button.Tag.ToString().Contains("bomb"))
                {
                    button.BackColor = Color.MediumPurple;
                }
            }
        }

        //Every time the timer ticks, we calculate how much time has passed and then we display it niceley
        private void playtimeTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timePlayed = DateTime.Now - start;
            timer.Text = timePlayed.Minutes.ToString().PadLeft(2, '0') + ":" + timePlayed.Seconds.ToString().PadLeft(2, '0');
        }
    }
}