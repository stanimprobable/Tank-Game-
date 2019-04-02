using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tank
{
    public class Grid
    {

        private char[,] grid { get; set; }
        public int gridy;
        public int gridx;
        private Random rand = new Random();
        private Panel panel;
        Bitmap brick = new Bitmap("C:\\Users\\stanhe\\source\\repos\\Tank\\Tank\\Picture\\brick-2.jpg");
        Bitmap snake = new Bitmap("C:\\Users\\stanhe\\source\\repos\\Tank\\Tank\\Picture\\Snake.jpg");
        Bitmap burger = new Bitmap("C:\\Users\\stanhe\\source\\repos\\Tank\\Tank\\Picture\\Burger.jpg");
        private int size = 10;

        private Random r = new Random();

        public Grid(Position position)
        {
            grid = new char[position.y, position.x];
            gridy = grid.GetLength(0);
            gridx = grid.GetLength(1);
        }



        public void Print(Graphics graphics) //Print the Grid
        {
            //screen.
            for (var y = 0; y < gridy; y++)
            {

                for (var x = 0; x < gridx; x++)
                {
                    if (grid[y,x]== '*')
                        graphics.DrawImage(brick, y * size, x * size, size, size);

                    if (grid[y, x] == '8')
                        graphics.DrawImage(snake, y * size, x * size, size, size);

                    if (grid[y, x] == 'z')
                        graphics.DrawImage(burger, y * size, x * size, size, size);



                }
            }

        }


        public void Set(Position position, char set)
        {
            grid[position.y, position.x] = set;
        }


        public void AddWall() //Set a frame for the game
        {


            for (int y = 0; y < gridy; y++)
            {

                Set(new Position(y, 0), '*');
                Set(new Position(y, gridx - 1), '*');
            }

            for (int x = 0; x < gridx; x++)
            {

                Set(new Position(0, x), '*');
                Set(new Position(gridy - 1, x), '*');

            }
        }

        public void AddSnake(Sanke snake)
        {
            ///Set(new Position(snake.SankePosition[0].y, snake.SankePosition[0].x), '0');


            foreach (Position position in snake.Get())
            {
                Set(new Position(position.y, position.x), '8');
            }



        }

        public void DrawFood(List<Food> food)  //if there is food , don't generate new one.
        {
            foreach (Food foods in food)
            {
                Set(foods.randomFoodPosition, 'z');
            }

        }


        public void Clear()
        {
            Array.Clear(grid, 0, gridy * gridx);
        }

    }
}