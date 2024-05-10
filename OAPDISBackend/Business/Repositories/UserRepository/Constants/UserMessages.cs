namespace Business.Repositories.UserRepository.Contans
{
    public class UserMessages
    {
        public static string AddedUser = "Kullanıcı kaydı başarıyla eklendi";
        public static string UpdatedUser = "Kullanıcı kaydı başarıyla güncellendi";
        public static string DeletedUser = "Kullanıcı kaydı başarıyla silindi";
        public static string WrongCurrentPassword = "Mevcut şifresinizi yanlış girdiniz";
        public static string UserAlreadyConfirm = "Kullanıcı zaten onaylanmış";
        public static string UserConfirmIsSuccesiful = "Kullanıcı maili başarıyla onaylandı";
        public static string AlreadySendForgotPasswordMail = "Şifre unuttum maili 5 dakikada bir gönderilebilir ve süresi geçmemiş bir mail isteği mevcut";
        public static string ForgotPasswordMailSendSuccessiful = "Şifremi unuttum maili başarıyla gönderildi";
        public static string ForgotPasswordValueIsUsed = "Şifre yenileme linki daha önce kullanılmış";
        public static string ForgotPasswordValueTimeIsEnded = "Şifre yenileme linkini süresi dolmuş";
        public static string ConfirmUserMailSendSuccessiful = "Kullanıcı onaylama maili başarıyla gönderildi";
        public static string UserNotFound = "Kullanıcı maili bulunamadı";
        public static string ForgotPasswordValueIsNotValid = "Şifre yenileme linki geçerli değil";

        public static string Listed = " tane kayıt getirildi";
        public static string NotListed = "Listeleme başarısız";

        public static string Added = "Kayıt işlemi başarılı";
        public static string NotAdded = "Kayıt işlemi başarısız";

        public static string Updated = "Güncelleme işlemi başarılı";
        public static string NotUpdated = "Güncelleme işlemi başarısız";

        public static string Deleted = "Silme işlemi başarılı";
        public static string NotDeleted = "Silme işlemi başarısız";

        public static string Founded = "Kayıt getirildi";
        public static string NotFounded = "Kayıt getirilemedi";

        public static string PasswordChanged = "Şifre başarıyla değiştirildi";
        public static string NotChangePassword = "Şifre değiştirilemedi";
    }
}