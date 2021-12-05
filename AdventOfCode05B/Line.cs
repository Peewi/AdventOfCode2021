using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode05A
{
	internal class Line
	{
		public int X1;
		public int Y1;
		public int X2;
		public int Y2;
		public bool Vertical => X1 == X2;
		public bool Horizontal => Y1 == Y2;
		public Line(int x1, int y1, int x2, int y2)
		{
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
		}
	}
}
