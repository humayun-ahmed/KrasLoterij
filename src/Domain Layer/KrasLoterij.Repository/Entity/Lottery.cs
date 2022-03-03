using System;
using NederlandseLoterij.KrasLoterij.Repository.Entity.Base;

namespace NederlandseLoterij.KrasLoterij.Repository.Entity
{
    public class Lottery : EntityBase
    {
        public double? Prize { get; set; }

        public Guid? UserId { get; set; }
    }
}