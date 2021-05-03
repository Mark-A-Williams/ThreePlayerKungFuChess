using Core;
using Core.Models;
using System;
using System.Linq;

namespace PieceMoves.TestApp
{
    class Program
    {
        static void Main()
        {
            var transverseCoord = GetTransverseCoordInput();

            var longitudinalCoord = GetLongitudinalCoordInput();

            var position = new Position
            {
                LongitudinalPosition = longitudinalCoord,
                TransversePosition = transverseCoord
            };

            Console.WriteLine($"Your position: {position}");

            // MovePieceOnCommand(position);

            Console.WriteLine("Enter a piece type:");
            if (Enum.TryParse(Console.ReadLine(), ignoreCase: true, out Piece pieceType))
            {
                Console.WriteLine("Enter the piece's colour:");
                if (Enum.TryParse(Console.ReadLine(), ignoreCase: true, out Colour colour))
                {
                    var validMoves = MovesService.GetValidMoves(pieceType, position, colour);

                    Console.WriteLine(
                        $"Valid moves for a {colour.ToString().ToLowerInvariant()} {pieceType.ToString().ToLowerInvariant()} from this position are:");

                    foreach (var move in validMoves)
                    {
                        Console.WriteLine(move);
                    }
                }
            }            
        }

        static void MovePieceOnCommand(Position position)
        {
            while (true)
            {
                Console.WriteLine("Type T to move transverse or L to move longitudinally");

                if (char.TryParse(Console.ReadLine(), out char keyPress))
                {
                    if (keyPress == 'T')
                    {
                        MoveTransverseByInput(position);
                    }
                    else if (keyPress == 'L')
                    {
                        MoveLongitudinalByInput(position);
                    }
                }
            }
        }

        static char GetTransverseCoordInput()
        {
            Console.WriteLine("Enter a transverse position (letter between A and L): ");

            var allowedLetters = "ABCDEFGHIJKL".ToCharArray();

            if (char.TryParse(Console.ReadLine(), out char transverseCoord))
            {
                var uppercaseChar = char.ToUpperInvariant(transverseCoord);

                if (allowedLetters.Contains(uppercaseChar))
                {
                    return uppercaseChar;
                }
            }

            return GetTransverseCoordInput();
        }

        static int GetLongitudinalCoordInput()
        {
            Console.WriteLine("Enter a longitudinal position (number between 1 and 12): ");
            
            if (int.TryParse(Console.ReadLine(), out int longitudinalCoord)) 
            {
                if (longitudinalCoord >= 1 && longitudinalCoord <= 12)
                {
                    return longitudinalCoord;
                }
            }

            return GetLongitudinalCoordInput();
        }

        static void MoveTransverseByInput(Position position)
        {
            Console.WriteLine("How far transverse should the piece move?");
            var transverseMove = int.Parse(Console.ReadLine());

            var newPosition = MovesService.MoveTransverse(position, transverseMove);

            Console.WriteLine($"New position: {newPosition}");
        }

        static void MoveLongitudinalByInput(Position position)
        {
            Console.WriteLine("How far longitudinal should the piece move?");
            var longitudinalMove = int.Parse(Console.ReadLine());

            var newPosition = MovesService.MoveLongitudinal(position, longitudinalMove, Colour.White);

            Console.WriteLine($"New position: {newPosition}");
        }
    }
}
