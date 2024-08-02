using InterShopAPI.Libs;
using Microsoft.EntityFrameworkCore;

namespace InterShopAPI.Models
{
    public class InterShopContext : DbContext
    {
        public static string ConnectionString { get; set; }
        private static bool firstCall = true;

        public InterShopContext()
        {
            if (firstCall)
            {
                updateDatabase();
                firstCall = false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
        // Пользователи //////////
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CategoryCharacteristics> CategoryCharacteristics { get; set; }
        public DbSet<CharacteristicValueSet> CharacteristicValueSets { get; set; }
        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductVariantCharacteristics> ProductVariantsCharacteristics { get; set; }
        public DbSet<ImagesOfProduct> ImagesOfProducts { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<DiscountHistory> DiscountHistories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PayStatus> PayStatuses { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }

        /// <summary>
        /// Констркутор. Задаёт параметры контекста БД, а также пересоздаёт БД в SqlServer.
        /// </summary>
        /// <param name="options">Параметры контекста БД</param>
        public InterShopContext(DbContextOptions<InterShopContext> options)
            : base(options)
        {
            
        }

        /// <summary>
        /// Метод, пересоздаёт базу данных и добавляет тестовые данные
        /// </summary>
        private void updateDatabase()
        {
            Database.EnsureDeleted();   // удаляем базу данных, если есть
            Database.EnsureCreated();   // создаем базу данных

            TestData.AddData(this);
        }
        
        /// <summary>
        /// Обработчик при создании модели. Задаёт дополнительные параметры для некоторых таблиц.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настройка составного ключа для таблиц
            modelBuilder.Entity<CategoryCharacteristics>().HasKey(u => new { u.CategoryId, u.CharacteristicId } );
            modelBuilder.Entity<ProductVariantCharacteristics>().HasKey(u => new { u.CharacteristicID, u.ProductVariantID } );
            modelBuilder.Entity<Basket>().HasKey(u => new { u.ProductVariantId, u.UserId } );
            modelBuilder.Entity<OrderDetails>().HasKey(u => new { u.ProductVariantId, u.OrderId});

            // настройка внешнего ключа, ссылающийся на свою же таблицу
            modelBuilder.Entity<Category>().HasOne(e => e.Parent).WithMany(e => e.Children).OnDelete(DeleteBehavior.NoAction);
        }
    }
}