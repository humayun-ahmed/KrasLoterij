using System;

namespace NederlandseLoterij.KrasLoterij.Service.Contracts.DTO
{
    public class LotteryDTO
    {
        public long Id { get; set; }

        public double? Prize { get; set; }

        public Guid? UserId { get; set; }
    }
}