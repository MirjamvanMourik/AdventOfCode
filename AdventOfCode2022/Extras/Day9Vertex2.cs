using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day9Vertex2
    {
        public List<Day9Visitor> Visitors { get; }
        public Day9Vertex2 Up { get; set; }
        public Day9Vertex2 Right { get; set; }
        public Day9Vertex2 Left { get; set; }
        public Day9Vertex2 Down { get; set; }
        public bool VisitedByTail = false;

        public Day9Vertex2()
        {
            Visitors = new List<Day9Visitor>();
        }

        public void AddVisitor(Day9Visitor visitor)
        {
            Visitors.Add(visitor);

            if (visitor.TailNumber == 9)
            {
                VisitedByTail = true;
            }
        }

        public Day9Visitor GetAndRemoveVisitor(VisitorKind kind, int tailNumber = 0)
        {
            var visitor = new Day9Visitor();

            if (kind == VisitorKind.Head)
            {
                visitor = Visitors.Where(v => v.Kind == kind).FirstOrDefault();
                Visitors.RemoveAll(v => v.Kind == VisitorKind.Head);
            }
            else
            {
                visitor = Visitors.Where(v => v.Kind == kind && v.TailNumber == tailNumber).FirstOrDefault();
                Visitors.RemoveAll(v => v.Kind == kind && v.TailNumber == tailNumber);
            }

            return visitor;
        }
    }
}
