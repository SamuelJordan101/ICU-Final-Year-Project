using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Image
    {
        public Image()
        {
        }

        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string Category { get; set; }
        public int? PatientID { get; set; }
    }
}
