﻿namespace Proyecto_PAA.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationDbContext : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'ApplicationDbContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'Proyecto_PAA.Models.ApplicationDbContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'ApplicationDbContext'  en el archivo de configuración de la aplicación.
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Prioridad> Prioridades { get; set; }
        public virtual DbSet<Requerimiento> Requerimientos { get; set; }
        public virtual DbSet<Establecimiento> Establecimientos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }




    }

 
}