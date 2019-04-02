using System;
using System.Collections.Generic;

namespace Tank
{
    public partial class Sanke
    {
            private Position orignialposition;
            private Position nextposition;
            private List<Position> SnakePosition = new List<Position>();
            private ConsoleKey keyInput = new ConsoleKey();

            public delegate void FoodHasEatenEventHandler(Food food);
            public event FoodHasEatenEventHandler FoodEaten;
            private Click clickInput;


            public Sanke(Grid grid,Form1 form1)
            {
                orignialposition.y = (grid.gridy) / 2;
                orignialposition.x = (grid.gridx) / 2;
                SnakePosition.Add(orignialposition);
                form1.ButtonClick += KeyInput;

        }



            public void Move(Grid grid, List<Food> foods)
            {
                // take key input
                nextposition = getNextPosition(SnakePosition[0]);

                bool IsValidMove = isValidMove(grid);
                bool IsSnakeMoving = Ismoving();



                // set position of the snake, if we don't hit a wall

                if (IsValidMove == true)
                {

                    if (IsSnakeMoving == true)
                    {
                        SnakePosition.Insert(0, nextposition);
                        SnakePosition.RemoveAt(SnakePosition.Count - 1);
                    }

                }
                else
                {
                    SnakePosition.Clear();
                    SnakePosition.Add(orignialposition);
                }

                List<Food> foodsCopy = foods.GetRange(0, foods.Count);

                foreach (Food food in foodsCopy)
                {
                    bool ifthereisfood = Isfood(food);

                    if (ifthereisfood == true)
                    {
                        SnakePosition.Insert(0, nextposition);
                        OnFoodEaten(food);
                    }

                }


            }

            public List<Position> Get()
            {
                return SnakePosition;
            }


            private bool isValidMove(Grid grid)
            {
                if (IsWall(grid) == false && IsselfCrash(grid) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }



            private bool IsWall(Grid grid)
            {
                if (nextposition.y < 1 || nextposition.y > grid.gridy - 2 || nextposition.x < 1 ||
                    nextposition.x > grid.gridx - 2)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }


            private bool IsselfCrash(Grid grid)
            {


                if (SnakePosition.GetRange(0, SnakePosition.Count - 1).Contains(nextposition) && Ismoving() == true)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }



            private bool Isfood(Food food)
            {
                if (SnakePosition[0].x == food.randomFoodPosition.x && SnakePosition[0].y == food.randomFoodPosition.y)
                {
                    food.Eaten = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private bool Ismoving()
            {
                if (nextposition.x != SnakePosition[0].x || nextposition.y != SnakePosition[0].y)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

          



            private Position getNextPosition(Position loopPosition)
            {
               
                return NextPosition(loopPosition);
            }

            private Position NextPosition(Position loopPosition)
            {
                switch (clickInput)
                {
                    case Click.Up:

                        return new Position(loopPosition.y, loopPosition.x - 1); 

                    case Click.Down:

                        return new Position(loopPosition.y, loopPosition.x + 1);

                    case Click.Left:

                        return new Position(loopPosition.y - 1, loopPosition.x);

                    case Click.Right:

                        return new Position(loopPosition.y + 1, loopPosition.x); 

                    default:

                        return loopPosition;
                }
            }

            private void OnFoodEaten(Food food)
            {
                FoodEaten?.Invoke(food);

            }

        private void KeyInput(Click click)
        {
            clickInput = click;
        }

    }

    
}