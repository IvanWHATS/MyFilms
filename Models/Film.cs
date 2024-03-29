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
    
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.FilmsActors = new HashSet<FilmsActor>();
            this.FilmsCrews = new HashSet<FilmsCrew>();
            this.FilmsRatings = new HashSet<FilmsRating>();
            this.Genres = new HashSet<Genre>();
            this.Languages = new HashSet<Language>();
            this.ProductionCompanies = new HashSet<ProductionCompany>();
        }
    
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
    
        public virtual FilmStatus FilmStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmsActor> FilmsActors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmsCrew> FilmsCrews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmsRating> FilmsRatings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Language> Languages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionCompany> ProductionCompanies { get; set; }
    }
}
