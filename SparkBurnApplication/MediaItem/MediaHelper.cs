using IMAPI2.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkBurnApplication.MediaItem
{
    public static class MediaHelper
    {
        public static string GetDriveLetterForRecorder(MsftDiscRecorder2 discRecorder)
        {
            // VolumePathNames is returned as an object array, so we need to cast it to string[]
            object[] volumePaths = (object[])discRecorder.VolumePathNames;

            // Loop through the volume paths, cast each one to string, and return the first valid drive letter
            foreach (object path in volumePaths)
            {
                string volumePath = path as string;
                if (!string.IsNullOrEmpty(volumePath))
                {
                    return volumePath.Substring(0, 1); // Return the drive letter (first character of the path, e.g., "E")
                }
            }
            return null;
        }
    }
}
