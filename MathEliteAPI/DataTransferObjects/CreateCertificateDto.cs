﻿using MathEliteAPI.Models;

namespace MathEliteAPI.DataTransferObjects
{
    public class CreateCertificateDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
