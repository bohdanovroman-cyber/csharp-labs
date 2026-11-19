using System.Globalization;
using ClinicApp.Models;

namespace ClinicApp.Comparators;

// За алфавітом (українська мова), від А до Я
public class DoctorByNameComparer : IComparer<DoctorStats>
{
    private static readonly CultureInfo Ukrainian = new CultureInfo("uk-UA");

    public int Compare(DoctorStats? x, DoctorStats? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.DoctorName, y.DoctorName, true, Ukrainian);
    }
}