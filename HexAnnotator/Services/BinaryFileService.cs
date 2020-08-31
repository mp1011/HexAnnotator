using HexAnnotator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexAnnotator.Services
{
    public class BinaryFileService
    {

        public IEnumerable<ByteRange> SplitByRepeatingBytes(BinaryFile file, byte delimiter, int minimumDelimiterSize)
        {
            var delimiterBlock = Enumerable.Repeat(delimiter, minimumDelimiterSize).ToArray();

            var foundDelimiter = file.Range.Find(0, delimiterBlock);
            if (foundDelimiter != null)
                yield return file.Range.Extract(0, foundDelimiter.Address - 1);

            while (foundDelimiter != null)
            {
                var nextBlockStart = file.Range.Find(foundDelimiter.Address, b => b.Value != 255);
                if (nextBlockStart == null)
                    break;

                foundDelimiter = file.Range.Find(nextBlockStart.Address, delimiterBlock);
                if (foundDelimiter != null)
                    yield return file.Range.Extract(nextBlockStart.Address, foundDelimiter.Address - 1);
            }
        }
    }
}
