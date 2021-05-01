using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class MovesService
    {
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
        /// <param name="startPosition">The starting position.</param>
        /// <param name="transverseMove">The quantity by which to move tranverse. Negative for left, positive for right.</param>
        /// <returns></returns>
        public static Position MoveTransverse(
            Position startPosition,
            int transverseMove)
        {
            if (transverseMove == 0)
            {
                return startPosition;
            }

            var availableTransverseCoords = CoordinateHelper.GetTransversePositionsForLongitudinalCoord(startPosition.LongitudinalPosition);
            var currentTransverseIndex = Array.IndexOf(availableTransverseCoords, startPosition.TransversePosition);

            try
            {
                startPosition.TransversePosition = availableTransverseCoords[currentTransverseIndex + transverseMove];
                return startPosition;
            }
            catch (IndexOutOfRangeException)
            {
                // If the move would not take the piece to a valid tile, do nothing.
                return startPosition;
            }
        }
    }
}
