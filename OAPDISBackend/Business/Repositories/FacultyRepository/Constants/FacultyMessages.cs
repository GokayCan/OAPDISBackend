using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.FacultyRepository.Constants
{
    public class FacultyMessages
    {
        public static string Added = "Kayıt işlemi başarılı";
        public static string Updated = "Güncelleme işlemi başarılı";
        public static string Deleted = "Silme işlemi başarılı";
        public static string NotAdded = "Kayıt işlemi başarısız";
        public static string NotUpdated = "Güncelleme işlemi başarısız";
        public static string NotDeleted = "Silme işlemi başarısız";
    }
}