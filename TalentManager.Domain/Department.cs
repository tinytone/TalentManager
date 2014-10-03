﻿using System.ComponentModel.DataAnnotations;

namespace TalentManager.Domain
{
    public class Department : IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------

        public int Id { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string Name { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
