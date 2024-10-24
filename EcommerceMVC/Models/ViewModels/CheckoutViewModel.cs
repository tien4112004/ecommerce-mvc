using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EcommerceMVC.Data.Views.ViewModels
{
    public class CheckoutViewModel
    {
        public CartViewModel Cart { get; set; }
        public decimal GrandTotal { get; set; }
        public List<SelectListItem> Addresses { get; set; }
        public Guid SelectedAddressId { get; set; }
        public string Note { get; set; }
    }
}