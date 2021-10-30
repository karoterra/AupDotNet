using System;
using System.IO;
using System.Text;
using Karoterra.AupDotNet;

namespace ReadWriteAup
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("ファイル名を指定してください");
                return 1;
            }
            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"\"{inputPath}\"が見つかりません");
                return 1;
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var aup = new AviUtlProject();
            using (var reader = new BinaryReader(File.OpenRead(inputPath)))
            {
                aup.Read(reader);
            }

            Console.WriteLine($"Edit filename: {aup.EditHandle.EditFilename}");
            Console.WriteLine($"Output filename: {aup.EditHandle.OutputFilename}");
            Console.WriteLine($"Project filename: {aup.EditHandle.ProjectFilename}");
            Console.WriteLine("FilterProjects");
            foreach (var filter in aup.FilterProjects)
            {
                Console.WriteLine($"- {filter.Name}");
            }

            var outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath),
                Path.GetFileNameWithoutExtension(inputPath) + "_parsed.aup");
            using (var writer = new BinaryWriter(File.OpenWrite(outputPath)))
            {
                aup.Write(writer);
            }

            return 0;
        }
    }
}
