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
        { 
            { Colour.White, 1 }, { Colour.Black, 8 }, { Colour.Red, 12 } 
        };

        /// <summary>
        /// Gets the valid moves from a starting position for a specified piece / colour.
        /// Does not account for the legality of a move or any pieces that may be in the way - this method works in a vacuum.
        /// </summary>
        /// <param name="piece">The piece type e.g. pawn, bishop.</param>
        /// <param name="startingPosition"></param>
        /// <param name="colour">The colour of the piece (only necessary for pawns).</param>
        /// <returns></returns>
        public static ICollection<Position> GetValidMoves(Piece piece, Position startingPosition, Colour? colour = null)
        {
            if (piece == Piece.Pawn && !colour.HasValue)
            {
                throw new ArgumentException("A colour must be provided for pawn moves");
            }

            return piece switch
            {
                Piece.King => PieceMovesService.GetValidMovesForKing(startingPosition),
                Piece.Queen => PieceMovesService.GetValidMovesForQueen(startingPosition),
                Piece.Rook => PieceMovesService.GetValidMovesForRook(startingPosition),
                Piece.Knight => PieceMovesService.GetValidMovesForKnight(startingPosition),
                Piece.Bishop => PieceMovesService.GetValidMovesForKing(startingPosition),
                Piece.Pawn => PieceMovesService.GetValidMovesForPawn(startingPosition, colour.Value),
                _ => throw new NotImplementedException(),
            };
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
