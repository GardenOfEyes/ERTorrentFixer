using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using SoulsAssetPipeline.Animation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using Assimp.Unmanaged;

string bndFile = args[0];

BND4 bnd = BND4.Read(bndFile);
IEnumerable<BinderFile> flverFiles = bnd.Files.Where(x => x.Name.Contains(".flver"));
foreach (var flverFile in flverFiles.Where(x => x.Bytes.Length > 0))
{
    FLVER2 flver = FLVER2.Read(flverFile.Bytes);
    flverFile.Bytes = flver.Write();
    bnd.Write(bndFile, DCX.Type.DCX_KRAK);
}

bnd.Write(bndFile, DCX.Type.DCX_KRAK);
