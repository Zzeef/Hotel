using BLL.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Model
{
    public class ProfileModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        [Required(ErrorMessage = "Имя не указано")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Фамилия не указана")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Отчество не указано")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Дата рождения не указана")]
        public DateTime BithDate { get; set; }
        [Required(ErrorMessage = "Номер паспорта не указан")]
        public int PassportId { get; set; }
    }
}
