using System;

namespace Fjtc.Model.Entity
{
    [Serializable]
    public class CMSRole
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedTime { get; set; }
    }
}
