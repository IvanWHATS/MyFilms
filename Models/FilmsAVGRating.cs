//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFilms_.NET_Framework_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FilmsAVGRating
    {
        public int film_id { get; set; }
        public string title { get; set; }
        public System.DateTime release_date { get; set; }
        public string original_language { get; set; }
        public string description { get; set; }
        public Nullable<decimal> runtime { get; set; }
        public Nullable<decimal> revenue { get; set; }
        public Nullable<int> status { get; set; }
        public string tagline { get; set; }
        public byte[] poster { get; set; }
        public Nullable<decimal> avg_rating { get; set; }
    }
}
