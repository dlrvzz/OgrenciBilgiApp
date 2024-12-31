using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulBilgiApp
{
    internal class Sinif
    {
        public int SinifId { get; set; } //primary key
        public string SinifAd { get; set; } 
        public string Kontenjan { get; set; } //dersin alabileceği öğrenci sayısı

        public ICollection<Ogrenci>? Ogrenciler { get; set; } //sınıfın öğrencilerle ilişkisi
                                                             // sınıfa kayıtlı olan öğrencilerin koleksiyonunu tutar.

    }
}
