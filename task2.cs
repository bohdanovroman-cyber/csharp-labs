using System;

namespace ClinicApp
{
    public struct WorkSchedule
    {
        public int Start { get; }
        public int End { get; }

        public int HoursPerDay => End - Start;

        public string Display =>
            $"{Start:D2}:00–{End:D2}:00";

        public bool IsNow =>
            Contains(DateTime.Now.Hour);

        public WorkSchedule(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool Contains(int hour)
        {
            return hour >= Start && hour < End;
        }

        public override string ToString()
        {
            return $"{Display} ({HoursPerDay} год)";
        }
    }

    public class Task2Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public WorkSchedule Schedule { get; set; }

        public Task2Doctor(
            int id,
            string firstName,
            string lastName,
            WorkSchedule schedule)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Schedule = schedule;
        }
    }
}