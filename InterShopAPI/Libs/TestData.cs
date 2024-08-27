using InterShopAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using InterShopAPI.Controllers;

namespace InterShopAPI.Libs
{
    /// <summary>
    /// Утилитарный класс для добавления тестовых данных в БД
    /// </summary>
    public static class TestData
    {
        public static InterShopContext Context { get; set; }

        /// <summary>
        /// Метод. Добавляет данные для всех таблиц 
        /// </summary>
        /// <param name="context"></param>
        public static void AddData(InterShopContext? context = null)
        {
            AddCategories(context);
            AddProducts(context);
            AddImagesOfProduct(context);
            AddProductVariants(context);
            AddPriceHistory(context);
            AddDiscountHistory(context);
            AddDataTypes(context);
            AddUnits(context);
            AddCharacteristics(context);
            AddCategoryCharacteristics(context);
            AddCharacteristicValueSet(context);
            AddProductVariantCharacteristics(context);
            AddRoles(context);
            AddUsers(context);
            AddComments(context);
        }

        private static void AddComments(InterShopContext context)
        {
            context.Comments.AddRange(
                new List<Comment>()
                {
                    new Comment() { UserId = 1, ProductId = 2, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 5 },
                    new Comment() { UserId = 1, ProductId = 2, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 5 },
                    new Comment() { UserId = 1, ProductId = 3, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 1 },
                    new Comment() { UserId = 1, ProductId = 3, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 3 },
                    new Comment() { UserId = 1, ProductId = 4, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 1 },
                    new Comment() { UserId = 1, ProductId = 4, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 2 },
                    new Comment() { UserId = 1, ProductId = 5, Message = "Кал.", DateTime = DateTime.UtcNow, Rating = 3 },
                }
            );

            context.SaveChanges();
        }

        private static void AddImagesOfProduct(InterShopContext? context)
        {
            setContext(context);

            context.ImagesOfProducts.AddRange(new List<ImagesOfProduct>
            {                                                                               // ID:
                new ImagesOfProduct() { Path = "we32fs.jpg", ProductId = 1},
                new ImagesOfProduct() { Path = "we33fs.jpg", ProductId = 1},
                new ImagesOfProduct() { Path = "we34fs.jpg", ProductId = 1},
                new ImagesOfProduct() { Path = "we31fs.jpg", ProductId = 2},
                new ImagesOfProduct() { Path = "we35fs.jpg", ProductId = 2},
                new ImagesOfProduct() { Path = "we36fs.jpg", ProductId = 3},
                new ImagesOfProduct() { Path = "we37fs.jpg", ProductId = 4},                  // 9
                new ImagesOfProduct() { Path = "we38fs.jpg", ProductId = 6}
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет категории в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddCategories(InterShopContext? context = null)
        {
            setContext(context);

            context.Categories.AddRange(new List<Category>
            {                                                                               // ID:
                new Category {Name = "Бытовая техника"},                                    // 1 
                new Category {Name = "ПК, ноутбуки, периферия"},                            // 2
                new Category {Name = "Комплектующие для ПК"},                               // 3

                new Category { Name = "Ноутбуки", ParentID = 2 },                           // 4
                new Category { Name = "Мониторы", ParentID = 2 },                           // 5
                new Category { Name = "Персональные компьютеры", ParentID = 2 },            // 6

                new Category { Name = "Компьютеры для бизнеса", ParentID = 6 },             // 7
                new Category { Name = "Игровые компьютеры", ParentID = 6 },                 // 8
                new Category { Name = "Офисные компьютеры", ParentID = 6 }                  // 9
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет товары в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddProducts(InterShopContext? context = null)
        {
            setContext(context);

            context.Products.AddRange(new List<Product>
            {                                                                                                                                           // ID:
                new Product { Name = "Игровой компьютер #1", CategoryID = 8, Description="Супер комп #1", OnSale=false, IsDeleted = true },             // 1
                new Product { Name = "Игровой компьютер #2", CategoryID = 8, Description="Супер комп #1", OnSale=true, IsDeleted = true },              // 2
                new Product { Name = "Игровой компьютер #3", CategoryID = 8, Description="Супер комп #1", OnSale=true },                                // 3
                new Product { Name = "Офисный компьютер #1", CategoryID = 9, Description="Супер комп #1", OnSale=false },                               // 4
                new Product { Name = "Офисный компьютер #2", CategoryID = 9, Description="Супер комп #1", OnSale=true },                                // 5
                new Product { Name = "Офисный компьютер #3", CategoryID = 9, Description="Супер комп #1", OnSale=true,
                    PreviewPath = "OfficePC_1.jpg" }                                                                                                    // 6
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет варианты товаров в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddProductVariants(InterShopContext? context = null)
        {
            setContext(context);

            context.ProductVariants.AddRange(new List<ProductVariant>
            {                                                                   // ID:                                                                    
                new ProductVariant { ProductID = 1,                             // 1
                    Name = "Игровой компьютер #1 Базовая версия", IsMain = true },
                new ProductVariant { ProductID = 1,                             // 2
                    Name = "Игровой компьютер #1 Про-версия"},
                new ProductVariant { ProductID = 2,                             // 3
                    Name = "Игровой компьютер #2 Базовая версия", IsMain = true },
                new ProductVariant { ProductID = 2,                             // 4
                    Name = "Игровой компьютер #2 Про-версия"},
                new ProductVariant { ProductID = 3,                             // 5
                    Name = "Игровой компьютер #3 Базовая версия", IsMain = true},
                new ProductVariant { ProductID = 4,                             // 6
                    Name = "Офисный компьютер #1 Базовая версия", IsMain = true},
                new ProductVariant { ProductID = 5,                             // 7
                    Name = "Офисный компьютер #2 Базовая версия", IsMain = true},
                new ProductVariant { ProductID = 6,                             // 8
                    Name = "Офисный компьютер #3 Базовая версия", IsMain = true}
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет типы данных в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddDataTypes(InterShopContext? context = null)
        {
            setContext(context);

            context.DataTypes.AddRange(new List<DataType>
            {                                                   // ID:
                new DataType { Name = "Числовой" },             // 1
                new DataType { Name = "Строковый"},             // 2
                new DataType { Name = "Список"},                // 3
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет единицы измерения в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddUnits(InterShopContext? context = null)
        {
            setContext(context);

            context.Units.AddRange(new List<Unit>
            {                                                                       // ID:
                new Unit { Name = "Гб", FullName = "Гигабайт" },                    // 1
                new Unit { Name = "Мб", FullName = "Мегабайт" },                    // 2
                new Unit { Name = "Кб", FullName = "Килобайт" },                    // 3
                new Unit { Name = "Гб/сек", FullName = "Гигабайт в секнуду" },      // 4
                new Unit { Name = "Мб/сек", FullName = "Мегабайт в секунду" },      // 5
                new Unit { Name = "Кб/сек", FullName = "Килобайт в всекунду"},      // 6
                new Unit { Name = "Кг", FullName = "Килограмм" },                   // 7
                new Unit { Name = "г", FullName = "Грамм" },                        // 8
                new Unit { Name = "мг", FullName = "Миллиграмм"},                   // 9
                new Unit { Name = "Вт", FullName = "Ватт"},                         // 10
                new Unit { Name = "руб", FullName = "Рублей"},                      // 11
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет характеристики в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddCharacteristics(InterShopContext? context = null)
        {
            setContext(context);


            context.Characteristics.AddRange(new List<Characteristic>
            {                                                                               // ID:
                new Characteristic { Name = "Объём ОЗУ", DataTypeID = 1, UnitID = 1 },      // 1
                new Characteristic { Name = "Объём ПЗУ", DataTypeID = 1, UnitID = 1 },      // 2
                new Characteristic { Name = "Процессор", DataTypeID = 3 },                  // 3
                new Characteristic { Name = "Видеокарта", DataTypeID = 3 },                 // 4
                new Characteristic { Name = "Мощность", DataTypeID = 1, UnitID = 10 },      // 5
                new Characteristic { Name = "Стоимость", DataTypeID = 1, UnitID = 11 },     // 6
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет характеристики категорий в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddCategoryCharacteristics(InterShopContext? context = null)
        {
            setContext(context);

            context.CategoryCharacteristics.AddRange(new List<CategoryCharacteristics>
            {
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 1 },
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 2 },
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 3 },
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 4 },
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 5 },
                new CategoryCharacteristics { CategoryId = 7, CharacteristicId = 6 },

                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 1 },
                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 2 },
                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 3 },
                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 4 },
                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 5 },
                new CategoryCharacteristics { CategoryId = 8, CharacteristicId = 6 },

                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 1 },
                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 2 },
                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 3 },
                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 4 },
                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 5 },
                new CategoryCharacteristics { CategoryId = 9, CharacteristicId = 6 },
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет наборы значений характеристик в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddCharacteristicValueSet(InterShopContext? context = null)
        {
            setContext(context);


            context.CharacteristicValueSets.AddRange(new List<CharacteristicValueSet>
            {                                                                                            // ID:
                new CharacteristicValueSet { CharacteristicID = 3, Value = "Intel Pentium 4 301" },      // 1
                new CharacteristicValueSet { CharacteristicID = 3, Value = "AMD Ryzen 5 R104" },         // 2
                new CharacteristicValueSet { CharacteristicID = 3, Value = "Intel Core I9 12600" },      // 3

                new CharacteristicValueSet { CharacteristicID = 4, Value = "NVidia GTX 4090" },          // 4
                new CharacteristicValueSet { CharacteristicID = 4, Value = "AMD Radeon HD 6570" },       // 5
                new CharacteristicValueSet { CharacteristicID = 4, Value = "Intel Graphics 436" },       // 6
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет характеристики вариантов товаров в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddProductVariantCharacteristics(InterShopContext? context = null)
        {
            setContext(context);
            context.ProductVariantsCharacteristics.AddRange(new List<ProductVariantCharacteristics>
            {
                new ProductVariantCharacteristics { ProductVariantID = 1, CharacteristicID = 1, Value = "16" },
                new ProductVariantCharacteristics { ProductVariantID = 1, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 1, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 1, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 1, CharacteristicID = 5, Value = "100" },

                new ProductVariantCharacteristics { ProductVariantID = 2, CharacteristicID = 1, Value = "32" },
                new ProductVariantCharacteristics { ProductVariantID = 2, CharacteristicID = 2, Value = "1024" },
                new ProductVariantCharacteristics { ProductVariantID = 2, CharacteristicID = 3, Value = "Intel Core I9 12600" },
                new ProductVariantCharacteristics { ProductVariantID = 2, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 2, CharacteristicID = 5, Value = "150" },

                new ProductVariantCharacteristics { ProductVariantID = 3, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 3, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 3, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 3, CharacteristicID = 4, Value = "Intel Graphics 436" },
                new ProductVariantCharacteristics { ProductVariantID = 3, CharacteristicID = 5, Value = "80" },

                new ProductVariantCharacteristics { ProductVariantID = 4, CharacteristicID = 1, Value = "16" },
                new ProductVariantCharacteristics { ProductVariantID = 4, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 4, CharacteristicID = 3, Value = "AMD Ryzen 5 R104" },
                new ProductVariantCharacteristics { ProductVariantID = 4, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 4, CharacteristicID = 5, Value = "110" },

                new ProductVariantCharacteristics { ProductVariantID = 5, CharacteristicID = 1, Value = "16" },
                new ProductVariantCharacteristics { ProductVariantID = 5, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 5, CharacteristicID = 3, Value = "AMD Ryzen 5 R104" },
                new ProductVariantCharacteristics { ProductVariantID = 5, CharacteristicID = 4, Value = "NVidia GTX 4090" },
                new ProductVariantCharacteristics { ProductVariantID = 5, CharacteristicID = 5, Value = "160" },

                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 5, Value = "90" },

                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 5, Value = "90" },

                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 5, Value = "90" }
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет роли пользователей в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddRoles(InterShopContext context)
        {
            context.Roles.AddRange(new List<Role>
            {
                new Role { Name = "User"},
                new Role { Name = "Moderator"},
                new Role { Name = "Administrator"}
            });
            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет тестовых пользователей в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddUsers(InterShopContext? context = null)
        {
            User user = new User();
            string password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM="; //123

            user.Login = "Test";
            user.Mail = "test@mail.ru";
            user.RoleId = 1;
            user.Password = LibJWT.AppendSalt(password);            
            User userAdmin = new();            
            userAdmin.Login = "TestAdmin";
            userAdmin.Mail = "tes@mail.ru";
            userAdmin.RoleId = 3;
            userAdmin.Password = LibJWT.AppendSalt(password);
            context.Users.Add(user);
            context.Users.Add(userAdmin);
            context.SaveChangesAsync();
        }

        /// <summary>
        /// Метод. Преобразует массив байт в строку 
        /// </summary>
        /// <param name="array">Массив байт</param>
        /// <returns>Строковое значение массива</returns>
        private static string byteArrayToString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in array)
            {
                builder.Append(num);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Метод. Добавляет тестовую историю цен в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddPriceHistory(InterShopContext? context = null)
        {
            setContext(context);
            context.PriceHistories.AddRange(new List<PriceHistory>()
            {
                new PriceHistory { ProductVariantId = 1, Date = DateOnly.Parse("01-01-2022"), Price = 12043.0f },
                new PriceHistory { ProductVariantId = 2, Date = DateOnly.Parse("01-01-2022"), Price = 12044.0f },
                new PriceHistory { ProductVariantId = 3, Date = DateOnly.Parse("01-01-2022"), Price = 12045.0f },
                new PriceHistory { ProductVariantId = 4, Date = DateOnly.Parse("01-01-2022"), Price = 12046.0f },
                new PriceHistory { ProductVariantId = 5, Date = DateOnly.Parse("01-01-2022"), Price = 12047.0f },
                new PriceHistory { ProductVariantId = 6, Date = DateOnly.Parse("01-01-2022"), Price = 12048.0f },
                new PriceHistory { ProductVariantId = 7, Date = DateOnly.Parse("01-01-2022"), Price = 12049.99f },
                new PriceHistory { ProductVariantId = 8, Date = DateOnly.Parse("01-01-2022"), Price = 14425.0f }
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Добавляет тестовую историю скидок в БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void AddDiscountHistory(InterShopContext? context = null)
        {
            setContext(context);
            context.DiscountHistories.AddRange(new List<DiscountHistory>()
            {
                new DiscountHistory { ProductId = 1, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 12 },
                new DiscountHistory { ProductId = 2, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 20 },
                new DiscountHistory { ProductId = 3, DateFrom = DateOnly.Parse("01-01-2019"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 42 },
                new DiscountHistory { ProductId = 4, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 10 },
                new DiscountHistory { ProductId = 5, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 10 },
            });

            context.SaveChanges();
        }

        /// <summary>
        /// Метод. Устанавливает контекст БД для всего класса
        /// </summary>
        /// <param name="context">Контекст БД</param>
        private static void setContext(InterShopContext context)
        {
            if (context != null)
            {
                Context = context;
            }
            if (Context is null)
                throw new Exception("Передайте контекст базы данных в качестве параметра," +
                    " или установите свойство класса вручную");
        }
    }
}