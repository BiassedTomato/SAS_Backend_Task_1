using System.ComponentModel.DataAnnotations;

namespace SAS_Backend_Task_1.Models
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Attack Helicopter")]
        AttackHelicopter
    }
}
