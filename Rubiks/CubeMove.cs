using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiksCubeSimulator.Rubiks
{
    /// <summary>
    /// Represents a single rubiks cube movement.
    /// </summary>
    internal class CubeMove
    {
        /// <summary>
        /// Gets the side corresponding to the move.
        /// </summary>
        public CubeSide Side { get; }

        /// <summary>
        /// Gets the move direction.
        /// </summary>
        public Rotation Rotation { get; }

        public int MovesCount { get; }

        public List<CubeMove> listMoves;
        public CubeMove(CubeSide side, Rotation rotation)
        {
            Side = side;
            Rotation = rotation;
            MovesCount = 1;
        }
        public CubeMove(CubeSide side, Rotation rotation, int movementCount)
        {
            Side = side;
            Rotation = rotation;
            MovesCount = movementCount;
        }

        /// <summary>
        /// Creates an instance of CubeMove from a single notation.
        /// </summary>
        /// <exception cref="ArgumentException">Value is not valid notation.</exception>
        public CubeMove(string notation)
        {
            listMoves = null;
            String movement= new string((from t in notation where !char.IsDigit(t) select t).ToArray());
            String movementCount = new string((from t in notation where char.IsDigit(t) select t).ToArray());
            int count = 0;
            MovesCount = 1;
            int.TryParse(movementCount, out count);
            if(count>0) MovesCount = count;
            switch (movement.Trim().ToLower())
            {
                case "f":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Front;
                    break;

                case "fi" :
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Front;
                    break;

                case "b":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Back;
                    break;

                case "bi":
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Back;
                    break;

                case "r":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Right;
                    break;

                case "ri":
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Right;
                    break;

                case "l":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Left;
                    break;

                case "li":
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Left;
                    break;

                case "u":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Up;
                    break;

                case "ui":
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Up;
                    break;

                case "d":
                    Rotation = Rotation.Cw;
                    Side = CubeSide.Down;
                    break;

                case "di":
                    Rotation = Rotation.Ccw;
                    Side = CubeSide.Down;
                    break;
                case "rs":
                    listMoves = new List<CubeMove>();
                    listMoves.Add(new CubeMove("ui"));
                    listMoves.Add(new CubeMove("h"));
                    listMoves.Add(new CubeMove("d"));
                    // Rotation = Rotation.RTurn;
                    // Side = CubeSide.Front;
                    break;
                case "ls":
                    listMoves = new List<CubeMove>();
                    listMoves.Add(new CubeMove("u"));
                    listMoves.Add(new CubeMove("hi"));
                    listMoves.Add(new CubeMove("di"));
                    break;
                case "us":
                    listMoves = new List<CubeMove>();
                    listMoves.Add(new CubeMove("r"));
                    listMoves.Add(new CubeMove("v"));
                    listMoves.Add(new CubeMove("li"));
                    break;
                case "ds":
                    listMoves = new List<CubeMove>();
                    listMoves.Add(new CubeMove("ri"));
                    listMoves.Add(new CubeMove("vi"));
                    listMoves.Add(new CubeMove("l"));
                    break;
                case "h":
                    Rotation = Rotation.MiddleLayerHorizone;
                    Side = CubeSide.Front;
                    break;
                case "hi":
                    Rotation = Rotation.MiddleLayerHorizoneInverse;
                    Side = CubeSide.Front;
                    break;
                case "v":
                    Rotation = Rotation.MiddleLayerVertical;
                    Side = CubeSide.Front;
                    break;
                case "vi":
                    Rotation = Rotation.MiddleLayerVerticalInverse;
                    Side = CubeSide.Front;
                    break;

                default:
                    throw new ArgumentException("Value is not valid notation.", nameof(notation));
            }
        }

        public override string ToString()
        {
            string text = string.Empty;

            switch (Side)
            {
                case CubeSide.Front: text += "Front "; break;
                case CubeSide.Back: text += "Back "; break;
                case CubeSide.Right: text += "Right "; break;
                case CubeSide.Left: text += "Left "; break;
                case CubeSide.Up: text += "Up "; break;
                case CubeSide.Down: text += "Down "; break;
            }

            text += (Rotation == Rotation.Cw) ? "CW" : "CCW";
            text += MovesCount > 1 ? " " + MovesCount + "" : "";
            return text;
        }
    }
}
