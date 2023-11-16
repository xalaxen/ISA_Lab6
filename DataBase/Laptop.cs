using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    [Table("Laptop")]
    public class Laptop
    {
        [Key]
        public string Title {  get; set; }
        public string Resolution { get; set; }
        public string CPU { get; set; }
        public int RAM { get; set; }
        public string GPU { get; set; }
        public string SSD { get; set; }
        public int Cost { get; set; }

        public override string ToString()
        {
            return $@"Название: {Title}
Разрешение экрана: {Resolution}
Процессор: {CPU}
Оперативная память: {RAM} ГБ
Видеокарта: {GPU}
Объем SSD: {SSD}
Цена: {Cost} руб
";
        }
    }
}
