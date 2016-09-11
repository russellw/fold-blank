using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fold_blank
{
    class Program
    {
        static bool isBinary(string path)
        {
            var bytes = File.ReadAllBytes(path);
            foreach (var b in bytes)
                if (b == 0)
                    return true;
            return false;
        }

        static string[] fold(string[] contents)
        {
            var r = new List<string>();
            foreach (var s in contents)
            {
                if (s.Length > 0)
                {
                    r.Add(s);
                    continue;
                }
                if (r.Count == 0)
                    continue;
                if (r.Last().Length == 0)
                    continue;
                r.Add(s);
            }
            while (r.Count > 0 && r.Last().Length == 0)
                r.RemoveAt(r.Count - 1);
            return r.ToArray();
        }

        static void Main(string[] args)
        {
            foreach (var arg in args)
                foreach (var path in Glob.Glob.Expand(arg))
                {
                    if (isBinary(path.ToString()))
                    {
                        Console.WriteLine("{0}: binary file", path);
                        continue;
                    }

                    var contents = File.ReadAllLines(path.ToString());
                    var changed = false;

                    for (int i = 0; i < contents.Length; i++)
                    {
                        var s = contents[i].TrimEnd(null);
                        if (s != contents[i])
                        {
                            contents[i] = s;
                            changed = true;
                        }
                    }

                    var n = contents.Length;
                    contents = fold(contents);
                    if (contents.Length != n)
                        changed = true;

                    if (changed)
                    {
                        Console.WriteLine(path);
                        File.WriteAllLines(path.ToString(), contents);
                    }
                }
        }
    }
}
