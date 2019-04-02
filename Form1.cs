using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank
{
    public partial class Form1 : Form
    { 
         Grid grid = new Grid(new Position(40, 40));
        Sanke snake;
         List<Food> foods = new List<Food>();
         Random rand = new Random();
        public delegate void ButtonClickEventHandler(Click click);
        public event ButtonClickEventHandler ButtonClick;
       

        //protected override void OnPaintBackground(PaintEventArgs e) { }

        public Form1()
        {
            InitializeComponent();

            snake = new Sanke(grid, this);
            snake.FoodEaten += FoodDestroyoperation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            OnClick(Tank.Click.Left);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            OnClick(Tank.Click.Right);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            OnClick(Tank.Click.Down);

        }

        private void upButton_Click(object sender, EventArgs e)
        {
            OnClick(Tank.Click.Up);
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //screen.Suspend();
            //screen.Text = "";
            // FoodCreationoperation();
            // grid.AddWall();
            // grid.DrawFood(foods);
            // snake.Move(grid, foods);
            // snake.FoodEaten += FoodDestroyoperation;
            //    grid.AddSnake(snake);
            // grid.Print(screen);
            // grid.Clear();
            // screen.Resume();
            grid.Clear();
            FoodCreationoperation();
            grid.AddWall();
            snake.Move(grid, foods);
            grid.AddSnake(snake);
            grid.DrawFood(foods);
            MyPanel.Invalidate();
        }




       

        private void OnClick(Click click)
        {
            ButtonClick?.Invoke(click);
        }

        void FoodCreationoperation()
        {
            while (foods.Count < 4)
            {
                Food food = new Food();
                food.randomFoodPosition.y = rand.Next(1, grid.gridy - 2);
                food.randomFoodPosition.x = rand.Next(1, grid.gridx - 2);
                foods.Add(food);
            }
        }


        void FoodDestroyoperation(Food food)
        {
            foods.Remove(food);
        }

        private void MyPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.CornflowerBlue);
            grid.Print(g);
           
        }
    }
}