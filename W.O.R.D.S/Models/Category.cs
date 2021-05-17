using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Image { get; private set; }
        public static int Count { get; private set; } = 0;

        public Category(string name)
        {
            Id = Count++;
            Name = name;
            Image = "/Images/" + name.ToLower() + ".png";
        }

        public static List<Category> Read()
        {
            string catPath = @"Files/categories.txt";
            string[] catLines = File.ReadAllLines(catPath);

            List<Category> list = new List<Category>();

            foreach (var item in catLines)
            {
                list.Add(new Category(item));
            }

            return list;
        }
    }
}
