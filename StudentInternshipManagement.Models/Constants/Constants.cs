namespace StudentInternshipManagement.Models.Constants
{
    public static class BundleConstants
    {
        public static readonly string KendoVersion = "2018.2.516";
    }

    public static class AccountConstants
    {
        public static readonly string DefaultPassword = "Ab=123456789";
    }

    public static class InternshipConstants
    {
        public static readonly int GroupsPerTeacher = 6;
        public static readonly int StudentsPerGroups = 5;
    }

    public static class ForgotPasswordConstants
    {
        public static readonly string Error = "Thất bại</br>Tài khoản này chưa có trong hệ thống";
        public static readonly string Success = "Thành công</br>Xin mời xem email để reset mật khẩu";
        public static readonly string CallbackUrl = @"http://sim.hust.edu.vn/Account/ResetPassword/";
    }

    public static class ResetPasswordConstants
    {
        public static readonly string Error = "Thất bại</br>Không thể reset mật khẩu";
        public static readonly string Success = "Reset mật khẩu thành công";
    }

    public static class ChangePasswordConstants
    {
        public static readonly string Error = "Thất bại</br>Không thể đổi mật khẩu";
        public static readonly string Success = "Đổi mật khẩu thành công";
    }
}