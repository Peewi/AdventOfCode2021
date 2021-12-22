using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22B
{
	internal class Cube
	{
		public long XMin;
		public long XMax;
		public long YMin;
		public long YMax;
		public long ZMin;
		public long ZMax;
		public Cube(long xmin, long xmax, long ymin, long ymax, long zmin, long zmax)
		{
			XMin = xmin;
			XMax = xmax;
			YMin = ymin;
			YMax = ymax;
			ZMin = zmin;
			ZMax = zmax;
		}
		public List<Cube> Carve(Cube carver)
		{
			List<Cube> retval = new List<Cube>(8);
			if (carver.XMin > XMin)
			{
				retval.Add(new Cube(XMin, carver.XMin - 1, YMin, YMax, ZMin, ZMax));
			}
			if (carver.XMax < XMax)
			{
				retval.Add(new Cube(carver.XMax + 1, XMax, YMin, YMax, ZMin, ZMax));
			}
			if (carver.YMin > YMin)
			{
				retval.Add(new Cube(Math.Max(carver.XMin, XMin), Math.Min(carver.XMax, XMax), YMin, carver.YMin - 1, ZMin, ZMax));
			}
			if (carver.YMax < YMax)
			{
				retval.Add(new Cube(Math.Max(carver.XMin, XMin), Math.Min(carver.XMax, XMax), carver.YMax + 1, YMax, ZMin, ZMax));
			}
			if (carver.ZMin > ZMin)
			{
				retval.Add(new Cube(Math.Max(carver.XMin, XMin), Math.Min(carver.XMax, XMax), Math.Max(carver.YMin, YMin), Math.Min(carver.YMax, YMax), ZMin, carver.ZMin - 1));
			}
			if (carver.ZMax < ZMax)
			{
				retval.Add(new Cube(Math.Max(carver.XMin, XMin), Math.Min(carver.XMax, XMax), Math.Max(carver.YMin, YMin), Math.Min(carver.YMax, YMax), carver.ZMax + 1, ZMax));
			}
			return retval;
		}
		public bool Overlaps(Cube other)
		{
			return other.XMax >= XMin && other.XMin <= XMax
				&& other.YMax >= YMin && other.YMin <= YMax
				&& other.ZMax >= ZMin && other.ZMin <= ZMax;
		}

		public long Area()
		{
			long x = Math.Abs(XMax - XMin) + 1;
			long y = Math.Abs(YMax - YMin) + 1;
			long z = Math.Abs(ZMax - ZMin) + 1;
			return x * y * z;
		}
	}
}
