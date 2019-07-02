using RubiksCubeSimulator.Rubiks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeSimulator.Algorithms
{
    internal static class DaisyFormation
    {
        public static bool DaisyFormed(this CubeDetails cube)
        {
            return cube.DMM == cube.UTM && cube.DMM == cube.UML && cube.DMM == cube.UMR && cube.DMM == cube.UDM;
        }
    }
}
