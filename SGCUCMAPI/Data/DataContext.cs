using Microsoft.EntityFrameworkCore;
using SGCUCMAPI.Models;
using SGCUCMAPI.Models.Historial;

namespace SGCUCMAPI.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Institucion> Instituciones { get; set; }
        public virtual DbSet<UnidadGestora> UnidadesGestoras { get; set; }
        public virtual DbSet<Coordinador> Coordinadores { get; set; }        
        public virtual DbSet<Convenio> Convenios { get; set; }
        public virtual DbSet<RelacionConvenioCoordinador> RelacionesConvenioCoordinador { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<InstitucionHistorial> InstitucionHistoriales { get; set; }
        public virtual DbSet<UnidadGestoraHistorial> UnidadGestoraHistoriales { get; set; }
        public virtual DbSet<CoordinadorHistorial> CoordinadorHistoriales { get; set; }
        public virtual DbSet<ConvenioHistorial> ConvenioHistoriales { get; set; }
        public virtual DbSet<UsuarioHistorial> UsuarioHistoriales { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.HasKey(e => e.IdInstitucion).HasName("PK_INSTITUCION");

                entity.ToTable("INSTITUCION");

                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.Alcance)
                    .HasMaxLength(300)
                    .HasColumnName("ALCANCE");
                entity.Property(e => e.NombreInstitucion)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE_INST");
                entity.Property(e => e.Pais)
                    .HasMaxLength(500)
                    .HasColumnName("PAIS");
                entity.Property(e => e.TipoInstitucion)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_INSTITUCION");
            });

            modelBuilder.Entity<UnidadGestora>(entity =>
            {
                entity.HasKey(e => e.IdUnidadGestora).HasName("PK_UNIDADGESTORA");

                entity.ToTable("UNIDAD_GESTORA");

                entity.Property(e => e.IdUnidadGestora).HasColumnName("ID_UNIDAD_GESTORA");
                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE_UNIDAD");

                entity.HasOne<Institucion>().WithMany()
                    .HasForeignKey(d => d.IdInstitucion)
                    .HasConstraintName("FK_UNIDAD_INSTITUCION");
            });

            modelBuilder.Entity<Coordinador>(entity =>
            {
                entity.HasKey(e => e.IdCoordinador).HasName("PK_COORDINADOR");

                entity.ToTable("COORDINADOR");

                entity.Property(e => e.IdCoordinador).HasColumnName("ID_COORDINADOR");
                entity.Property(e => e.Correo)
                    .HasMaxLength(1000)
                    .HasColumnName("CORREO");
                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE");
                entity.Property(e => e.Tipo)
                    .HasMaxLength(200)
                    .HasColumnName("TIPO");

                entity.HasOne<Institucion>().WithMany()
                    .HasForeignKey(d => d.IdInstitucion)
                    .HasConstraintName("FK_COORDINADOR_INSTITUCION");
            });

            modelBuilder.Entity<Convenio>(entity =>
            {
                entity.HasKey(e => e.IdConvenio).HasName("PK_CONVENIO");

                entity.ToTable("CONVENIO");

                entity.Property(e => e.IdConvenio).HasColumnName("ID_CONVENIO");
                entity.Property(e => e.AnioFirma).HasColumnName("ANO_FIRMA");
                entity.Property(e => e.CondicionRenovacion)
                    .HasMaxLength(100)
                    .HasColumnName("CONDICION_RENOVACION");
                entity.Property(e => e.Cupos).HasColumnName("CUPOS");
                entity.Property(e => e.Documentos)
                    .HasMaxLength(1000)
                    .HasColumnName("DOCUMENTOS");
                entity.Property(e => e.Estatus)
                    .HasMaxLength(500)
                    .HasColumnName("ESTATUS");
                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_INICIO");
                entity.Property(e => e.FechaTermino)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_TERMINO");
                entity.Property(e => e.IdUnidadGestora).HasColumnName("ID_UNIDAD_GESTORA");
                entity.Property(e => e.Movilidad)
                    .HasMaxLength(2)
                    .HasColumnName("MOVILIDAD");
                entity.Property(e => e.NombreConvenio)
                    .HasMaxLength(500)
                    .HasColumnName("NOMBRE_CONV");
                entity.Property(e => e.TipoConvenio)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_CONV");
                entity.Property(e => e.TipoFirma)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_FIRMA");
                entity.Property(e => e.Vigencia)
                    .HasMaxLength(200)
                    .HasColumnName("VIGENCIA");

                entity.HasOne<UnidadGestora>().WithMany()
                    .HasForeignKey(d => d.IdUnidadGestora)
                    .HasConstraintName("FK_CONVENIO_UNIDADGESTORA");
            });

            modelBuilder.Entity<RelacionConvenioCoordinador>(entity =>
            {
                entity.HasKey(e => e.IdRelacion).HasName("PK_RELACION");

                entity.ToTable("RELACION_CONVENIO_COORDINADOR");

                entity.Property(e => e.IdRelacion).HasColumnName("ID_RELACION_CONV_COORD");
                entity.Property(e => e.IdConvenio).HasColumnName("ID_CONVENIO");
                entity.Property(e => e.IdCoordinador).HasColumnName("ID_COORDINADOR");

                entity.HasOne<Convenio>().WithMany()
                    .HasForeignKey(d => d.IdConvenio)
                    .HasConstraintName("FK_RELACION_CONVENIO")
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<Coordinador>().WithMany()
                    .HasForeignKey(d => d.IdCoordinador)
                    .HasConstraintName("FK_RELACION_COORDINADOR")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK_USUARIO");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .HasColumnName("APELLIDO");
                entity.Property(e => e.Contrasena)
                    .HasMaxLength(500)
                    .HasColumnName("CONTRASENA");
                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .HasColumnName("EMAIL");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .HasColumnName("NOMBRE");
                entity.Property(e => e.Privilegios)
                    .HasMaxLength(2)
                    .HasColumnName("PRIVILEGIOS");
                entity.Property(e => e.Vigencia)
                    .HasMaxLength(5)
                    .HasColumnName("VIGENCIA");
            });

            modelBuilder.Entity<InstitucionHistorial>(entity =>
            {
                entity.HasKey(e => e.IdCambioInstitucion).HasName("PK_INSTITUCION_HISTORIAL");

                entity.ToTable("INSTITUCION_HISTORIAL");

                entity.Property(e => e.IdCambioInstitucion).HasColumnName("ID_CAMBIO");
                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCION");
                entity.Property(e => e.Alcance)
                    .HasMaxLength(300)
                    .HasColumnName("ALCANCE");
                entity.Property(e => e.FechaCambio)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CAMBIO");
                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.NombreInstitucion)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE_INST");
                entity.Property(e => e.Pais)
                    .HasMaxLength(500)
                    .HasColumnName("PAIS");
                entity.Property(e => e.TipoInstitucion)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_INSTITUCION");
                entity.Property(e => e.UsuarioCambio)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CAMBIO");
            });

            modelBuilder.Entity<UnidadGestoraHistorial>(entity =>
            {
                entity.HasKey(e => e.IdCambioUnidadGestora).HasName("PK_UNIDAD_HISTORIAL");

                entity.ToTable("UNIDAD_GESTORA_HISTORIAL");

                entity.Property(e => e.IdCambioUnidadGestora).HasColumnName("ID_CAMBIO");
                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCION");
                entity.Property(e => e.FechaCambio)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CAMBIO");
                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.IdUnidadGestora).HasColumnName("ID_UNIDAD_GESTORA");
                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE_UNIDAD");
                entity.Property(e => e.UsuarioCambio)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CAMBIO");
            });

            modelBuilder.Entity<CoordinadorHistorial>(entity =>
            {
                entity.HasKey(e => e.IdCambioCoordinador).HasName("PK_COORDINADOR_HISTORIAL");

                entity.ToTable("COORDINADOR_HISTORIAL");

                entity.Property(e => e.IdCambioCoordinador).HasColumnName("ID_CAMBIO");
                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCION");
                entity.Property(e => e.Correo)
                    .HasMaxLength(1000)
                    .HasColumnName("CORREO");
                entity.Property(e => e.FechaCambio)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CAMBIO");
                entity.Property(e => e.IdCoordinador).HasColumnName("ID_COORDINADOR");
                entity.Property(e => e.IdInstitucion).HasColumnName("ID_INSTITUCION");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(1000)
                    .HasColumnName("NOMBRE");
                entity.Property(e => e.Tipo)
                    .HasMaxLength(200)
                    .HasColumnName("TIPO");
                entity.Property(e => e.UsuarioCambio)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CAMBIO");
            });

            modelBuilder.Entity<ConvenioHistorial>(entity =>
            {
                entity.HasKey(e => e.IdCambioConvenio).HasName("PK_CONVENIO_HISTORIAL");

                entity.ToTable("CONVENIO_HISTORIAL");

                entity.Property(e => e.IdCambioConvenio).HasColumnName("ID_CAMBIO");
                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCION");
                entity.Property(e => e.AnioFirma).HasColumnName("ANO_FIRMA");
                entity.Property(e => e.CondicionRenovacion)
                    .HasMaxLength(100)
                    .HasColumnName("CONDICION_RENOVACION");
                entity.Property(e => e.Cupos).HasColumnName("CUPOS");
                entity.Property(e => e.Documentos)
                    .HasMaxLength(1000)
                    .HasColumnName("DOCUMENTOS");
                entity.Property(e => e.Estatus)
                    .HasMaxLength(500)
                    .HasColumnName("ESTATUS");
                entity.Property(e => e.FechaCambio)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CAMBIO");
                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_INICIO");
                entity.Property(e => e.FechaTermino)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_TERMINO");
                entity.Property(e => e.IdConvenio).HasColumnName("ID_CONVENIO");
                entity.Property(e => e.IdUnidadGestora).HasColumnName("ID_UNIDAD_GESTORA");
                entity.Property(e => e.Movilidad)
                    .HasMaxLength(2)
                    .HasColumnName("MOVILIDAD");
                entity.Property(e => e.NombreConvenio)
                    .HasMaxLength(500)
                    .HasColumnName("NOMBRE_CONV");
                entity.Property(e => e.TipoConvenio)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_CONV");
                entity.Property(e => e.TipoFirma)
                    .HasMaxLength(500)
                    .HasColumnName("TIPO_FIRMA");
                entity.Property(e => e.UsuarioCambio)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CAMBIO");
                entity.Property(e => e.Vigencia)
                    .HasMaxLength(200)
                    .HasColumnName("VIGENCIA");
            });

            modelBuilder.Entity<UsuarioHistorial>(entity =>
            {
                entity.HasKey(e => e.IdCambioUsuario).HasName("PK_USUARIO_HISTORIAL");

                entity.ToTable("USUARIO_HISTORIAL");

                entity.Property(e => e.IdCambioUsuario).HasColumnName("ID_CAMBIO");
                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCION");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .HasColumnName("APELLIDO");
                entity.Property(e => e.Contrasena)
                    .HasMaxLength(500)
                    .HasColumnName("CONTRASENA");
                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .HasColumnName("EMAIL");
                entity.Property(e => e.FechaCambio)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CAMBIO");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .HasColumnName("NOMBRE");
                entity.Property(e => e.Privilegios)
                    .HasMaxLength(2)
                    .HasColumnName("PRIVILEGIOS");
                entity.Property(e => e.UsuarioCambio)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CAMBIO");
                entity.Property(e => e.Vigencia)
                    .HasMaxLength(5)
                    .HasColumnName("VIGENCIA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
