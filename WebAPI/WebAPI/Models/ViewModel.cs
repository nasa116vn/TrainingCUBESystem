using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ViewModel
    {
        //lay danh sach id, cd va ten cua bang store va bang bumon len de hien thi tren select cua giao dien Device
        public IEnumerable<StoreModel> Stores { get; set; }
        public IEnumerable<Bumon> Bumons { get; set; }
    }
}