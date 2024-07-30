using System;
using System.Collections.Generic;
using CBEsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CBEsApi.Data;

public partial class CbesManagementContext : DbContext
{
    public CbesManagementContext()
    {
    }

    public CbesManagementContext(DbContextOptions<CbesManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cbe> Cbes { get; set; }

    public virtual DbSet<CbesActivity> CbesActivities { get; set; }

    public virtual DbSet<CbesActivityLog> CbesActivityLogs { get; set; }

    public virtual DbSet<CbesComment> CbesComments { get; set; }

    public virtual DbSet<CbesIndicator> CbesIndicators { get; set; }

    public virtual DbSet<CbesIndicatorLog> CbesIndicatorLogs { get; set; }

    public virtual DbSet<CbesLog> CbesLogs { get; set; }

    public virtual DbSet<CbesLogHeader> CbesLogHeaders { get; set; }

    public virtual DbSet<CbesLogType> CbesLogTypes { get; set; }

    public virtual DbSet<CbesMaturity> CbesMaturities { get; set; }

    public virtual DbSet<CbesMaturityLog> CbesMaturityLogs { get; set; }

    public virtual DbSet<CbesPermission> CbesPermissions { get; set; }

    public virtual DbSet<CbesPlanning> CbesPlannings { get; set; }

    public virtual DbSet<CbesPlanningLog> CbesPlanningLogs { get; set; }

    public virtual DbSet<CbesPlanningLogHeader> CbesPlanningLogHeaders { get; set; }

    public virtual DbSet<CbesPlanningLogType> CbesPlanningLogTypes { get; set; }

    public virtual DbSet<CbesPointOfQuarter> CbesPointOfQuarters { get; set; }

    public virtual DbSet<CbesPointOfQuarterLog> CbesPointOfQuarterLogs { get; set; }

    public virtual DbSet<CbesPosition> CbesPositions { get; set; }

    public virtual DbSet<CbesProcess> CbesProcesses { get; set; }

    public virtual DbSet<CbesProcessLog> CbesProcessLogs { get; set; }

    public virtual DbSet<CbesProcessPlanning> CbesProcessPlannings { get; set; }

    public virtual DbSet<CbesProcessPlanningLog> CbesProcessPlanningLogs { get; set; }

    public virtual DbSet<CbesProcessResult> CbesProcessResults { get; set; }

    public virtual DbSet<CbesProcessResultLog> CbesProcessResultLogs { get; set; }

    public virtual DbSet<CbesProcessTarget> CbesProcessTargets { get; set; }

    public virtual DbSet<CbesProcessTargetLog> CbesProcessTargetLogs { get; set; }

    public virtual DbSet<CbesQuarterlyScore> CbesQuarterlyScores { get; set; }

    public virtual DbSet<CbesQuarterlyScoreLog> CbesQuarterlyScoreLogs { get; set; }

    public virtual DbSet<CbesReportForm> CbesReportForms { get; set; }

    public virtual DbSet<CbesReportFormLog> CbesReportFormLogs { get; set; }

    public virtual DbSet<CbesRole> CbesRoles { get; set; }

    public virtual DbSet<CbesRoleWithPermission> CbesRoleWithPermissions { get; set; }

    public virtual DbSet<CbesTargetResultLogHeader> CbesTargetResultLogHeaders { get; set; }

    public virtual DbSet<CbesTragetResultLogType> CbesTragetResultLogTypes { get; set; }

    public virtual DbSet<CbesUser> CbesUsers { get; set; }

    public virtual DbSet<CbesUserWithRole> CbesUserWithRoles { get; set; }

    public virtual DbSet<CbesWithSubSupervisor> CbesWithSubSupervisors { get; set; }

    public virtual DbSet<CbesWithSubSupervisorLog> CbesWithSubSupervisorLogs { get; set; }

    public virtual DbSet<CbesWithSupervisorLog> CbesWithSupervisorLogs { get; set; }

    public virtual DbSet<CbeswithSupervisor> CbeswithSupervisors { get; set; }

    public virtual DbSet<MaturityWithSupervisor> MaturityWithSupervisors { get; set; }

    public virtual DbSet<MaturityWithSupervisorLog> MaturityWithSupervisorLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NB_JASDAKORN;Initial Catalog=CBEsManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cbe>(entity =>
        {
            entity.ToTable("CBEs");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EngName).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.ThaiName).HasMaxLength(500);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Cbes)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_CBEs_CreateBy");
        });

        modelBuilder.Entity<CbesActivity>(entity =>
        {
            entity.ToTable("CBEsActivity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CbesPlanningId).HasColumnName("CBEsPlanning_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.HeaderId).HasColumnName("Header_id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanning).WithMany(p => p.CbesActivities)
                .HasForeignKey(d => d.CbesPlanningId)
                .HasConstraintName("FK_Table_1_CBEsPlanningID");

            entity.HasOne(d => d.Header).WithMany(p => p.InverseHeader)
                .HasForeignKey(d => d.HeaderId)
                .HasConstraintName("FK_Table_1_HeaderID");
        });

        modelBuilder.Entity<CbesActivityLog>(entity =>
        {
            entity.ToTable("CBEsActivity_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlanningLogId).HasColumnName("CBEsPlanning_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.HeaderId).HasColumnName("Header_id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanningLog).WithMany(p => p.CbesActivityLogs)
                .HasForeignKey(d => d.CbesPlanningLogId)
                .HasConstraintName("FK_CBEsActivity_LOG_CBESPlanning_LOG_id");

            entity.HasOne(d => d.Header).WithMany(p => p.InverseHeader)
                .HasForeignKey(d => d.HeaderId)
                .HasConstraintName("FK_CBEsActivity_LOG_HeaderID");
        });

        modelBuilder.Entity<CbesComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CBEscomment");

            entity.ToTable("CBEsComment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CbesPointOfQuarterId).HasColumnName("CBEsPointOfQuarter_id");
            entity.Property(e => e.HowTo).HasMaxLength(50);
            entity.Property(e => e.Problem).HasMaxLength(50);
            entity.Property(e => e.Summarize).HasMaxLength(50);

            entity.HasOne(d => d.CbesPointOfQuarter).WithMany(p => p.CbesComments)
                .HasForeignKey(d => d.CbesPointOfQuarterId)
                .HasConstraintName("FK_CBEScomment_CBEsPointOfQuarter");
        });

        modelBuilder.Entity<CbesIndicator>(entity =>
        {
            entity.ToTable("CBEsIndicators");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesActivityId).HasColumnName("CBEsActivity_id");
            entity.Property(e => e.CbesPlanningId).HasColumnName("CBEsPlanning_id");
            entity.Property(e => e.CbesProcessId).HasColumnName("CBEsProcess_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsChecked1).HasColumnName("IsChecked_1");
            entity.Property(e => e.IsChecked2).HasColumnName("IsChecked_2");
            entity.Property(e => e.IsChecked3).HasColumnName("IsChecked_3");
            entity.Property(e => e.IsChecked4).HasColumnName("IsChecked_4");
            entity.Property(e => e.IsChecked5).HasColumnName("IsChecked_5");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesActivity).WithMany(p => p.CbesIndicators)
                .HasForeignKey(d => d.CbesActivityId)
                .HasConstraintName("FK_CBEsIndicators_CBEsActivityID");

            entity.HasOne(d => d.CbesPlanning).WithMany(p => p.CbesIndicators)
                .HasForeignKey(d => d.CbesPlanningId)
                .HasConstraintName("FK_CBEsIndicators_CBEsPlanningID");

            entity.HasOne(d => d.CbesProcess).WithMany(p => p.CbesIndicators)
                .HasForeignKey(d => d.CbesProcessId)
                .HasConstraintName("FK_CBEsIndicators_CBEsProcessID");
        });

        modelBuilder.Entity<CbesIndicatorLog>(entity =>
        {
            entity.ToTable("CBEsIndicator_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesActivityLogId).HasColumnName("CBEsActivity_LOG_id");
            entity.Property(e => e.CbesPlannnigLogId).HasColumnName("CBEsPlannnig_LOG_id");
            entity.Property(e => e.CbesProcessPlanningLogId).HasColumnName("CBEsProcessPlanning_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsChecked1).HasColumnName("IsChecked_1");
            entity.Property(e => e.IsChecked2).HasColumnName("IsChecked_2");
            entity.Property(e => e.IsChecked3).HasColumnName("IsChecked_3");
            entity.Property(e => e.IsChecked4).HasColumnName("IsChecked_4");
            entity.Property(e => e.IsChecked5).HasColumnName("IsChecked_5");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesActivityLog).WithMany(p => p.CbesIndicatorLogs)
                .HasForeignKey(d => d.CbesActivityLogId)
                .HasConstraintName("FK_CBEsIndicator_LOG_CBEsActiviity_LOG_id");

            entity.HasOne(d => d.CbesPlannnigLog).WithMany(p => p.CbesIndicatorLogs)
                .HasForeignKey(d => d.CbesPlannnigLogId)
                .HasConstraintName("FK_CBEsIndicator_LOG_CBEsPlanning_LOG");

            entity.HasOne(d => d.CbesProcessPlanningLog).WithMany(p => p.CbesIndicatorLogs)
                .HasForeignKey(d => d.CbesProcessPlanningLogId)
                .HasConstraintName("FK_CBEsIndicator_LOG_CBEsProcessPlanning_LOG_id");
        });

        modelBuilder.Entity<CbesLog>(entity =>
        {
            entity.ToTable("CBEs_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EngName).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.ThaiName).HasMaxLength(2000);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesLogs)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEs_LOG_UpdateByID");
        });

        modelBuilder.Entity<CbesLogHeader>(entity =>
        {
            entity.ToTable("CBEs_LOG_Header");

            entity.HasIndex(e => e.CbesLogId, "IX_CBEs_LOG_Header").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CbesLogId).HasColumnName("CBEs_Log_id");
            entity.Property(e => e.CbesLogTypeId).HasColumnName("CBEs_Log_Type_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesLogHeaders)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEs_LOG_Header_CBEs");

            entity.HasOne(d => d.CbesLog).WithOne(p => p.CbesLogHeader)
                .HasForeignKey<CbesLogHeader>(d => d.CbesLogId)
                .HasConstraintName("FK_CBEs_LOG_Header_CBEs_LOG");

            entity.HasOne(d => d.CbesLogType).WithMany(p => p.CbesLogHeaders)
                .HasForeignKey(d => d.CbesLogTypeId)
                .HasConstraintName("FK_CBEs_Log_Header_Type_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesLogHeaders)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEs_Log_Header_UpdateBy");
        });

        modelBuilder.Entity<CbesLogType>(entity =>
        {
            entity.ToTable("CBEs_LOG_Type");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CbesMaturity>(entity =>
        {
            entity.ToTable("CBEsMaturity");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessId).HasColumnName("CBEsProcess_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(2000);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesProcess).WithMany(p => p.CbesMaturities)
                .HasForeignKey(d => d.CbesProcessId)
                .HasConstraintName("FK_CBEsMaturity_CBEsProcess");
        });

        modelBuilder.Entity<CbesMaturityLog>(entity =>
        {
            entity.ToTable("CBEsMaturity_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessLogId).HasColumnName("CBEsProcess_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(2000);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesProcessLog).WithMany(p => p.CbesMaturityLogs)
                .HasForeignKey(d => d.CbesProcessLogId)
                .HasConstraintName("FK_CBEsMaturity_LOG_CBEsProcess_LOG");
        });

        modelBuilder.Entity<CbesPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Permission");

            entity.ToTable("CBEsPermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesPermissionCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Permission_CreateBy");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesPermissionUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_Permission_UpdateBy");
        });

        modelBuilder.Entity<CbesPlanning>(entity =>
        {
            entity.ToTable("CBEsPlanning");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesPlannings)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_Table_1_CBEsID");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesPlannings)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Table_1_CreateByID");
        });

        modelBuilder.Entity<CbesPlanningLog>(entity =>
        {
            entity.ToTable("CBEsPlanning_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CbesPlanningLogHeaderId).HasColumnName("CBEsPlanning_LOG_Header_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesPlanningLogs)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEsPlanning_LOG_CBEs");

            entity.HasOne(d => d.CbesPlanningLogHeader).WithMany(p => p.CbesPlanningLogs)
                .HasForeignKey(d => d.CbesPlanningLogHeaderId)
                .HasConstraintName("FK_CBEsPlanning_LOG_CBEsPlanning_LOG_Header_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesPlanningLogs)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsPlanning_LOG_UpdateByID");
        });

        modelBuilder.Entity<CbesPlanningLogHeader>(entity =>
        {
            entity.ToTable("CBEsPlanning_LOG_Header");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlanningLogId).HasColumnName("CBEsPlanning_LOG_id");
            entity.Property(e => e.CbesPlanningLogTypeId).HasColumnName("CBEsPlanning_LOG_Type_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanningLog).WithMany(p => p.CbesPlanningLogHeaders)
                .HasForeignKey(d => d.CbesPlanningLogId)
                .HasConstraintName("FK_CBEsPlanning_LOG_Header_CBEsPlanning_LOG_id");

            entity.HasOne(d => d.CbesPlanningLogType).WithMany(p => p.CbesPlanningLogHeaders)
                .HasForeignKey(d => d.CbesPlanningLogTypeId)
                .HasConstraintName("FK_CBEsPlanning_LOG_Header_CBEsPlanning_LOG_Type_id");
        });

        modelBuilder.Entity<CbesPlanningLogType>(entity =>
        {
            entity.ToTable("CBEsPlanning_LOG_Type");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CbesPointOfQuarter>(entity =>
        {
            entity.ToTable("CBEsPointOfQuarter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesQuarterlyScoreId).HasColumnName("CBEsQuarterlyScore_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesQuarterlyScore).WithMany(p => p.CbesPointOfQuarters)
                .HasForeignKey(d => d.CbesQuarterlyScoreId)
                .HasConstraintName("FK_CBESPointOfQuarter_CBEsQuaterlyScoreID");
        });

        modelBuilder.Entity<CbesPointOfQuarterLog>(entity =>
        {
            entity.ToTable("CBEsPointOfQuarter_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesQuarterlyScoreLogId).HasColumnName("CBEsQuarterlyScore_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesQuarterlyScoreLog).WithMany(p => p.CbesPointOfQuarterLogs)
                .HasForeignKey(d => d.CbesQuarterlyScoreLogId)
                .HasConstraintName("FK_CBEsPointOfQuarter_LOG_CBESQuarterlyScore_LOG_id");
        });

        modelBuilder.Entity<CbesPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Position");

            entity.ToTable("CBEsPosition");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesPositionCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_CBEsPosition_CreateBy");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesPositionUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsPosition_UpdateBy");
        });

        modelBuilder.Entity<CbesProcess>(entity =>
        {
            entity.ToTable("CBEsProcess");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.ProcessHeaderId).HasColumnName("ProcessHeader_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesProcesses)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEsProcess_CBEs");

            entity.HasOne(d => d.ProcessHeader).WithMany(p => p.InverseProcessHeader)
                .HasForeignKey(d => d.ProcessHeaderId)
                .HasConstraintName("FK_CBEsProcess_CBEsProcessHeader");
        });

        modelBuilder.Entity<CbesProcessLog>(entity =>
        {
            entity.ToTable("CBEsProcess_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesLogId).HasColumnName("CBEs_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.ProcessLogHeaderId).HasColumnName("Process_Log_Header_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.CbesLog).WithMany(p => p.CbesProcessLogs)
                .HasForeignKey(d => d.CbesLogId)
                .HasConstraintName("FK_CBEsProcess_LOG_CBEs_LOG_id");

            entity.HasOne(d => d.ProcessLogHeader).WithMany(p => p.InverseProcessLogHeader)
                .HasForeignKey(d => d.ProcessLogHeaderId)
                .HasConstraintName("FK_CBEsProcess_LOG_CBEsProcess_LOG");
        });

        modelBuilder.Entity<CbesProcessPlanning>(entity =>
        {
            entity.ToTable("CBEsProcessPlanning");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlanningId).HasColumnName("CBEsPlanning_id");
            entity.Property(e => e.CbesProcessId).HasColumnName("CBEsProcess_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanning).WithMany(p => p.CbesProcessPlannings)
                .HasForeignKey(d => d.CbesPlanningId)
                .HasConstraintName("FK_CBEsProcessPlanning_CBESPlanningID");

            entity.HasOne(d => d.CbesProcess).WithMany(p => p.CbesProcessPlannings)
                .HasForeignKey(d => d.CbesProcessId)
                .HasConstraintName("FK_CBEsProcessPlanning_CBESProcessID");
        });

        modelBuilder.Entity<CbesProcessPlanningLog>(entity =>
        {
            entity.ToTable("CBEsProcessPlanning_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlannnigId).HasColumnName("CBEsPlannnig_id");
            entity.Property(e => e.CbesProcessLogId).HasColumnName("CBEsProcess_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlannnig).WithMany(p => p.CbesProcessPlanningLogs)
                .HasForeignKey(d => d.CbesPlannnigId)
                .HasConstraintName("FK_CBEsProcessPlanning_LOG_CBESPlanning_id");

            entity.HasOne(d => d.CbesProcessLog).WithMany(p => p.CbesProcessPlanningLogs)
                .HasForeignKey(d => d.CbesProcessLogId)
                .HasConstraintName("FK_CBEsProcessPlanning_LOG_CBEsPlanning_LOG");
        });

        modelBuilder.Entity<CbesProcessResult>(entity =>
        {
            entity.ToTable("CBEsProcess_Result");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessId).HasColumnName("CBEs_Process_id");
            entity.Property(e => e.CbesProcessTargetId).HasColumnName("CBEs_ProcessTarget_id");
            entity.Property(e => e.Remark).HasMaxLength(50);

            entity.HasOne(d => d.CbesProcess).WithMany(p => p.CbesProcessResults)
                .HasForeignKey(d => d.CbesProcessId)
                .HasConstraintName("FK_CBEsProcess_Result_CBEsProcess");

            entity.HasOne(d => d.CbesProcessTarget).WithMany(p => p.CbesProcessResults)
                .HasForeignKey(d => d.CbesProcessTargetId)
                .HasConstraintName("FK_CBEsProcess_Result_CBEsProcess_Target");
        });

        modelBuilder.Entity<CbesProcessResultLog>(entity =>
        {
            entity.ToTable("CBEsProcess_Result_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessLogId).HasColumnName("CBEsProcess_LOG_id");
            entity.Property(e => e.CbesProcessTargetLogId).HasColumnName("CBEsProcess_Target_LOG_id");
            entity.Property(e => e.CbesTargetResultLogHeaderId).HasColumnName("CBEsTargetResult_LOG_Header_id");
            entity.Property(e => e.Remark).HasMaxLength(50);

            entity.HasOne(d => d.CbesProcessLog).WithMany(p => p.CbesProcessResultLogs)
                .HasForeignKey(d => d.CbesProcessLogId)
                .HasConstraintName("FK_CBEsProcess_Result_LOG_CBEsProcess_LOG_id");

            entity.HasOne(d => d.CbesProcessTargetLog).WithMany(p => p.CbesProcessResultLogs)
                .HasForeignKey(d => d.CbesProcessTargetLogId)
                .HasConstraintName("FK_CBEsProcess_Result_LOG_CBEsProcess_Target_LOG_id");

            entity.HasOne(d => d.CbesTargetResultLogHeader).WithMany(p => p.CbesProcessResultLogs)
                .HasForeignKey(d => d.CbesTargetResultLogHeaderId)
                .HasConstraintName("FK_CBEsProcess_Result_LOG_CBEsTargetResult_LOG_Header_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesProcessResultLogs)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsProcess_Result_LOG_UpdateByID");
        });

        modelBuilder.Entity<CbesProcessTarget>(entity =>
        {
            entity.ToTable("CBEsProcess_Target");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessId).HasColumnName("CBEsProcess_id");

            entity.HasOne(d => d.CbesProcess).WithMany(p => p.CbesProcessTargets)
                .HasForeignKey(d => d.CbesProcessId)
                .HasConstraintName("FK_CBEsProcessTarget_CBEsProcessID");
        });

        modelBuilder.Entity<CbesProcessTargetLog>(entity =>
        {
            entity.ToTable("CBEsProcess_Target_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesProcessLogId).HasColumnName("CBEsProcess_LOG_id");
            entity.Property(e => e.CbesTargetResultLogHeaderId).HasColumnName("CBEsTargetResult_LOG_Header_id");

            entity.HasOne(d => d.CbesProcessLog).WithMany(p => p.CbesProcessTargetLogs)
                .HasForeignKey(d => d.CbesProcessLogId)
                .HasConstraintName("FK_CBEsProcess_Target_LOG_CBEsProcess_LOG_id");

            entity.HasOne(d => d.CbesTargetResultLogHeader).WithMany(p => p.CbesProcessTargetLogs)
                .HasForeignKey(d => d.CbesTargetResultLogHeaderId)
                .HasConstraintName("FK_CBEsProcess_Target_LOG_CBEsTargetResult_LOG_Header_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesProcessTargetLogs)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsProcess_Target_LOG_UpdateByID");
        });

        modelBuilder.Entity<CbesQuarterlyScore>(entity =>
        {
            entity.ToTable("CBEsQuarterlyScore");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesActivityId).HasColumnName("CBEsActivity_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesActivity).WithMany(p => p.CbesQuarterlyScores)
                .HasForeignKey(d => d.CbesActivityId)
                .HasConstraintName("FK_CBEsQuarterlyScore_CBEsActivityID");
        });

        modelBuilder.Entity<CbesQuarterlyScoreLog>(entity =>
        {
            entity.ToTable("CBEsQuarterlyScore_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesActivityLogId).HasColumnName("CBEsActivity_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesActivityLog).WithMany(p => p.CbesQuarterlyScoreLogs)
                .HasForeignKey(d => d.CbesActivityLogId)
                .HasConstraintName("FK_CBEsQuarterlyScore_LOG_CBESActivity_LOG_id");
        });

        modelBuilder.Entity<CbesReportForm>(entity =>
        {
            entity.ToTable("CBEsReportForm");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlanningId).HasColumnName("CBEsPlanning_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanning).WithMany(p => p.CbesReportForms)
                .HasForeignKey(d => d.CbesPlanningId)
                .HasConstraintName("FK_CBEsReportForm_CBEsPlanningID");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesReportFormCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_CBEsReportForm_CreateBy");

            entity.HasOne(d => d.UserVerifyNavigation).WithMany(p => p.CbesReportFormUserVerifyNavigations)
                .HasForeignKey(d => d.UserVerify)
                .HasConstraintName("FK_CBEsReportForm_CBEsUserVerify");
        });

        modelBuilder.Entity<CbesReportFormLog>(entity =>
        {
            entity.ToTable("CBEsReportForm_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesPlanningLogHeaderId).HasColumnName("CBEsPlanning_LOG_Header_id");
            entity.Property(e => e.CbesPlanningLogId).HasColumnName("CBEsPlanning_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesPlanningLogHeader).WithMany(p => p.CbesReportFormLogs)
                .HasForeignKey(d => d.CbesPlanningLogHeaderId)
                .HasConstraintName("FK_CBEsReportForm_LOG_CBEsTargetResult_LOG_Header");

            entity.HasOne(d => d.CbesPlanningLog).WithMany(p => p.CbesReportFormLogs)
                .HasForeignKey(d => d.CbesPlanningLogId)
                .HasConstraintName("FK_CBEsReportForm_LOG_CBESPlanning_LOG_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesReportFormLogUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsReportForm_LOG_UpdateByID");

            entity.HasOne(d => d.UserVerifyNavigation).WithMany(p => p.CbesReportFormLogUserVerifyNavigations)
                .HasForeignKey(d => d.UserVerify)
                .HasConstraintName("FK_CBEsReportForm_LOG_UserVerify");
        });

        modelBuilder.Entity<CbesRole>(entity =>
        {
            entity.ToTable("CBEsRole");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesRoleCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Role_CreateBy");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesRoleUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_Role_UpdateBy");
        });

        modelBuilder.Entity<CbesRoleWithPermission>(entity =>
        {
            entity.ToTable("CBEsRoleWithPermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PermissionId).HasColumnName("Permission_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Permission).WithMany(p => p.CbesRoleWithPermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_RoleWithPermission_PermissionID");

            entity.HasOne(d => d.Role).WithMany(p => p.CbesRoleWithPermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleWithPermission_RoleID");
        });

        modelBuilder.Entity<CbesTargetResultLogHeader>(entity =>
        {
            entity.ToTable("CBEsTargetResult_LOG_Header");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CbesLogId).HasColumnName("CBEs_Log_id");
            entity.Property(e => e.CbesTargetResultLogTypeId).HasColumnName("CBEsTargetResult_LOG_Type_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesTargetResultLogHeaders)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEsTargetResult_LOG_Header_CBEsID");

            entity.HasOne(d => d.CbesLog).WithMany(p => p.CbesTargetResultLogHeaders)
                .HasForeignKey(d => d.CbesLogId)
                .HasConstraintName("FK_CBEsTargetResult_LOG_Header_CBEs_LOG");

            entity.HasOne(d => d.CbesTargetResultLogType).WithMany(p => p.CbesTargetResultLogHeaders)
                .HasForeignKey(d => d.CbesTargetResultLogTypeId)
                .HasConstraintName("FK_CBEsTargetResult_LOG_Header_CBEsTargetResult_LOG_Type_id");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesTargetResultLogHeaders)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsTargetResult_LOG_Header_CBEsUser");
        });

        modelBuilder.Entity<CbesTragetResultLogType>(entity =>
        {
            entity.ToTable("CBEsTragetResult_LOG_Type");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CbesUser>(entity =>
        {
            entity.ToTable("CBEsUser");

            entity.HasIndex(e => e.Username, "CBEsUser_Username_Unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.LoginDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserWithRoleId).HasColumnName("UserWithRole_id");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.InverseCreateByNavigation)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_User_CreateBy");

            entity.HasOne(d => d.Position).WithMany(p => p.CbesUsers)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_User_PositionID");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.InverseUpdateByNavigation)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_User_UpdateBy");
        });

        modelBuilder.Entity<CbesUserWithRole>(entity =>
        {
            entity.ToTable("CBEsUserWithRole");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesUserWithRoleCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_UserWithRole_CreateBy");

            entity.HasOne(d => d.Role).WithMany(p => p.CbesUserWithRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserWithRole_RoleID");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesUserWithRoleUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_UserWithRole_UpdateBy");

            entity.HasOne(d => d.User).WithMany(p => p.CbesUserWithRoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserWithRole_UserID");
        });

        modelBuilder.Entity<CbesWithSubSupervisor>(entity =>
        {
            entity.ToTable("CBEsWithSubSupervisor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbesWithSubSupervisors)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEsWithSubSupervisor_CBEsID");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbesWithSubSupervisorCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_CBEsWithSubSupervisor_CreateByID");

            entity.HasOne(d => d.Position).WithMany(p => p.CbesWithSubSupervisors)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_CBEsWithSubSupervisor_PositionID");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbesWithSubSupervisorUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsWithSubSupervisor_UpdateByID");
        });

        modelBuilder.Entity<CbesWithSubSupervisorLog>(entity =>
        {
            entity.ToTable("CBEsWithSubSupervisor_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesLogHeaderId).HasColumnName("CBEs_LOG_Header_id");
            entity.Property(e => e.CbesLogId).HasColumnName("CBEs_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesLogHeader).WithMany(p => p.CbesWithSubSupervisorLogs)
                .HasForeignKey(d => d.CbesLogHeaderId)
                .HasConstraintName("FK_CBEsWithSubSupervisor_LOG_CBEs_LOG_Header");

            entity.HasOne(d => d.CbesLog).WithMany(p => p.CbesWithSubSupervisorLogs)
                .HasForeignKey(d => d.CbesLogId)
                .HasConstraintName("FK_CBEsWithSubSupervisor_LOG_CBEs_LOG");

            entity.HasOne(d => d.Position).WithMany(p => p.CbesWithSubSupervisorLogs)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_CBEsWithSubSupervisor_LOG_CBEsPosition");
        });

        modelBuilder.Entity<CbesWithSupervisorLog>(entity =>
        {
            entity.ToTable("CBEsWithSupervisor_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesLogHeaderId).HasColumnName("CBEs_LOG_Header_id");
            entity.Property(e => e.CbesLogId).HasColumnName("CBEs_LOG_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesLogHeader).WithMany(p => p.CbesWithSupervisorLogs)
                .HasForeignKey(d => d.CbesLogHeaderId)
                .HasConstraintName("FK_CBEsWithSupervisor_LOG_CBEs_LOG_Header");

            entity.HasOne(d => d.CbesLog).WithMany(p => p.CbesWithSupervisorLogs)
                .HasForeignKey(d => d.CbesLogId)
                .HasConstraintName("FK_CBEsWithSupervisor_LOG_CBEsWithSupervisor_LOG");

            entity.HasOne(d => d.Position).WithMany(p => p.CbesWithSupervisorLogs)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_CBEsWithSupervisor_LOG_CBEsPosition");
        });

        modelBuilder.Entity<CbeswithSupervisor>(entity =>
        {
            entity.ToTable("CBESwithSupervisor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesId).HasColumnName("CBEs_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cbes).WithMany(p => p.CbeswithSupervisors)
                .HasForeignKey(d => d.CbesId)
                .HasConstraintName("FK_CBEsWithSupervisor_CBEsID");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.CbeswithSupervisorCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_CBEsWithSupervisor_CreateByID");

            entity.HasOne(d => d.Position).WithMany(p => p.CbeswithSupervisors)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_CBEsWithSupervisor_PositionID");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CbeswithSupervisorUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_CBEsWithSupervisor_UpdateByID");
        });

        modelBuilder.Entity<MaturityWithSupervisor>(entity =>
        {
            entity.ToTable("MaturityWithSupervisor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MaturityId).HasColumnName("Maturity_id");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.MaturityWithSupervisorCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_MaturityWithSupervisor_CreateBy");

            entity.HasOne(d => d.Maturity).WithMany(p => p.MaturityWithSupervisors)
                .HasForeignKey(d => d.MaturityId)
                .HasConstraintName("FK_MaturityWithSupervisor_MaturityID");

            entity.HasOne(d => d.Position).WithMany(p => p.MaturityWithSupervisors)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_MaturityWithSupervisor_PositionID");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.MaturityWithSupervisorUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_MaturityWithSupervisor_UpdateBy");
        });

        modelBuilder.Entity<MaturityWithSupervisorLog>(entity =>
        {
            entity.ToTable("MaturityWithSupervisor_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CbesLogHeaderId).HasColumnName("CBEs_LOG_Header_id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MaturityLogId).HasColumnName("MaturityLOG_id");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CbesLogHeader).WithMany(p => p.MaturityWithSupervisorLogs)
                .HasForeignKey(d => d.CbesLogHeaderId)
                .HasConstraintName("FK_MaturityWithSupervisor_LOG_CBEs_LOG_Header");

            entity.HasOne(d => d.MaturityLog).WithMany(p => p.MaturityWithSupervisorLogs)
                .HasForeignKey(d => d.MaturityLogId)
                .HasConstraintName("FK_MaturityWithSupervisor_LOG_CBEsMaturity_LOG");

            entity.HasOne(d => d.Position).WithMany(p => p.MaturityWithSupervisorLogs)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_MaturityWithSupervisor_LOG_CBEsPosition");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
