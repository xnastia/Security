using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    abstract class Person
    {
        public static TimeSpan workdaybegins;
        public static TimeSpan workdayends;
        public bool IsPresentLegal(TimeSpan currenttime)
        {
            return currenttime >= workdaybegins && currenttime <= workdayends;

        }

        public static Person GetClassByBadge(int bage)
        {
            switch (bage)
            {

                case 1: return new Visitor();
                case 2: return new Support();
                case 3: return new SecurityOfficer();
                default: return new Intruder();
            }
        }

    }
    class Intruder : Person
    {
        //static Intruder() { workdaybegins = new TimeSpan(0, 0, 0); workdayends = new TimeSpan(0, 0, 0); }

    }
    class Visitor : Person
    {
        static Visitor() { workdaybegins = new TimeSpan(10, 0, 0); workdayends = new TimeSpan(15, 0, 0); }

    }
    class Support : Person
    {
        static Support() { workdaybegins = new TimeSpan(8, 0, 0); workdayends = new TimeSpan(20, 0, 0); }
    }

    class SecurityOfficer : Person
    {
        public SecurityOfficer() { workdaybegins = new TimeSpan(0, 0, 0); workdayends = new TimeSpan(24, 0, 0); }

    }
    class Camera
    {
        public int[] DetectedBages;
        public Camera(int[] db)
        {
            int Length = db.Length;
            DetectedBages = new int[Length];
            for (int i = 0; i < db.Length; i++)
            {
                DetectedBages[i] = db[i];
            }

        }
        public bool IsSafety()
        {
            bool result = true;
            foreach (int db in DetectedBages)
            {
                var person = Person.GetClassByBadge(db);
                result = result &&person.IsPresentLegal(DateTime.Now.TimeOfDay);
            }
            return result;


        }

    }
    class RoomMonitor
    {
        public List<Camera> Cameras;
        public bool IsIntruderInRoom(List<Camera> LocalCameras)
        {

            foreach (Camera c in LocalCameras)
            {
                if (!c.IsSafety())
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RoomMonitor RS = new RoomMonitor();
            RS.Cameras = new List<Camera>() { new Camera(new int[] { 1, 2, 1 } ),
                new Camera( new int[] { 1, 2, 1, 3 }),
                new Camera( new int[] { 1, 2, 1, 3 }),
                new Camera( new int[] { 3, 2, 0, 3 })};
            Console.WriteLine(RS.IsIntruderInRoom(RS.Cameras));
            Console.ReadLine();

        }
    }
}