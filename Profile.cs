using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBondarenko
{
    public class Profile
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }

        public Profile(string name, string surname, string patronymic, string role)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Role = role;
        }
    }

    public static class ProfileDataManager
    {
        public static Profile LoadProfile()
        {
            // Загрузка данных профиля (например, из базы данных или настроек)
            return new Profile("Имя", "Фамилия", "Отчество", "Роль");
        }
    }

}
