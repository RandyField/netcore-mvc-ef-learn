using Microsoft.EntityFrameworkCore;

namespace MODEL
{
    //创建DbSet每个实体集的属性
    //EF中的约定-
    //1.DbSet类型的属性名作为表名,对于那些没有被引用为DbSet属性的,实体类名称作为表名(如下面的Course可以不定义)
    //2.实体属性名称作为数据库中表的列名称
    //3.实体属性名称为ID或者classnameID 会被识别作为主键
    //4.一个属性被解释作为一个外键,那么一定是这种情况(eg:当Student的主键为ID时,
    //那么同时还存在StudentID,那么StudentID作为导航属性)
    //被命名为外键属性的(eg:EnrollmentID ,Enrollment实体的主键是EnrollmentID)
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            :base(options)
            {

            }

        //实体集通常对应于数据库表
        //实体对应与表中的行
        public DbSet<Course> Courses{get;set;}
        public DbSet<Enrollment> Enrollments {get;set;}

        //上面的Course和Enrollment是可以省略的,
        //因为在Student中隐式引用了Course和Enrollment
        public DbSet<Student> Students{get;set;}

        //在数据库创建好,EF Core创建与DbSet属性名相同的表
        //属性名通常是复数形式(Students,而不是student)
        //开发人员一般是不在乎表名是否是复数形式
        //所以在很多教程中,默认行为以单数表名形式重写在数据库上下文
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}