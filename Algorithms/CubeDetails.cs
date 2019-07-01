using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeSimulator.Algorithms
{
    internal class CubeDetails
    {
        #region Front
        public Color FTL { get;  }
        public Color FTM { get;  }
        public Color FTR { get;  }
        public Color FML { get;  }
        public Color FMM { get;  }
        public Color FMR { get;  }
        public Color FDL { get;  }
        public Color FDM { get;  }
        public Color FDR { get;  }
        #endregion
       
        #region Left
        public Color LTL { get;  }
        public Color LTM { get;  }
        public Color LTR { get;  }
        public Color LML { get;  }
        public Color LMM { get;  }
        public Color LMR { get;  }
        public Color LDL { get;  }
        public Color LDM { get;  }
        public Color LDR { get;  }
        #endregion
        
        #region Right
        public Color RTL { get;  }
        public Color RTM { get;  }
        public Color RTR { get;  }
        public Color RML { get;  }
        public Color RMM { get;  }
        public Color RMR { get;  }
        public Color RDL { get;  }
        public Color RDM { get;  }
        public Color RDR { get;  }
        #endregion
        
        #region UP
        public Color UTL { get;  }
        public Color UTM { get;  }
        public Color UTR { get;  }
        public Color UML { get;  }
        public Color UMM { get;  }
        public Color UMR { get;  }
        public Color UDL { get;  }
        public Color UDM { get;  }
        public Color UDR { get;  }
        #endregion
        
        #region Down
        public Color DTL { get;  }
        public Color DTM { get;  }
        public Color DTR { get;  }
        public Color DML { get;  }
        public Color DMM { get;  }
        public Color DMR { get;  }
        public Color DDL { get;  }
        public Color DDM { get;  }
        public Color DDR { get;  }
        #endregion
        
        #region Back
        public Color BTL { get;  }
        public Color BTM { get;  }
        public Color BTR { get;  }
        public Color BML { get;  }
        public Color BMM { get;  }
        public Color BMR { get;  }
        public Color BDL { get;  }
        public Color BDM { get;  }
        public Color BDR { get;  }
        #endregion

        public CubeDetails(Rubiks.RubiksCube cube)
        {
            #region Front
            FTL = cube.FrontColors[0, 0];
            FTM = cube.FrontColors[0, 1];
            FTR = cube.FrontColors[0, 2];
            FML = cube.FrontColors[1, 0];
            FMM = cube.FrontColors[1, 1];
            FMR = cube.FrontColors[1, 2];
            FDL = cube.FrontColors[2, 0];
            FDM = cube.FrontColors[2, 1];
            FDR = cube.FrontColors[2, 2];
            #endregion
            #region UP
            UTL = cube.UpColors[0, 0];
            UTM = cube.UpColors[0, 1];
            UTR = cube.UpColors[0, 2];
            UML = cube.UpColors[1, 0];
            UMM = cube.UpColors[1, 1];
            UMR = cube.UpColors[1, 2];
            UDL = cube.UpColors[2, 0];
            UDM = cube.UpColors[2, 1];
            UDR = cube.UpColors[2, 2];
            #endregion
            #region Back
            BTL = cube.BackColors[0, 0];
            BTM = cube.BackColors[0, 1];
            BTR = cube.BackColors[0, 2];
            BML = cube.BackColors[1, 0];
            BMM = cube.BackColors[1, 1];
            BMR = cube.BackColors[1, 2];
            BDL = cube.BackColors[2, 0];
            BDM = cube.BackColors[2, 1];
            BDR = cube.BackColors[2, 2];
            #endregion
            #region Down
            DTL = cube.DownColors[0, 0];
            DTM = cube.DownColors[0, 1];
            DTR = cube.DownColors[0, 2];
            DML = cube.DownColors[1, 0];
            DMM = cube.DownColors[1, 1];
            DMR = cube.DownColors[1, 2];
            DDL = cube.DownColors[2, 0];
            DDM = cube.DownColors[2, 1];
            DDR = cube.DownColors[2, 2];
            #endregion
            #region Left
            LTL = cube.LeftColors[0, 0];
            LTM = cube.LeftColors[0, 1];
            LTR = cube.LeftColors[0, 2];
            LML = cube.LeftColors[1, 0];
            LMM = cube.LeftColors[1, 1];
            LMR = cube.LeftColors[1, 2];
            LDL = cube.LeftColors[2, 0];
            LDM = cube.LeftColors[2, 1];
            LDR = cube.LeftColors[2, 2];
            #endregion
            #region Right
            RTL = cube.RightColors[0, 0];
            RTM = cube.RightColors[0, 1];
            RTR = cube.RightColors[0, 2];
            RML = cube.RightColors[1, 0];
            RMM = cube.RightColors[1, 1];
            RMR = cube.RightColors[1, 2];
            RDL = cube.RightColors[2, 0];
            RDM = cube.RightColors[2, 1];
            RDR = cube.RightColors[2, 2];
            #endregion

        }
    }
}
