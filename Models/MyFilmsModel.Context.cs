﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyFilmsEntities : DbContext
    {
        public MyFilmsEntities()
            : base("name=MyFilmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmsActor> FilmsActors { get; set; }
        public virtual DbSet<FilmsCrew> FilmsCrews { get; set; }
        public virtual DbSet<FilmsRating> FilmsRatings { get; set; }
        public virtual DbSet<FilmStatus> FilmStatuses { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; }
        public virtual DbSet<UserFilmStatus> UserFilmStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersType> UsersTypes { get; set; }
        public virtual DbSet<FilmsRatingsCount> FilmsRatingsCounts { get; set; }
        public virtual DbSet<FilmsAVGRating> FilmsAVGRatings { get; set; }
        public virtual DbSet<Top100Films> Top100Films { get; set; }
    }
}
