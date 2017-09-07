Steps in Generating of Context and Entities for EFCore

1. copy Magenic.Manpower.EFCore.dll and paste to \MagenicManpower\Magenic.Manpower\bin\
2. copy Magenic.Manpower.EFCore.deps.json and paste to \MagenicManpower\Magenic.Manpower.EFCore\bin\Debug\netcoreapp1.0\

3. use the text below to generate context and entities
Scaffold-DbContext "Server=<YOUR SERVER>;Database=MagenicManpowerDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

or use the full command line below
Scaffold-DbContext -Connection "Server=<YOUR SERVER>;Database=MagenicManpowerDB;Integrated Security=True;Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context MagenicManpowerDBContext -Project Magenic.Manpower.EFCore -force

4. after generating entities and context, In MagenicManpowerDBContext.cs file, replace the OnConfiguring method by this:
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var config = new ConfigurationBuilder()
		.SetBasePath(System.IO.Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json")
		.Build();
		optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
	}
5. Add necessary namespace for ConfigurationBuilder