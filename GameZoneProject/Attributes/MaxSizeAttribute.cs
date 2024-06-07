using System.ComponentModel.DataAnnotations;

namespace GameZoneProject.Attributes
{
    public class MaxSizeAttribute:ValidationAttribute
    {
        private readonly int _MaxSize;
        public MaxSizeAttribute(int MaxSize)
        {
            _MaxSize = MaxSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                if (file.Length>_MaxSize)
                {
                    return new ValidationResult($"Maximum allowed size is {_MaxSize} Bytes");
                }

            }
            return ValidationResult.Success;
        }
    }
}
