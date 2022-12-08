using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day6
    {
        public static int GetAmountOfCharactersToBeProcessedBeforeMarkerDetected()
        {
            var datastream = Day6Datastream.Input;

            var characterPosition = FindPackagePosition(datastream, PackageType.Marker);

            return characterPosition;
        }

        public static int GetAmountOfCharactersToBeProcessedBeforeMessageDetected()
        {
            var datastream = Day6Datastream.Input;

            var characterPosition = FindPackagePosition(datastream, PackageType.Message);

            return characterPosition;
        }

        private static int FindPackagePosition(string datastream, PackageType type)
        {
            var packageSize = (int)type;

            var lastPackageCharacters = new List<char>();

            for (var i = 0; i < datastream.Length; i++)
            {
                if (lastPackageCharacters.Count != packageSize)
                {
                    lastPackageCharacters.Add(datastream[i]);
                }
                else
                {
                    if (CheckIfPackageFound(lastPackageCharacters))
                    {
                        return i;
                    }

                    lastPackageCharacters = lastPackageCharacters.TakeLast(packageSize - 1).ToList();
                    lastPackageCharacters.Add(datastream[i]);
                }
            }

            return -1;
        }

        private static bool CheckIfPackageFound(List<char> lastPackageCharacters)
        {
            var processedCharacters = new List<string>();

            foreach (var character in lastPackageCharacters)
            {
                if (!processedCharacters.Contains(character.ToString()))
                {
                    processedCharacters.Add(character.ToString());
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private enum PackageType
        {
            Marker = 4,
            Message = 14
        }
    }
}
