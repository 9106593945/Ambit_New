using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ambit.Domain.Common
{
     public enum LoginRole
     {
          SuperAdmin = 0,

          Admin = 1,

          Users = 2,
     }

     public enum SelectedOptionType
     {
          Standard = 0,
          Individual = 1
     }

     public enum PaymentType
     {
          [Display(Name ="Cash")]
          Cash = 1,
          [Display(Name = "Bank Transfer")]
          BankAccount = 2,
          [Display(Name = "GPay")]
          GPay = 3,
          [Display(Name = "Angadiyu")]
          Angadiyu = 4
     }

     public static class StaticConst
     {
          public const string PasswordPolicy = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
     }

     public static class ControllerConst
     {
          public const string Login = "Login";
          public const string Home = "Home";
          public const string Items = "Items";
          public const string Company = "Company";
          public const string Customer = "Customer";
          public const string CustomerLogin = "CustomerLogin";
          public const string Purchase = "Purchase";
          public const string Supplier = "Supplier";
          public const string Invoice = "Invoice";
          public const string Payment = "Payment";
          public const string Category = "Category";
          public const string Reciept = "Reciept";
          public const string Quotation = "Quotation";
          public const string Ledger = "Ledger";
          public const string SupplierLedger = "SupplierLedger";
          public const string Report = "Report";
    }

     public static class MethodConst
     {
          public const string Index = "Index";
          public const string Create = "Create";
          public const string Create_post = "Create_post";
          public const string CustomerItemReport = "CustomerItemReport";
          public const string ProfitLoss = "ProfitLoss";
     }

     public static class ScriveDocumentStatus
     {
          public const string Preparation = "preparation";
          public const string Pending = "pending";
          public const string Closed = "closed";
          public const string Canceled = "canceled";
          public const string Timedout = "timedout";
          public const string Rejected = "rejected";
          public const string DocumentError = "document_error";
     }
}
