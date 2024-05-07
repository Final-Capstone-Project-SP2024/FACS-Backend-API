using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddAlarmConfigurationRequest : IValidatableObject
    {
        [Range(0, 100)]
        public decimal Start { get; set; }

        [Range(0, 100)]
        public decimal End { get; set; }

        [JsonIgnore]
        public int AlarmConfigurationId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (AlarmConfigurationId)
            {
                case 1 when Start != 0 && End > 40:
                    yield return new ValidationResult("Must in 0- 40");
                    break;
                case 2 when Start < 40 && End > 60:
                    yield return new ValidationResult("Must in 40-60");
                    break;
                case 3 when Start < 60 && End != 100:
                    yield return new ValidationResult("Must in 60-100");
                    break;


            }
        }
    }
}
