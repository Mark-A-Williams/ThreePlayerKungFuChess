using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class MovesService
    {
        // Pointless interface is pointless ¯\_(ツ)_/¯
        private static readonly IDictionary<Colour, int> ColourBaselines = new Dictionary<Colour, int>
            { { Colour.White, 1 }, { Colour.Black, 8 }, { Colour.Red, 12 } };

        public static ICollection<Position> GetValidMoves(Piece piece, Colour colour, Position startingPosition)
        {
            switch (piece)
            {
                case Piece.King:
                    break;
                case Piece.Queen:
                    break;
                case Piece.Rook:
                    return CoordinateHelper.GetLongitudinalPositionsForTransverseCoord(startingPosition.TransversePosition).Select(
                        o => new Position
                        {
                            TransversePosition = startingPosition.TransversePosition,
                            LongitudinalPosition = o
                        }).Concat(
                        CoordinateHelper.GetTransversePositionsForLongitudinalCoord(startingPosition.LongitudinalPosition).Select(
                        o => new Position
                        {
                            TransversePosition = o,
                            LongitudinalPosition = startingPosition.LongitudinalPosition
                        }))
                        .Where(o => o.LongitudinalPosition != startingPosition.LongitudinalPosition || o.TransversePosition != startingPosition.TransversePosition)
                        .ToList();
                case Piece.Knight:
                    break;
                case Piece.Bishop:
                    break;
                case Piece.Pawn:
                    break;
                default:
                    throw new NotSupportedException("That's no piece!");
            }

            throw new Exception();
        }

        /// <summary>
        /// Takes a starting position and the components of a move and returns the new position.
        /// </summary>
        /// <param name="startPosition">The starting position of the piece.</param>
        /// <param name="transverseMove">Negative for left, positive for right.</param>
        /// <param name="longitudinalMove">Negative for backwards, positive for forwards.</param>
        /// <param name="pieceColour">The colour of the piece being moved.</param>
        /// <returns></returns>
        public static Position GetNewPosition(
            Position startPosition,
            int transverseMove,
            int longitudinalMove,
            Colour pieceColour)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the position resulting from a specified transverse (left / right) move from a given position.
        /// </summary>
        /// <param name="position">The starting position.</param>
        /// <param name="distance">The quantity by which to move tranverse. Negative for left, positive for right.</param>
        /// <returns></returns>
        public static Position MoveTransverse(
            Position position,
            int distance)
        {
            if (distance == 0)
            {
                return position;
            }

            var availableTransverseCoords = CoordinateHelper.GetTransversePositionsForLongitudinalCoord(position.LongitudinalPosition);
            var currentTransverseIndex = Array.IndexOf(availableTransverseCoords, position.TransversePosition);

            try
            {
                position.TransversePosition = availableTransverseCoords[currentTransverseIndex + distance];
            }
            catch (IndexOutOfRangeException)
            {
                // If the move would not take the piece to a valid tile, do nothing.
            }

            return position;
        }

        /// <summary>
        /// Get the position resulting from a specified longitudinal (forward / backward) move from a given position.
        /// </summary>
        /// <param name="position">The starting position.</param>
        /// <param name="distance">The quantity by which to move longitudinally. Negative for backward, positive for forward.</param>
        /// <param name="pieceColour">The colour of the piece being moved (affects which way is considered forward).</param>
        /// <returns></returns>
        public static Position MoveLongitudinal(
            Position position,
            int distance,
            Colour pieceColour)
        {
            if (distance == 0)
            {
                return position;
            }

            var orderedLongitudinalCoords = OrderLongitudinalAxisForColour(
                CoordinateHelper.GetLongitudinalPositionsForTransverseCoord(position.TransversePosition),
                pieceColour,
                position);

            var currentLongitudinalIndex = Array.IndexOf(orderedLongitudinalCoords, position.LongitudinalPosition);

            try
            {
                position.LongitudinalPosition = orderedLongitudinalCoords[currentLongitudinalIndex + distance];
            }
            catch (IndexOutOfRangeException)
            {
                // If the move would not take the piece to a valid tile, do nothing.
            }

            return position;
        }

        private static int[] OrderLongitudinalAxisForColour(
            int[] longitudinalCoords,
            Colour colour,
            Position currentPosition)
        {
            // Throughout this method I boldly assume that the provided "axis" (the array of coords) is either
            // in order or in exactly the opposite order.
            var baselineLongitudinalCoord = ColourBaselines[colour];

            if (longitudinalCoords.Contains(baselineLongitudinalCoord))
            {
                // We are in a direct forward path from the start coord for this colour
                if (longitudinalCoords.First() == baselineLongitudinalCoord)
                {
                    return longitudinalCoords;
                }
                else
                {
                    Array.Reverse(longitudinalCoords);
                    return longitudinalCoords;
                }
            }

            if (Array.IndexOf(longitudinalCoords, currentPosition.LongitudinalPosition) < 4)
            {
                Array.Reverse(longitudinalCoords);
            }

            return longitudinalCoords;
        }
    }
}
