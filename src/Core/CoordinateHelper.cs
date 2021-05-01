using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class CoordinateHelper
    {
        /// <summary>
        /// Returns the allowable transverse positions (letters) for a given longitudinal one (number).
        /// </summary>
        /// <param name="longitudinalCoord">The longitudinal coordinate (number).</param>
        /// <returns></returns>
        public static char[] GetTransversePositionsForLongitudinalCoord(int longitudinalCoord)
        {
            var outOfRangeMessage = "Longitudinal coordinate must be between 1 and 12.";

            if (longitudinalCoord <= 0)
            {
                throw new ArgumentException(outOfRangeMessage);
            }
            else if (longitudinalCoord <= 4)
            {
                return "ABCDEFGH".ToArray();
            }
            else if (longitudinalCoord <= 8)
            {
                return "LKJIDCBA".ToArray();
            }
            else if (longitudinalCoord <= 12)
            {
                return "HGFEIJKL".ToArray();
            }

            throw new ArgumentException(outOfRangeMessage);
        }

        /// <summary>
        /// Returns the allowable longitudinal positions (numbers) for a given transverse one (letter).
        /// </summary>
        /// <param name="transverseCoord">The transverse coordinate (letter).</param>
        /// <returns></returns>
        public static int[] GetLongitudinalPositionsForTransverseCoord(char transverseCoord)
        {
            if ("ABCD".ToArray().Contains(transverseCoord))
            {
                return Enumerable.Range(1, 8).ToArray();
            }
            else if ("EFGH".ToArray().Contains(transverseCoord))
            {
                // Double Enumerable.Range concatenated just looked ridiculous...
                return new int[] { 1, 2, 3, 4, 9, 10, 11, 12 };
            }
            else if ("IJKL".ToArray().Contains(transverseCoord))
            {
                return new int[] { 8, 7, 6, 5, 9, 10, 11, 12 };
            }

            throw new ArgumentException("Transverse coordinate must be between A and L");
        }
    }
}
