using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class PieceMovesService
    {
        public static ICollection<Position> GetValidMovesForRook(Position startingPosition)
        {
            var longitudinalMoves = CoordinateHelper.GetLongitudinalPositionsForTransverseCoord(startingPosition.TransversePosition).Select(
                o => new Position
                {
                    TransversePosition = startingPosition.TransversePosition,
                    LongitudinalPosition = o
                });

            var transverseMoves = CoordinateHelper.GetTransversePositionsForLongitudinalCoord(startingPosition.LongitudinalPosition).Select(
                o => new Position
                {
                    TransversePosition = o,
                    LongitudinalPosition = startingPosition.LongitudinalPosition
                });

            return longitudinalMoves
                .Concat(transverseMoves)
                .Where(o => o.LongitudinalPosition != startingPosition.LongitudinalPosition ||
                            o.TransversePosition != startingPosition.TransversePosition)
                .ToList();
        }

        public static ICollection<Position> GetValidMovesForBishop(Position startingPosition)
        {
            throw new NotImplementedException();
        }

        public static ICollection<Position> GetValidMovesForQueen(Position startingPosition)
        {
            throw new NotImplementedException();
        }

        public static ICollection<Position> GetValidMovesForKing(Position startingPosition)
        {
            throw new NotImplementedException();
        }

        public static ICollection<Position> GetValidMovesForKnight(Position startingPosition)
        {
            throw new NotImplementedException();
        }

        public static ICollection<Position> GetValidMovesForPawn(Position startingPosition, Colour colour)
        {
            throw new NotImplementedException();
        }
    }
}
