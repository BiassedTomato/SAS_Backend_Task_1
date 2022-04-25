using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1.Models
{
    public class UserModel
    {
        public ulong ID;

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Zа-яА-Я]+[' -]*", ErrorMessage = "Invalid name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Zа-яА-Я]+[' -]*", ErrorMessage = "Invalid name")]

        [Range(2,100)]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required")]
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        float _height;

        [Required(ErrorMessage = "This field is required")]
        [Range(50, 240)]
        public float Height
        {
            get => _height;

            set
            {
                if (value > 240f)
                {
                    _height = 240f;
                }
                else if (value < 50f)
                {
                    _height = 50f;
                }

                else _height = value;

            }
        }

        float _weight;

        [Required(ErrorMessage = "This field is required")]
        [Range(20, 500)]
        public float Weight
        {
            get => _weight;

            set
            {
                if (value > 500)
                {
                    _weight = 500;
                }
                else if (value < 20f)
                {
                    _weight = 20f;
                }

                else _weight = value;

            }
        }

        public float BMI
        {
            get
            {
                var h = Height / 100;

                return Weight / (h * h);
            }
        }

        public bool IsStudent { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Zа-яА-Я]+[' -]*", ErrorMessage = "Invalid country name")]
        public string Country { get; set; }
    }
}
