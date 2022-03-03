using System;

namespace NederlandseLoterij.KrasLoterij.Service.Contracts.DTO
{
    public class ScratchCommand
    {
        public long Id { get; set; }

        public Guid? UserId { get; set; }
    }
}