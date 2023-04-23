using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using Assimp.Unmanaged;

// Get the file path from the command line arguments
string bndFile = args[0];

// Read the BND4 file content
BND4 bnd = BND4.Read(bndFile);

// Filter the files contained in the BND4 file to only include those with a ".flver" extension
IEnumerable<BinderFile> flverFiles = bnd.Files.Where(x => x.Name.Contains(".flver"));

// Iterate through the filtered FLVER files
foreach (var flverFile in flverFiles.Where(x => x.Bytes.Length > 0))
{
    // Read the FLVER2 content from the current file's bytes
    FLVER2 flver = FLVER2.Read(flverFile.Bytes);

    // Write the modified FLVER2 content back to the current file's bytes
    flverFile.Bytes = flver.Write();

    // Save the BND4 file with the modified FLVER2 content
    bnd.Write(bndFile, DCX.Type.DCX_KRAK);
}

