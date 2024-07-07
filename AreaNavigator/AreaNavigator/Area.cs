using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaNavigator
{
    internal class Area
    {
        public string Name;
        public Area? NorthArea;
        public Area? EastArea;
        public Area? SouthArea;
        public Area? WestArea;

        // Default Constructor
        public Area()
        {
            Name = "DEFAULT_AREA";
            NorthArea = null;
            EastArea = null;
            SouthArea = null;
            WestArea = null;
        }

        // Custom Constructor, name only
        public Area(string name)
        {
            Name = name;
            NorthArea = null;
            EastArea = null;
            SouthArea = null;
            WestArea = null;
        }

        // Custom complete constructor
        public Area(string name, Area? northArea, Area? eastArea, Area? southArea, Area? westArea)
        {
            Name = name;
            NorthArea = northArea;
            EastArea = eastArea;
            SouthArea = southArea;
            WestArea = westArea;
        }

        public void SetNeighbors(Area? northArea, Area? eastArea, Area? southArea, Area? westArea)
        {
            NorthArea = northArea;
            EastArea = eastArea;
            SouthArea = southArea;
            WestArea = westArea;
        }

        public Area? GetNeighbor(Directions direction)
        {
            switch (direction)
            {
                case Directions.NORTH:
                    return NorthArea;
                case Directions.EAST:
                    return EastArea;
                case Directions.SOUTH:
                    return SouthArea;
                case Directions.WEST:
                    return WestArea;
            }
            return null;
        }

        public Area? GetNeighbor(string areaName)
        {
            if (NorthArea != null && NorthArea.Name == areaName) return NorthArea;
            if (EastArea != null && EastArea.Name == areaName) return EastArea;
            if (SouthArea != null && SouthArea.Name == areaName) return SouthArea;
            if (WestArea != null && WestArea.Name == areaName) return WestArea;

            return null;
        }
    }
}
