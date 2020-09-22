using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Roulette.Models
{
    class DAL
    {
        private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
        private static readonly SQLiteConnection db;

        static DAL()
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<SectorColor>();
            db.CreateTable<User>();
        }

        public static void SetSectorColor()
        {
            if (db.Table<SectorColor>().FirstOrDefault() == null)
            {
                IEnumerable<SectorColor> sectors = new List<SectorColor>
            {
                new SectorColor(5, 14, "red"),
                new SectorColor(15, 24, "black"),
                new SectorColor(25, 33, "red"),
                new SectorColor(34, 43, "black"),
                new SectorColor(44, 53, "red"),
                new SectorColor(54, 63, "black"),
                new SectorColor(64, 72, "red"),
                new SectorColor(73, 82, "black"),
                new SectorColor(83, 92, "red"),
                new SectorColor(93, 102, "black"),
                new SectorColor(103, 111, "red"),
                new SectorColor(112, 121, "black"),
                new SectorColor(122, 131, "red"),
                new SectorColor(132, 140, "black"),
                new SectorColor(141, 150, "red"),
                new SectorColor(151, 160, "black"),
                new SectorColor(161, 170, "red"),
                new SectorColor(171, 179, "black"),
                new SectorColor(180, 189, "red"),
                new SectorColor(190, 199, "black"),
                new SectorColor(200, 209, "red"),
                new SectorColor(210, 218, "black"),
                new SectorColor(219, 228, "red"),
                new SectorColor(229, 238, "black"),
                new SectorColor(239, 248, "red"),
                new SectorColor(249, 257, "black"),
                new SectorColor(258, 267, "red"),
                new SectorColor(268, 277, "black"),
                new SectorColor(278, 286, "red"),
                new SectorColor(287, 296, "black"),
                new SectorColor(297, 306, "red"),
                new SectorColor(307, 316, "black"),
                new SectorColor(317, 325, "red"),
                new SectorColor(326, 335, "black"),
                new SectorColor(336, 345, "red"),
                new SectorColor(346, 355, "black"),
            };

                db.InsertAll(sectors);
            }
        }

        public static void SetUser(User user)
        {
            db.Insert(user);
        }

        public static List<SectorColor> GetSelectColorList()
        {
            return db.Table<SectorColor>().ToList();
        }

        public static IEnumerable<User> GetUserScoreList()
        {
            return db.Table<User>().ToList().OrderByDescending(n => n.Credit).Take(3);
        }

        public static void ResetUserTable()
        {
            db.DropTable<User>();
            db.CreateTable<User>();
        }

        public static string FindColor(int position)
        {
            string color = "";

            var test = db.Table<SectorColor>().Where(n => n.StartValue <= position && n.EndValue >= position);
            
            foreach (var item in test)
            {
                color = item.Color;
            }

            return color;
        }
    }
}