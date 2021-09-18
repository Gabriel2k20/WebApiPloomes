using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Models
{
    public partial class escolatesteploomesContext : DbContext
    {
        public escolatesteploomesContext()
        {
        }

        public escolatesteploomesContext(DbContextOptions<escolatesteploomesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<Cep> Ceps { get; set; }
        public virtual DbSet<Disciplina> Disciplinas { get; set; }
        public virtual DbSet<DisciplinasInstituicao> DisciplinasInstituicaos { get; set; }
        public virtual DbSet<HistoricoAluno> HistoricoAlunos { get; set; }
        public virtual DbSet<Instituico> Instituicoes { get; set; }
        public virtual DbSet<Professore> Professores { get; set; }
        public virtual DbSet<Prova> Provas { get; set; }
        public virtual DbSet<RegProva> RegProvas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=WebAPIDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //VALIDACOES ALUNO
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.IdAluno)
                    .HasName("PK_alunos_id_aluno");

                entity.ToTable("alunos");
                entity.Property(e => e.IdAluno).HasColumnName("id_aluno");
                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("cep");

                entity.Property(e => e.Cpf)
                    .IsFixedLength()
                    .IsUnicode(false)
                    .HasColumnName("cpf");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.NrRes).HasColumnName("nr_res");

                entity.HasOne(d => d.CepNavigation)
                    .WithMany(p => p.Alunoes)
                    .HasForeignKey(d => d.Cep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_alunos_cep_ceps_codigo");
            });

            //VALIDACOES CEP
            modelBuilder.Entity<Cep>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_ceps_codigo");

                entity.ToTable("ceps");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bairro");

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("logradouro");

                entity.Property(e => e.Municipio)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("municipio");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("uf");
            });
            //VALIDACOES DISCIPLINA
            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(e => e.IdDisciplina)
                    .HasName("PK_disciplinas_id_disciplina");

                entity.ToTable("disciplinas");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            //VALIDACOES DISCIPLINAS_INSTITUICAO
            modelBuilder.Entity<DisciplinasInstituicao>(entity =>
            {
                entity.HasKey(e => e.IdDisciplinaInsituicao)
                    .HasName("PK_disciplinas_instituicao_id_disciplina_insituicao");

                entity.ToTable("disciplinas_instituicao");

                entity.Property(e => e.IdDisciplinaInsituicao).HasColumnName("id_disciplina_insituicao");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.Property(e => e.IdInstituicao).HasColumnName("id_instituicao");

                entity.HasOne(d => d.IdDisciplinaNavigation)
                    .WithMany(p => p.DisciplinasInstituicaos)
                    .HasForeignKey(d => d.IdDisciplina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_disciplinas_instituicao_id_disciplina_disciplinas_id_disciplina");

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.DisciplinasInstituicaos)
                    .HasForeignKey(d => d.IdInstituicao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_disciplinas_instituicao_id_instituicao_instituicoes_id_instituicao");
            });
            //VALIDACOES HISTORICO_ALUNO
            modelBuilder.Entity<HistoricoAluno>(entity =>
            {
                entity.HasKey(e => e.IdHistorico)
                    .HasName("PK_historico_alunos_id_historico");

                entity.ToTable("historico_alunos");

                entity.Property(e => e.IdHistorico).HasColumnName("id_historico");

                entity.Property(e => e.DtMatricula)
                    .HasColumnType("date")
                    .HasColumnName("dt_matricula");

                entity.Property(e => e.DtRematricula)
                    .HasColumnType("date")
                    .HasColumnName("dt_rematricula");

                entity.Property(e => e.IdAluno).HasColumnName("id_aluno");

                entity.Property(e => e.IdInstituicao).HasColumnName("id_instituicao");

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.HistoricoAlunoes)
                    .HasForeignKey(d => d.IdAluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historico_alunos_id_aluno_alunos_id_aluno");

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.HistoricoAlunoes)
                    .HasForeignKey(d => d.IdInstituicao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historico_alunos_id_instituicao_instituicoes_id_instituicao");
            });

            //VALIDACOES INSTITUICAO
            modelBuilder.Entity<Instituico>(entity =>
            {
                entity.HasKey(e => e.IdInstituicao)
                    .HasName("PK_instituicoes_id_instituicao");

                entity.ToTable("instituicoes");

                entity.Property(e => e.IdInstituicao).HasColumnName("id_instituicao");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("cep");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.NotaCorte).HasColumnName("nota_corte");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("razao_social");

                entity.HasOne(d => d.CepNavigation)
                    .WithMany(p => p.Instituicoes)
                    .HasForeignKey(d => d.Cep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_instituicoes_cep_ceps_codigo");
            });

            //VALIDACOES PROFESSORES
            modelBuilder.Entity<Professore>(entity =>
            {
                entity.HasKey(e => e.IdProfessor)
                    .HasName("PK_professores_id_professor");

                entity.ToTable("professores");

                entity.Property(e => e.IdProfessor).HasColumnName("id_professor");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("cep");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cpf");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.NrRes).HasColumnName("nr_res");
            });

            //VALIDACOES PROVAS
            modelBuilder.Entity<Prova>(entity =>
            {
                entity.HasKey(e => e.IdProva)
                    .HasName("PK_provas_id_prova");

                entity.ToTable("provas");

                entity.Property(e => e.IdProva).HasColumnName("id_prova");

                entity.Property(e => e.DtReferente)
                    .HasColumnType("date")
                    .HasColumnName("dt_referente");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.Property(e => e.IdProfessor).HasColumnName("id_professor");

                entity.HasOne(d => d.IdDisciplinaNavigation)
                    .WithMany(p => p.Provas)
                    .HasForeignKey(d => d.IdDisciplina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_provas_id_disciplina_disciplinas_id_disciplina");

                entity.HasOne(d => d.IdProfessorNavigation)
                    .WithMany(p => p.Provas)
                    .HasForeignKey(d => d.IdProfessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_provas_id_professor_professor_id_professor");
            });

            //VALIDACOES REG_PROVAS
            modelBuilder.Entity<RegProva>(entity =>
            {
                entity.HasKey(e => e.IdRegProva)
                    .HasName("PK_reg_provas_id_reg_prova");

                entity.ToTable("reg_provas");

                entity.Property(e => e.IdRegProva).HasColumnName("id_reg_prova");

                entity.Property(e => e.IdAluno).HasColumnName("id_aluno");

                entity.Property(e => e.IdInstituicao).HasColumnName("id_instituicao");

                entity.Property(e => e.IdProva).HasColumnName("id_prova");

                entity.Property(e => e.Nota).HasColumnName("nota");

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.RegProvas)
                    .HasForeignKey(d => d.IdAluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reg_provas_provas_id_prova");

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.RegProvas)
                    .HasForeignKey(d => d.IdInstituicao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reg_provas_id_instituicao_instituicoes_id_instituicao");

                entity.HasOne(d => d.IdProvaNavigation)
                    .WithMany(p => p.RegProvas)
                    .HasForeignKey(d => d.IdProva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reg_provas_id_aluno_alunos_id_aluno");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
