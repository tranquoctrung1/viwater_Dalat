//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

public partial class DBEntities : DbContext
{
    public DBEntities()
        : base("name=DBEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public DbSet<t_Accreditation_Types> t_Accreditation_Types { get; set; }
    public DbSet<t_Accreditations> t_Accreditations { get; set; }
    public DbSet<t_Channel_Configurations> t_Channel_Configurations { get; set; }
    public DbSet<t_Consumers> t_Consumers { get; set; }
    public DbSet<t_Data_Complexes> t_Data_Complexes { get; set; }
    public DbSet<t_Data_Raw_Indexes> t_Data_Raw_Indexes { get; set; }
    public DbSet<t_DisplayGroups> t_DisplayGroups { get; set; }
    public DbSet<t_Emails> t_Emails { get; set; }
    public DbSet<t_Logger_Configurations> t_Logger_Configurations { get; set; }
    public DbSet<t_Logger_Files> t_Logger_Files { get; set; }
    public DbSet<t_Logger_Histories> t_Logger_Histories { get; set; }
    public DbSet<t_Loggers> t_Loggers { get; set; }
    public DbSet<t_Meter_Files> t_Meter_Files { get; set; }
    public DbSet<t_Meter_Histories> t_Meter_Histories { get; set; }
    public DbSet<t_Meters> t_Meters { get; set; }
    public DbSet<t_Roles> t_Roles { get; set; }
    public DbSet<t_Site_Availabilities> t_Site_Availabilities { get; set; }
    public DbSet<t_Site_Files> t_Site_Files { get; set; }
    public DbSet<t_Site_Status> t_Site_Status { get; set; }
    public DbSet<t_Sites> t_Sites { get; set; }
    public DbSet<t_Staffs> t_Staffs { get; set; }
    public DbSet<t_Transmitter_Files> t_Transmitter_Files { get; set; }
    public DbSet<t_Transmitter_Histories> t_Transmitter_Histories { get; set; }
    public DbSet<t_Transmitters> t_Transmitters { get; set; }
    public DbSet<t_Units> t_Units { get; set; }
    public DbSet<t_Users> t_Users { get; set; }
    public DbSet<t_Function> t_Function { get; set; }
    public DbSet<t_Language> t_Language { get; set; }
    public DbSet<t_LanguageTranslate> t_LanguageTranslate { get; set; }
    public DbSet<t_SysParam> t_SysParam { get; set; }
    public DbSet<t_RoleFunction> t_RoleFunction { get; set; }
    public DbSet<t_Device_Status> t_Device_Status { get; set; }
    public DbSet<DeviceTokenApp> DeviceTokenApps { get; set; }
    public DbSet<t_GroupChannel> t_GroupChannel { get; set; }
    public DbSet<t_TakeSampleHistory> t_TakeSampleHistory { get; set; }
    public DbSet<t_Control_Sampler> t_Control_Sampler { get; set; }

    public virtual int p_Create_Data_Table(string channelId)
    {
        var channelIdParameter = channelId != null ?
            new ObjectParameter("ChannelId", channelId) :
            new ObjectParameter("ChannelId", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Create_Data_Table", channelIdParameter);
    }

    public virtual int p_Data_Logger_Get(string channelId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
    {
        var channelIdParameter = channelId != null ?
            new ObjectParameter("ChannelId", channelId) :
            new ObjectParameter("ChannelId", typeof(string));

        var startDateParameter = startDate.HasValue ?
            new ObjectParameter("StartDate", startDate) :
            new ObjectParameter("StartDate", typeof(System.DateTime));

        var endDateParameter = endDate.HasValue ?
            new ObjectParameter("EndDate", endDate) :
            new ObjectParameter("EndDate", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Data_Logger_Get", channelIdParameter, startDateParameter, endDateParameter);
    }

    public virtual int p_Drop_Data_Table(string channelId)
    {
        var channelIdParameter = channelId != null ?
            new ObjectParameter("channelId", channelId) :
            new ObjectParameter("channelId", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Drop_Data_Table", channelIdParameter);
    }

    public virtual int p_Index_Logger_Get(string channelId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
    {
        var channelIdParameter = channelId != null ?
            new ObjectParameter("ChannelId", channelId) :
            new ObjectParameter("ChannelId", typeof(string));

        var startDateParameter = startDate.HasValue ?
            new ObjectParameter("StartDate", startDate) :
            new ObjectParameter("StartDate", typeof(System.DateTime));

        var endDateParameter = endDate.HasValue ?
            new ObjectParameter("EndDate", endDate) :
            new ObjectParameter("EndDate", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Index_Logger_Get", channelIdParameter, startDateParameter, endDateParameter);
    }

    public virtual int p_ComplexData_Select_Faster()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_ComplexData_Select_Faster");
    }

    public virtual int p_Calculate_One_Site_Daily_Output(string siteId, Nullable<System.DateTime> start, Nullable<System.DateTime> end)
    {
        var siteIdParameter = siteId != null ?
            new ObjectParameter("SiteId", siteId) :
            new ObjectParameter("SiteId", typeof(string));

        var startParameter = start.HasValue ?
            new ObjectParameter("Start", start) :
            new ObjectParameter("Start", typeof(System.DateTime));

        var endParameter = end.HasValue ?
            new ObjectParameter("End", end) :
            new ObjectParameter("End", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Calculate_One_Site_Daily_Output", siteIdParameter, startParameter, endParameter);
    }

    public virtual int p_Calculate_One_Site_Hourly_Output(string siteId, Nullable<System.DateTime> start, Nullable<System.DateTime> end)
    {
        var siteIdParameter = siteId != null ?
            new ObjectParameter("SiteId", siteId) :
            new ObjectParameter("SiteId", typeof(string));

        var startParameter = start.HasValue ?
            new ObjectParameter("Start", start) :
            new ObjectParameter("Start", typeof(System.DateTime));

        var endParameter = end.HasValue ?
            new ObjectParameter("End", end) :
            new ObjectParameter("End", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_Calculate_One_Site_Hourly_Output", siteIdParameter, startParameter, endParameter);
    }
}
