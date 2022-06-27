using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constans
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added ";
        public static string ProductNameInvalid = "Product name invalid";
        public static string MaintenanceTime="Sistem Bakimda";
        public static string ProductsListed="Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "Kategori ürün sayısı hatası";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductNameAlreadyExists="Isim daha once alinmis.";
        public static string CategoryLimitExceded = "Kategori limiti aşıldı.";
        public static string AuthorizationDenied="Yetkiniz yok.";
        public static string UserRegistered="Kayıt oldu";
        public static string UserNotFound="Kullanici bulunamadi";
        public static string PasswordError="Sifre Hatali";
        public static string SuccessfulLogin="Giris Basarili";
        public static string UserAlreadyExists="Kullanici mevcut";
        public static string AccessTokenCreated="Erisim tokeni basari ile olustururdu.";
    }
}
