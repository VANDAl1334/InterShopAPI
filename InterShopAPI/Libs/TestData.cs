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
            // AddComments(context);
            AddBaskets(context);
            AddOrderInfo(context);
            AddOrders(context);
            AddShop(context);
        }

        private static void AddShop(InterShopContext context)
        {
            setContext(context);

            context.Shops.AddRange(new List<Shop> {
                new Shop { Address = "ул. Зиповская, 7", Longitude = 38.995674f, Latitude = 45.063859f, YandexLink = "https://yandex.ru/maps/35/krasnodar/?ll=38.997031%2C45.063108&mode=poi&poi%5Bpoint%5D=38.995674%2C45.063859&poi%5Buri%5D=ymapsbm1%3A%2F%2Forg%3Foid%3D1002617669&z=17.36"},
                new Shop { Address = "ул. Российская, 100", Longitude = 39.025423f, Latitude = 45.120432f, YandexLink = "https://yandex.ru/maps/35/krasnodar/?ll=39.027754%2C45.121731&mode=poi&poi%5Bpoint%5D=39.025423%2C45.120432&poi%5Buri%5D=ymapsbm1%3A%2F%2Forg%3Foid%3D1239480739&z=14.72"},
                new Shop { Address = "ул. Красная, 12", Longitude = 38.964455f, Latitude = 45.011862f, YandexLink = "https://yandex.ru/maps/org/spetsializirovannaya_klinicheskaya_psikhiatricheskaya_bolnitsa_1/8285523058/?display-text=%D0%BF%D1%81%D0%B8%D1%85%D0%B8%D0%B0%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F%20%D0%B1%D0%BE%D0%BB%D1%8C%D0%BD%D0%B8%D1%86%D0%B0&ll=38.965700%2C45.011395&mode=search&sctx=ZAAAAAgBEAAaKAoSCdh%2BMsaHgUNAESYA%2F5QqhUZAEhIJQpjbvdwnsz8RAmISLuQRnD8iBgABAgMEBSgKOABAI0gBYh5yZWxldl9zZXJ2aWNlX2FyZWFfcGVyY2VudD0xMDBqAnJ1nQHNzEw9oAEAqAEAvQE%2Brh7qwgEQ8pjs7h73krT%2BA8P37%2BTZAYICL9C%2F0YHQuNGF0LjQsNGC0YDQuNGH0LXRgdC60LDRjyDQsdC%2B0LvRjNC90LjRhtCwigIsNTM0MzcyNjA1NTkkMTg0MTA1OTU2JDE4NDEwNTk1OCQxOTgzOTUyODk1NDKSAgCaAgxkZXNrdG9wLW1hcHOqAgwyMjQ1MTg3MTQ5NjM%3D&sll=38.968201%2C45.011395&sspn=0.013704%2C0.006857&text=%D0%BF%D1%81%D0%B8%D1%85%D0%B8%D0%B0%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F%20%D0%B1%D0%BE%D0%BB%D1%8C%D0%BD%D0%B8%D1%86%D0%B0&z=16.72"},
            });

            context.SaveChanges();
        }

        private static void AddOrderInfo(InterShopContext context)
        {
            setContext(context);

            context.OrderStatuses.AddRange(
                new List<OrderStatus>
                {
                    new OrderStatus { Name = "Готов к выдаче" },
                    new OrderStatus { Name = "Выдан" },
                    new OrderStatus { Name = "В пути" },
                    new OrderStatus { Name = "Отменён" }
                }
            );

            context.DeliveryTypes.AddRange(
                new List<DeliveryType>
                {
                    new DeliveryType { Name = "Самовывоз" },
                    new DeliveryType { Name = "Доставка" }
                }
            );


            context.PaymentTypes.AddRange(
                new List<PaymentType>
                {
                    new PaymentType { Name = "Наличными" },
                    new PaymentType { Name = "Банковской картой" }
                }
            );

            context.PayStatuses.AddRange(
                new List<PayStatus>
                {
                    new PayStatus { Name = "Оплачен" },
                    new PayStatus { Name = "Не оплачен" }
                }
            );

            context.SaveChanges();
        }

        private static void AddOrders(InterShopContext context)
        {
            setContext(context);

            context.Orders.AddRange(
                new List<Order> {
                    new Order
                    {
                        UserId = 1,
                        DateTimeCreating = DateTime.UtcNow,
                        DeliveryDate = DateOnly.Parse("12-12-2024"),
                        OrderStatusId = 1,
                        DeliveryTypeId = 1,
                        DeliveryAddress = "г. Краснодар, ул. Греновская, д.23",
                        PaymentTypeId = 1,
                        PayStatusId = 1,

                        OrderDetails = new List<OrderDetails>
                        {
                            new OrderDetails
                            {
                                OrderId = 1,
                                ProductVariantId = 1,
                                Count = 2,
                                Price = 12524.12f
                            },
                            new OrderDetails
                            {
                                OrderId = 1,
                                ProductVariantId = 2,
                                Count = 3,
                                Price = 12524.12f
                            }
                        }
                    },
                    new Order
                    {
                        UserId = 1,
                        DateTimeCreating = DateTime.UtcNow,
                        DeliveryDate = DateOnly.Parse("12-12-2024"),
                        OrderStatusId = 1,
                        DeliveryTypeId = 1,
                        DeliveryAddress = "г. Краснодар, ул. Греновская, д.23",
                        PaymentTypeId = 1,
                        PayStatusId = 1,

                        OrderDetails = new List<OrderDetails>
                        {
                            new OrderDetails
                            {
                                OrderId = 1,
                                ProductVariantId = 1,
                                Count = 2,
                                Price = 12524.12f
                            },
                            new OrderDetails
                            {
                                OrderId = 1,
                                ProductVariantId = 2,
                                Count = 4,
                                Price = 12524.12f
                            }
                        }
                    },
                }
            );

            context.SaveChanges();
        }

        public static void AddBaskets(InterShopContext context)
        {
            setContext(context);

            context.Baskets.AddRange(
                new List<Basket> {
                    new Basket { ProductVariantId = 1, UserId = 1, Count = 2},
                    new Basket { ProductVariantId = 2, UserId = 1, Count = 1},
                    new Basket { ProductVariantId = 3, UserId = 1, Count = 3},
                    new Basket { ProductVariantId = 4, UserId = 1, Count = 4},
                    new Basket { ProductVariantId = 5, UserId = 1, Count = 2},
                }
            );

            context.SaveChanges();
        }

        public static void AddComments(InterShopContext context)
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
                new ProductVariant { ProductID = 3,                             // 6
                    Name = "Игровой компьютер #3 Про-версия"},
                new ProductVariant { ProductID = 4,                             // 7
                    Name = "Офисный компьютер #1 Базовая версия", IsMain = true},
                new ProductVariant { ProductID = 5,                             // 8
                    Name = "Офисный компьютер #2 Базовая версия", IsMain = true},
                new ProductVariant { ProductID = 6,                             // 9
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

                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 1, Value = "32" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 2, Value = "1024" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 3, Value = "AMD Ryzen 5 R104" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 4, Value = "NVidia GTX 8090" },
                new ProductVariantCharacteristics { ProductVariantID = 6, CharacteristicID = 5, Value = "280" },

                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 7, CharacteristicID = 5, Value = "90" },

                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 8, CharacteristicID = 5, Value = "90" },

                new ProductVariantCharacteristics { ProductVariantID = 9, CharacteristicID = 1, Value = "8" },
                new ProductVariantCharacteristics { ProductVariantID = 9, CharacteristicID = 2, Value = "512" },
                new ProductVariantCharacteristics { ProductVariantID = 9, CharacteristicID = 3, Value = "Intel Pentium 4 301" },
                new ProductVariantCharacteristics { ProductVariantID = 9, CharacteristicID = 4, Value = "AMD Radeon HD 6570" },
                new ProductVariantCharacteristics { ProductVariantID = 9, CharacteristicID = 5, Value = "90" }
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
            context.SaveChanges();
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
                new PriceHistory { ProductVariantId = 6, Date = DateOnly.Parse("01-01-2022"), Price = 14013.0f },
                new PriceHistory { ProductVariantId = 7, Date = DateOnly.Parse("01-01-2022"), Price = 12048.0f },
                new PriceHistory { ProductVariantId = 8, Date = DateOnly.Parse("01-01-2022"), Price = 12049.99f },
                new PriceHistory { ProductVariantId = 9, Date = DateOnly.Parse("01-01-2022"), Price = 14425.0f }
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
                //new DiscountHistory { ProductId = 1, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 12 },
                //new DiscountHistory { ProductId = 2, DateFrom = DateOnly.Parse("01-01-2022"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 20 },
                //new DiscountHistory { ProductId = 3, DateFrom = DateOnly.Parse("01-01-2019"), DateTo = DateOnly.Parse("01-01-2026"), Discount = 42 },
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