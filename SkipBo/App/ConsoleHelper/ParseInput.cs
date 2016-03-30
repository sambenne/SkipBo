using System;

namespace SkipBo.App
{
    public class ParseInput
    {
        public static Input For(string line)
        {
            if(line == "")
                throw new Exception("No Input!");

            line = line.ToLower();
            var parts = line.Split(' ');
            return new Input
            {
                Action = parts[0],
                MainArg = parts.Length > 1 ? parts[1] : string.Empty,
                SecondaryArg = parts.Length > 2 ? parts[2] : string.Empty
            };
        }
    }
}