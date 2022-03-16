using System.ComponentModel.DataAnnotations;

namespace JobPortal.Model.Validation
{
    class RequiredNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int i;
            return value != null && int.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
