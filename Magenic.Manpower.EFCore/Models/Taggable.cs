using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class Taggable
    {
        public int Id { get; set; }
        public int PrimarySkillid { get; set; }
        public int? RefNoId { get; set; }
    }

    public class TaggableUniqueRefNoComparer : IEqualityComparer<Taggable>
    {
        public bool Equals(Taggable x, Taggable y)
        {
            if (x.RefNoId.HasValue && y.RefNoId.HasValue)
            {
                return x.RefNoId.Value == y.RefNoId.Value;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Taggable obj)
        {
            return obj.GetHashCode();
        }
    }

    public class TaggableUniqueApplicantComparer : IEqualityComparer<Taggable>
    {
        public bool Equals(Taggable x, Taggable y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Taggable obj)
        {
            return obj.GetHashCode();
        }
    }
}
