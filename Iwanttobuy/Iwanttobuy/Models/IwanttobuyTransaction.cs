//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Iwanttobuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class IwanttobuyTransaction
    {
        public int TransactionID { get; set; }
        public string CustomerName { get; set; }
        public string Line_IG_Name { get; set; }
        public Nullable<int> Necklace { get; set; }
        public Nullable<int> Bracelet { get; set; }
        public Nullable<int> Earring { get; set; }
        public Nullable<int> Hairpin { get; set; }
        public Nullable<int> Ring { get; set; }
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public System.DateTime Datetobuy { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public string ProductState { get; set; }
        public string Remark { get; set; }

        public static IEnumerable<SelectListItem> GetDDLProductState()
        {
            yield return new SelectListItem { Text = "�ѧ������礢ͧ", Value = "�ѧ������礢ͧ" };
            yield return new SelectListItem { Text = "�礢ͧ����", Value = "�礢ͧ����" };
            yield return new SelectListItem { Text = "�觢ͧ����", Value = "�觢ͧ����" };
        }
    }
}
