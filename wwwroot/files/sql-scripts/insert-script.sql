insert into roles(name_) values
('Клиент'),
('Администратор');

insert into users(login, password_, email, role_id) values
('client', 'client', 'client@gmail.com', 1),
('admin', 'admin', 'admin@gmail.com', 2);

--insert into categories_of_products(name_, description) values
--('Test Category', 'Description of Test Category');

insert into categories_of_products(name_, description) values
('Протеиновые батончики', 'Вкусные и питательные перекусы с высоким содержанием белка, идеальные для восстановления после тренировки или в качестве легкого угощения на ходу.'),
-- ('Протеиновые вафли', 'Хрустящие и сладкие вафли, обогащенные белком, которые удовлетворят вашу тягу к сладкому и помогут поддержать уровень энергии в течение дня.'),
-- ('Протеиновое печенье', 'Нежное и сытное печенье с высоким содержанием протеина, идеально подходит для перекуса после тренировки или как угощение без чувства вины.'),
('Протеин', 'Основной строительный блок для мышц, необходимый для восстановления и роста, идеален для всех, кто стремится увеличить мышечную массу и улучшить физическую форму.'),
('Гейнеры', 'Питательные добавки с высоким содержанием углеводов и белков, предназначенные для набора массы и увеличения калорийности рациона, особенно полезны для спортсменов с высоким уровнем активности.'),
-- ('Изотоник', 'Напиток, обеспечивающий быстрое восстановление водно-солевого баланса во время и после интенсивных тренировок, помогает поддерживать высокую производительность.'),
-- ('Коллаген', 'Продукт, способствующий поддержанию здоровья суставов, кожи и волос, помогает сохранить эластичность и упругость, особенно важен для активных людей и спортсменов.'),
('Креатин', 'Эффективная добавка для повышения физической силы и выносливости, помогает улучшить результаты в силовых тренингах и ускорить восстановление мышц.'),
-- ('L-карнитин', 'Аминокислота, способствующая сжиганию жиров и повышению энергетических уровней, идеальна для тех, кто хочет улучшить результаты в кардионагрузках и похудении.'),
('Предтрен', 'Энергетическая добавка, предназначенная для повышения выносливости и концентрации перед тренировкой, помогает достичь максимальных результатов во время занятий.'),
-- ('Жиросжигатели', 'Специально разработанные формулы для ускорения обмена веществ и уменьшения жировых отложений, поддерживают процесс похудения и помогают достичь желаемого результата.'),
('Шейкеры', 'Удобные контейнеры для смешивания и хранения протеиновых коктейлей и других напитков, идеально подходят для использования в спортзале и на ходу.');

--insert into brands(name_, description, url) values
--('Optimum Nutrition', 'Description of Test Brand', 'https://www.optimumnutrition.ru/about/'),
--('Bombbar', 'Description of Test Brand', 'https://www.bombbar.ru/page/about'),
--('MyProtein', 'Description of Test Brand', 'https://www.myprotein.ru/about-us.list#:~:text=Myprotein%20является%20одним%20из%20лидеров,диетические%20закуски%20и%20спортивную%20одежду.'),
--('Maxler', 'Description of Test Brand', 'https://maxler.ru/about-us/'),
--('Optimeal', 'Description of Test Brand', 'https://optimeal.pro/o-nas');

insert into brands(name_, url) values
('Optimum Nutrition', 'https://www.optimumnutrition.ru/about/'),
('Bombbar', 'https://www.bombbar.ru/page/about'),
('MyProtein', 'https://www.myprotein.ru/about-us.list#:~:text=Myprotein%20является%20одним%20из%20лидеров,диетические%20закуски%20и%20спортивную%20одежду.'),
('Maxler', 'https://maxler.ru/about-us/'),
('Optimeal', 'https://optimeal.pro/o-nas');

insert into products(name_, description, photo, volume, price, category_id, brand_id) values
('GOLD STANDARD 100% WHEY', 'GOLD STANDARD 100% WHEY создан для поддержки и восстановления мышц после тренировки, а также широко используется, для дополнительного источника высококачественного белка в ваш основной рацион питания.', 'https://www.optimumnutrition.ru/upload/iblock/966/8fimbzxq5sjlgqpu235ohjtm04yjw6i5.jpg', 907, 5699, 2, 1),
('GOLD STANDARD 100% WHEY', 'GOLD STANDARD 100% WHEY создан для поддержки и восстановления мышц после тренировки, а также широко используется, для дополнительного источника высококачественного белка в ваш основной рацион питания.', 'https://www.optimumnutrition.ru/upload/iblock/966/8fimbzxq5sjlgqpu235ohjtm04yjw6i5.jpg', 4540, 22999, 2, 1),
('GOLD STANDARD 100% CASEIN', 'Премиальный мицеллярный казеин GOLD STANDARD 100% CASEIN обеспечит лучшее и более медленное усвоение белка до и после тренировки, чтобы помочь восстановить силы и мышцы.', 'https://www.optimumnutrition.ru/upload/iblock/fb2/3zqyfsuayvoue1ajs4prc811kyqzy2ze.jpg', 850, 6199, 2, 1),
('GOLD STANDARD PRO GAINER', 'PRO Gainer ™ от Optimum Nutrition - это высокобелковый гейнер для набора качественной мышечной массы. Благодаря тому, что суточный калораж данного гейнера повышается за счет белка, обеспечивается быстрый рост мышечной массы без жировых отложений', 'https://www.optimumnutrition.ru/upload/iblock/a36/rayixj1vch8s49mfev9jodf15inudi7w.jpg', 4620, 14999, 3, 1),
('GOLD STANDARD PRE-WORKOUT', 'Gold Standard Pre-Workout от Optimum Nutrition - это инновационный предтренировочный продукт, выполненный из проверенных и безопасных ингредиентов.', 'https://www.optimumnutrition.ru/upload/iblock/4bb/kjmozomz2ppr66xu6kq1yn0xy4bwesbd.jpg', 300, 3299, 5, 1),
('CREATINE 2500 CAPS', 'Micronized Creatine Сaps – это комплекс на основе креатина моногидрата в удобной капсулированной форме приёма от мирового бренда спортивного питания Optimum Nutrition, направленный на увеличение силового потенциала и выносливости мышечных волокон.', 'https://www.optimumnutrition.ru/upload/iblock/73e/3jjcc7u1hkrlnexjs1vhlayrtulcz3fl.jpg', 100, 5269, 4, 1),
('Протеиновый батончик Bombbar - Фисташковый пломбир (12 шт.)', 'Фисташковый батончик: приятный вкус, нежная консистенция, минимум калорий, удобен для перекуса, поддерживает уровень энергии.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3658.jpg.webp?1000000148', 12, 1668, 1, 2),
('Протеиновый батончик Bombbar - Печенье с кремом (12 шт.)', 'Батончики с разнообразными вкусами, включая орео и печенье, с хрустящими шариками, идеально подходят для перекуса и поддержания белка', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3660.jpg.webp?1000000148', 12, 1668, 1, 2),
('Протеиновый батончик Bombbar - Тирамису (12 шт.)', 'Батончики с насыщенным вкусом тирамису, идеально подходят для сытного перекуса и снижения углеводов, без лишней сладости и глазури.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3648.jpg.webp?1000000148', 12, 1668, 1, 2),
('Whey Protein Pro - Банан-манго (900г)', 'концентрат сывороточного белка, банан, манго, эмульгатор  (соевый лецитин),  ароматизаторы  натуральные, загустители (ксантановая камедь, гуаровая камедь), соль,  антиокислитель (аскорбиновая кислота), подсластители (сукралоза).', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_4101.jpg.webp?1000000148', 900, 2860, 2, 2),
('Гейнер Pro - Банановый коктейль (1000 г.)', 'Гейнер с приятным вкусом, хорошо размешивается, способствует набору массы при регулярных тренировках, подходит для восстановления после физических нагрузок.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3971.jpg.webp?1000000148', 1000, 1590, 3, 2),
('Креатин + Таурин Bombbar Pro - Лесные ягоды (300 г.)', 'Креатин эффективно поддерживает энергию и выносливость, приятный вкус, быстро растворяется, содержит таурин для дополнительного бодрящего эффекта.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3328.jpg.webp?1000000148', 300, 1390, 4, 2),
('Креатин + Таурин Bombbar Pro - Яблочный (300 г.)', 'Креатин способствует увеличению массы и силы, имеет разнообразные вкусы, хорошо растворяется, подходит для тренировок, улучшает выносливость.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3327.jpg.webp?1000000148', 300, 1390, 4, 2),
('Предтренировочный комплекс Bombbar Pro - Тропический (300 г.)', 'Эффективный предтренировочный комплекс: бодрит, улучшает выносливость, мягко действует, не вызывает побочных эффектов, приятный вкус, подходит для тренировок после рабочего дня.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3331.jpg.webp?1000000148', 300, 1490, 5, 2),
('Шейкер - "Bombbsar", 600 мл', 'Спортивный шейкер с логотипом "Бомббар" объемом 600 миллилитров, с мерной шкалой и сеточкой внутри для эффективного размешивания. Эргономичная форма и герметичная крышка - идеально для спортивного зала.', 'https://www.cpanel.bombbar.ru/uploads/pictures/shop/shop_nomenclature_picture_size/w500_shop_nomenclature_picture_3291.jpg.webp?1000000148', 600, 590, 6, 2),
('Сывороточный протеин (Зерновое молоко)', 'Мы выпускаем Impact Whey Protein с первых дней основания компании, и все это время он остается неизменным фаворитом наших покупателей. Высококачественный сывороточный белок, проверенный на наличие примесей, и приятная цена ― неудивительно, что этот продукт является нашим бестселлером уже более 20 лет. Каждая мерная ложка этой формулы, тщательно разработанной специалистами по питанию, содержит до 23 г белка*, который поддерживает рост и восстановление мышц. Производительность, хорошее самочувствие или поддержка сбалансированного образа жизни ― с нашим сывороточным белком вы сможете достигать любых целей каждый день.', 'https://static.thcdn.com/images/large/webp//productimg/1600/1600/10530943-2075212346863326.jpg', 250, 999, 2, 3),
('Сывороточный протеин (Ваниль)', 'Мы выпускаем Impact Whey Protein с первых дней основания компании, и все это время он остается неизменным фаворитом наших покупателей. Высококачественный сывороточный белок, проверенный на наличие примесей, и приятная цена ― неудивительно, что этот продукт является нашим бестселлером уже более 20 лет. Каждая мерная ложка этой формулы, тщательно разработанной специалистами по питанию, содержит до 23 г белка*, который поддерживает рост и восстановление мышц. Производительность, хорошее самочувствие или поддержка сбалансированного образа жизни ― с нашим сывороточным белком вы сможете достигать любых целей каждый день.', 'https://static.thcdn.com/images/large/original//productimg/1600/1600/13442763-1645168620211454.jpg', 500, 1799, 2, 3),
('Креатин моногидрат', 'Наш эффективный порошок является одной из самых изученных в мире форм креатина. Научно доказано, что она повышает физическую результативность, улучшая общие возможности организма.', 'https://static.thcdn.com/images/large/webp//productimg/1600/1600/10530050-1515217809145399.jpg', 100, 800, 4, 3),
('Хрустящий многослойный батончик – 12 x 58 g – (Шоколадная карамель)', 'Хрустящие многослойные протеиновые батончики — одни из самых соблазнительных в нашем ассортименте. Они состоят из нескольких слоев со вкусом и текстурой, перед которыми невозможно устоять. На хрустящий слой нанесены слой с высоким содержанием белка и карамель — мы создали аппетитную закуску, которая не вызовет угрызений совести за нарушение диеты и поможет в достижении ваших фитнес-целей.', 'https://static.thcdn.com/images/large/webp//productimg/1600/1600/12856634-1965172693084276.jpg', 12, 2399, 1, 3),
('Протеин 100% Golden Whey', 'Протеиновый порошок высшего качества, содержащий незаменимые аминокислоты, 907 г.', 'https://maxler.ru/wp-content/uploads/2023/02/pic-5.jpg', 907, 3699, 2, 4),
('Протеин 100% Golden Whey', 'Протеиновый порошок высшего качества, содержащий незаменимые аминокислоты, 2270 г.', 'https://maxler.ru/wp-content/uploads/2023/02/100_Golden_Whey_milk_chocolate_site1.jpg', 2270, 7299, 2, 4),
('Креатин моногидрат (Creatine 100% Monohydrate)', 'Высококачественный Maxler Creatine 100% Monohydrate, не содержит лишних ингредиентов. Он поможет вам выйти на новый уровень, будь то тренировки в зале или соревнования.', 'https://maxler.ru/wp-content/uploads/2023/09/Creatine_500g_can_site1-1.jpg', 500, 2699, 4, 4),
('Гейнер Mega Gainer', 'Mega Gainer рекомендуется для всех категорий спортсменов для быстрого получения энергии перед любой физической активностью. Кроме того, вы можете использовать продукт для увеличения и поддержания общей массы тела.', 'https://maxler.ru/wp-content/uploads/2023/09/Mega_Gainer_1000g_vanilla_site4.jpg', 1000, 4499, 3, 4),
('OptiMeal Whey Source', 'OptiMeal Whey Source сывороточный протеин в виде порошка, спортивное питание, спортпит, белковый коктейль, для мужчин и женщин, 900 г, печенье', 'https://ir.ozone.ru/s3/multimedia-t/wc1000/6381765917.jpg', 900, 3199,  2, 5),
('Предтренировочный комплекс Fury Rogers OptiMeal', 'Предтренировочный комплекс Fury Rogers OptiMeal, спортивное питание предтрен, энергетик спортивный, 280 г, вкус Мохито', 'https://optimeal.pro/storage/app/uploads/public/06b/7c3/df6/thumb__371_0_0_0_auto.png', 280, 1799, 5, 5);

insert into discounts(product_id, amount, date_of_end) values
(1, 50, '2025-08-15');

insert into reviews(text_, grade, product_id, user_id) values
('Test Text Review', 5, 1, 1),
('Test Text Review', 5, 2, 1),
('Test Text Review', 5, 3, 1),
('Test Text Review', 5, 4, 1),
('Test Text Review', 5, 5, 1),
('Test Text Review', 5, 6, 1),
('Test Text Review', 5, 7, 1),
('Test Text Review', 5, 8, 1),
('Test Text Review', 5, 9, 1),
('Test Text Review', 5, 10, 1),
('Test Text Review', 5, 11, 1),
('Test Text Review', 5, 12, 1),
('Test Text Review', 5, 13, 1),
('Test Text Review', 5, 14, 1),
('Test Text Review', 5, 15, 1),
('Test Text Review', 5, 16, 1),
('Test Text Review', 5, 17, 1),
('Test Text Review', 5, 18, 1),
('Test Text Review', 5, 19, 1),
('Test Text Review', 5, 20, 1),
('Test Text Review', 5, 21, 1),
('Test Text Review', 5, 22, 1),
('Test Text Review', 5, 23, 1),
('Test Text Review', 5, 24, 1),
('Test Text Review', 5, 25, 1),
('Test Text Review', 4, 1, 1),
('Test Text Review', 3, 2, 1),
('Test Text Review', 2, 3, 1),
('Test Text Review', 5, 4, 1),
('Test Text Review', 3, 5, 1),
('Test Text Review', 2, 6, 1),
('Test Text Review', 5, 7, 1),
('Test Text Review', 5, 8, 1),
('Test Text Review', 4, 9, 1),
('Test Text Review', 1, 10, 1),
('Test Text Review', 2, 11, 1),
('Test Text Review', 3, 12, 1),
('Test Text Review', 4, 13, 1),
('Test Text Review', 2, 14, 1),
('Test Text Review', 2, 15, 1),
('Test Text Review', 3, 16, 1),
('Test Text Review', 4, 17, 1),
('Test Text Review', 5, 18, 1),
('Test Text Review', 2, 19, 1),
('Test Text Review', 1, 20, 1),
('Test Text Review', 3, 21, 1),
('Test Text Review', 4, 22, 1),
('Test Text Review', 5, 23, 1),
('Test Text Review', 2, 24, 1),
('Test Text Review', 4, 25, 1);

insert into states_of_order (name_) values
('Заполняется'),
('Оформлен'),
('Собирается'),
('Готов к выдаче'),
('Выдан');

insert into addresses(city, street, building) values
('Ярославль', 'Гагарина', '11'),
('Ростов', 'Спартаковская', '57');

insert into stores(address_id) values
(1), (2);

insert into orders(user_id) values
(1);

insert into orders_products (product_id, order_id, count_of_product) values
(1, 1, 5), (2, 1, 3), (8, 1, 2);

insert into orders(user_id, store_id, state_of_order_id) values
(1, 1, 5);

insert into orders_products(product_id, order_id, count_of_product) values
(7, 2, 2), (10, 2, 1), (15, 2, 1);

insert into stores_products(store_id, product_id, count_of_product) values
(1, 1, 10), (1, 2, 10), (1, 3, 10), (1, 4, 10), (1, 5, 10), (1, 6, 10), (1, 7, 10), (1, 8, 10), (1, 9, 10), (1, 10, 10), (1, 11, 10), (1, 12, 10),
(1, 13, 0), (1, 14, 0), (1, 15, 0), (1, 16, 0), (1, 17, 0), (1, 18, 0), (1, 19, 0), (1, 20, 0), (1, 21, 0), (1, 22, 0), (1, 23, 0), (1, 24, 0), (1, 25, 10),
(2, 13, 10), (2, 14, 10), (2, 15, 10), (2, 16, 10), (2, 17, 10), (2, 18, 10), (2, 19, 10), (2, 20, 10), (2, 21, 10), (2, 22, 10), (2, 23, 10), (2, 24, 10), (2, 25, 10),
(2, 1, 0), (2, 2, 0), (2, 3, 0), (2, 4, 0), (2, 5, 0), (2, 6, 0), (2, 7, 0), (2, 8, 0), (2, 9, 10), (2, 10, 0), (2, 11, 0), (2, 12, 0);
