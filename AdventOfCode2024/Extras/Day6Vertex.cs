namespace AdventOfCode2024.Extras
{
    public class Day6Vertex
    {
        public bool IsStartingPosition = false;
        public bool IsObstacle = false;
        public bool Visited = false;
        public bool IsPossibleObstacle = false;
        public bool IsCurrentPosition = false;


        public bool SavedIsStartingPosition = false;
        public bool SavedIsObstacle = false;
        public bool SavedVisited = false;
        public bool SavedIsPossibleObstacle = false;
        public bool SavedIsCurrentPosition = false;


        public int Row = -1;
        public int Col = -1;


        public Day6Vertex? Up { get; set; }
        public Day6Vertex? Right { get; set; }
        public Day6Vertex? Left { get; set; }
        public Day6Vertex? Down { get; set; }

        public void MarkAsObstacle()
        {
            IsObstacle = true;
        }

        public void RemoveAsObstacle()
        {
            IsObstacle = false;
        }

        public void VisitVertex()
        {
            Visited = true;
        }

        public void Print(bool withPossibleObstacles)
        {
            var character = ".";

            if (IsObstacle)
            {
                character = "#";
            }
            else if (IsCurrentPosition)
            {
                character = "x";
            }

            if (withPossibleObstacles)
            {
                if (IsPossibleObstacle)
                {
                    if (IsStartingPosition)
                    {
                        character = "X";
                    }
                    else
                    {
                        character = "0";
                    }
                }
            }


            Console.Write(character);
        }


        public void Save()
        {
            SavedIsStartingPosition = IsStartingPosition;
            SavedIsObstacle = IsObstacle;
            SavedVisited = Visited;
            SavedIsPossibleObstacle = IsPossibleObstacle;
            SavedIsCurrentPosition = IsCurrentPosition;
        }


        public void Restore()
        {
            // Do the reverse
            IsStartingPosition = SavedIsStartingPosition;
            IsObstacle = SavedIsObstacle;
            Visited = SavedVisited;
            IsPossibleObstacle = SavedIsPossibleObstacle;
            IsCurrentPosition = SavedIsCurrentPosition;
        }
    }
}