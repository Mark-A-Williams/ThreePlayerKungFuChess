using Core;
using Core.Models;
using System;
using System.Linq;

namespace PieceMoves.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var transverseCoord = GetTransverseCoordInput();

            var longitudinalCoord = GetLongitudinalCoordInput();

            var position = new Position
            {
                LongitudinalPosition = longitudinalCoord,
                TransversePosition = transverseCoord
            };

            Console.WriteLine($"Your position: {position}");

            Console.WriteLine("How far transverse should the piece move?");
            var transverseMove = int.Parse(Console.ReadLine());

            var newPosition = MovesService.MoveTransverse(position, transverseMove);

            Console.WriteLine($"New position: {newPosition}");
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
    }
}
