using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Xml.Linq;

public class Item
{
    public string Name {  get; set; }
    public string Type {  get; set; }
    public int AttackBonus {  get; set; }
    public int DefenseBonus {  get; set; }

    public Item(string name, string type, int attackBonus = 0, int defenseBonus = 0) // Конструктор класса Item для инициализации свойств
    {
        Name = name;
        Type = type;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
    }
    public virtual void Use(Player player) // Виртуальный метод использования предмета 
    {
        Console.WriteLine($"Использован предмет: {Name}");
    }
}
public class Weapon: Item
{
    // Конструктор класса Weapon, вызывает базовый конструктор с типом "Оружие"
    public Weapon(string name, int attackBonus) 
        : base(name, "Оружие", attackBonus, 0) { }
}
public class Armor: Item
{
    // Конструктор класса Armor, вызывает базовый конструктор с типом "Доспехи"
    public Armor(string name, int defenseBonus)
        : base(name, "Доспехи", 0, defenseBonus) { } 
}
public class Potion: Item
{
    // Конструктор класса Potion, вызывает базовый конструктор с типом "Зелье"
    public Potion(string name)
        : base(name, "Зелье") { } 
    public override void Use(Player player) // Переопределенный метод использования зелья
    {
        int healAmount = player.MaxHealth - player.Health;
        player.Heal(player.MaxHealth);
        Console.WriteLine($"{Name} полностью восстановил здоровье! (+{healAmount} HP)");
    }
}
public class Player
{
    public int Health {  get; set; }
    public int MaxHealth { get; set; }
    public Weapon CurrentWeapon { get; set; }
    public Armor CurrentArmor { get; set; }
    public bool IsFrozen { get; set; }
    public bool IsDefending { get; set; }
    public Player() //Конструктор класса Player, инициализирует начальные значения
    {
        MaxHealth = 100;
        Health = MaxHealth;
        CurrentWeapon = new Weapon("Кулаки", 5);
        CurrentArmor = new Armor("Простая одежда", 2);
        IsFrozen = false;
        IsDefending = false;
    }
    public void Attack(Enemy enemy) // Метод атаки врага
    {
        int damage = GetTotalAttack();
        Console.WriteLine($"\nВы атакуете {enemy.Name} и наносите {damage} урона!");
        enemy.TakeDamage(damage);
        IsDefending = false;
    }
    public void Defend() // Метод защиты от атаки
    {
        IsDefending = true;
        Console.WriteLine("Вы принимаете защитную стойку.Следующая атака будет ослаблена!");
    }
    public void TakeDamage(int damage) // Метод получения урона
    {
        if (IsDefending)
        {
            Random random = new Random();
            double dodgeChance = 0.4;
            if (random.NextDouble() < dodgeChance)
            {
                Console.WriteLine("\nВы увернулись от атаки!");
                IsDefending= false;
                return;
            }
            double block = 0.7 + (random.NextDouble() * 0.3);
            int blockedDamage = (int)(GetTotalDefense() * block);
            damage = Math.Max(0, damage - blockedDamage);
            Console.WriteLine($"Вы блокируете {blockedDamage} урона! Получено урона: {damage}");
            IsDefending = false;
        }
        Health = Math.Max(0, Health-damage);
        Console.WriteLine($"Вы получили {damage} урона. HP:{Health}/{MaxHealth}");
    }
    public void Heal(int amount) // Метод лечения игрока
    {
        int oldHealth = Health;
        Health = Math.Min(MaxHealth, Health + amount);
        int actualHeal = Health - oldHealth;
        Console.WriteLine($"Восстановлено {actualHeal} здоровья.\nHP: {Health}/{MaxHealth}"); // Вывод информации о лечении
    }
    public void EquipWeapon(Weapon weapon) // Метод экипировки оружия
    {
        CurrentWeapon = weapon;
        Console.WriteLine($"Вы экипировали: {weapon.Name} (+{weapon.AttackBonus} к атаке)");
    }
    public void EquipArmor(Armor armor) // Метод экипировки доспехов
    {
        CurrentArmor = armor;
        Console.WriteLine($"Вы экипировали: {armor.Name} (+{armor.AttackBonus} к атаке)");
    }
    public int GetTotalAttack() // Метод расчета общей атаки игрока
    {
        int attack = 10;
        if (CurrentWeapon != null)
            attack += CurrentWeapon.AttackBonus;
        return attack;
    }   
    public int GetTotalDefense() // Метод расчета общей защиты игрока
    {
        int defense = 5;
        if (CurrentArmor != null)
            defense += CurrentArmor.DefenseBonus;
        return defense;
    }
    public void ShowStatus() // Метод отображения статуса игрока
    {
        Console.WriteLine($"=== СТАТУС ИГРОКА ===");
        Console.WriteLine($"HP: {Health}/{MaxHealth}");
        Console.WriteLine($"Оружие: {CurrentWeapon.Name} (+{CurrentWeapon.AttackBonus} атаки)"); 
        Console.WriteLine($"Доспехи: {CurrentArmor.Name} (+{CurrentArmor.DefenseBonus} защиты)"); 
        Console.WriteLine($"Общая атака: {GetTotalAttack()}");
        Console.WriteLine($"Общая защита: {GetTotalDefense()}\n");
    }
    public void ShowCurrentEquipmentStats()  // Метод отображения статистики текущей экипировки
    {
        Console.WriteLine("\n=== ТЕКУЩАЯ ЭКИПИРОВКА ===");
        Console.WriteLine($"Оружие: {CurrentWeapon.Name} (+{CurrentWeapon.AttackBonus} атаки)");
        Console.WriteLine($"Доспехи: {CurrentArmor.Name} (+{CurrentArmor.DefenseBonus} защиты)");
        Console.WriteLine($"Общая атака: {GetTotalAttack()}");
        Console.WriteLine($"Общая защита: {GetTotalDefense()}\n");
    }
}
public class Enemy
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public double CritChance { get; set; }
    public double FreezeChance { get; set; }
    public bool IgnoreArmor { get; set; }
    protected Random random;
    public Enemy(string name, string type, int health, int attack, int defense) // Конструктор класса Enemy
    {
        Name = name;
        Type = type;
        MaxHealth = health;
        Health = MaxHealth;
        Attack = attack;
        Defense = defense;
        CritChance = 0;
        FreezeChance = 0;
        IgnoreArmor = false;
        random = new Random();
    }

    public virtual void AttackPlayer(Player player) // Виртуальный метод атаки игрока
    {
       if (player.IsFrozen)
       {
            Console.WriteLine($"{Name} атакует, но игрок заморожен и не может действовать!");
            player.IsFrozen = false;
            return;
       }
       int damage = Attack;
       if (random.NextDouble() < CritChance) // Проверка на критический удар
       {
            damage = (int)(damage * 1.5);
            Console.WriteLine($"{Name} наносит критический удар!");
       }
       ApplySpecialAbility(player); // Применение особой способности врага
       int finalDamage = IgnoreArmor ? damage : Math.Max(1, damage - player.GetTotalDefense() / 2); // Игнорирование брони или уменьшение урона
       Console.WriteLine($"{Name} атакует и наносит {finalDamage} урона!");
       player.TakeDamage(finalDamage);
    }
    protected virtual void ApplySpecialAbility(Player player)  // Виртуальный метод применения особой способности
    {
        // Базовая реализация без особых способностей
    }
    public void TakeDamage(int damage) // Метод получения урона врагом
    {
        Health = Math.Max(0, Health -  damage);
        Console.WriteLine($"{Name} получает {damage} урона. HP:{Health}/{MaxHealth}");
    }
    public bool IsAlive() // Метод проверки, жив ли враг
    {
        return Health > 0;
    }
}
public class Goblin: Enemy
{
    // Конструктор гоблина с базовыми характеристиками
    public Goblin() 
        : base("Гоблин", "Гоблин", 30, 8, 3)
    {
        CritChance = 0.2; // 20% шанс крит. урона
    }
}
public class Skeleton: Enemy
{
    // Конструктор скелета с базовыми характеристиками
    public Skeleton() 
        : base("Скелет", "Скелет", 25, 10, 2)
    {
        IgnoreArmor = true; // Скелет игнорирует броню игрока

    }
    protected override void ApplySpecialAbility(Player player) // Переопределенный метод особой способности скелета
    {
        Console.WriteLine($"{Name} игнорирует защиту игрока!");
    }
}
public class Mage: Enemy
{
    // Конструктор мага с базовыми характеристиками
    public Mage() 
        : base("Маг", "Маг", 20, 12, 1)
    {
        FreezeChance = 0.25; // 25% шанс заморозки игрока
    }
    protected override void ApplySpecialAbility(Player player) // Переопределенный метод особой способности мага
    {
        if (random.NextDouble() < FreezeChance) // Проверка срабатывания заморозки
        {
            player.IsFrozen = true;
            Console.WriteLine($"{Name} замораживает игрока! Игрок пропустит следующий ход.");
        }
    }
}
public class Boss: Enemy
{
    public string BossType { get; set; }
    // Конструктор босса
    public Boss(string name, string bossType, int health, int attack, int defense)
        : base(name, "Босс", health, attack, defense)
    {
        BossType = bossType;
    }
}
public class VVG : Boss
{
    // Конструктор босса ВВГ с усиленными характеристиками
    public VVG() 
        : base("ВВГ", "ВВГ", 60, 12, 4)
    {
        CritChance = 0.3; // 30% шанс крит. урона (обычный гоблин 20% + 10%)
    }
}
public class Kovalsky: Boss
{
    // Конструктор босса Ковальского с усиленными характеристиками
    public Kovalsky() 
        : base("Ковальский", "Ковальский", 63, 13, 3)
    {
        IgnoreArmor = true;
    }
    protected override void ApplySpecialAbility(Player player) // Переопределенный метод особой способности
    {
        Console.WriteLine($"{Name} полностью игнорирует защиту игрока!");
    }
}
public class ArchimageCPP: Boss
{
    // Конструктор босса Архимаг C++ с усиленными характеристиками
    public ArchimageCPP() 
        : base("Архимаг C++", "Архимаг C++", 36, 19, 2)
    {
        FreezeChance = 0.35; // 35% шанс заморозки (обычный маг 25% + 10%)
    }
    protected override void ApplySpecialAbility(Player player) // Переопределенный метод особой способности
    {
        if (random.NextDouble() < FreezeChance)
        {
            player.IsFrozen = true;
            Console.WriteLine($"{Name} замораживает игрока магией C++! Игрок пропустит следующий ход.");
        }
    }
}
public class PestovCMM: Boss
{
    // Конструктор босса Пестов С-- с гибридными характеристиками
    public PestovCMM() 
        : base("Пестов С--", "Пестов С--", 33, 18, 1)
    {
        IgnoreArmor = true;
        FreezeChance = 0.4; // 40% шанс заморозки (обычный маг 25% + 15%)
    }
    protected override void ApplySpecialAbility(Player player) // Переопределенный метод особой способности
    {
        Console.WriteLine($"{Name} игнорирует защиту игрока!");
        if (random.NextDouble() < FreezeChance)
        {
            player.IsFrozen = true;
            Console.WriteLine($"{Name} замораживает игрока магией С--! Игрок пропустит следующий ход.");
        }
    }
}
public class Chest
{
    public Item Contents { get; set; }
    private Random random;
    public Chest() // Конструктор сундука
    {
        random = new Random();
        Contents = GenerateRandomItem();
    }
    public Item Open() // Метод открытия сундука
    {
        Console.WriteLine("Вы нашли сундук!");
        Console.WriteLine($"Внутри: {Contents.Name} (ATK/DF:{Contents.AttackBonus}/{Contents.DefenseBonus})");
        return Contents;
    }
    private Item GenerateRandomItem() // Приватный метод генерации случайного предмета
    {
        var itemTypes = new List<string> { "Оружие", "Доспехи", "Зелье" };
        string selectedType = itemTypes[random.Next(itemTypes.Count)]; // Случайный выбор типа предмета
        switch (selectedType)
        {
            case "Оружие":
                    return GenerateRandomWeapon();                
            case "Доспехи":                
                    return GenerateRandomArmor();
            default: 
                return new Potion("Зелье лечения");
        }
    }
    private Weapon GenerateRandomWeapon() // Приватный метод генерации случайного оружия
    {
        var weapons = new List<Weapon>
        {
            new Weapon("Кинжал", 8),
            new Weapon("Меч", 12),
            new Weapon("Топор", 15),
            new Weapon("Посох мага", 10),
            new Weapon("Лук", 11),
            new Weapon("Булава", 13),
            new Weapon("Рапира", 14),
            new Weapon("Двуручный меч", 18)
        };
        return weapons[random.Next(weapons.Count)]; // Возврат случайного оружия из списка
    }
    private Armor GenerateRandomArmor() // Приватный метод генерации случайных доспехов
    {
        var armors = new List<Armor>
        {
            new Armor("Кожаная броня", 6),
            new Armor("Кольчуга", 10),
            new Armor("Латные доспехи", 15),
            new Armor("Мантия мага", 8),
            new Armor("Щит", 12),
            new Armor("Драконья чешуя", 20),
            new Armor("Броня костяного стража", 16)
        };
        return armors[random.Next(armors.Count)]; // Возврат случайных доспехов из списка
    }
}
public class Game
{
    public void StartGame() // Основной метод запуска игры
    {

    }
    private void GameLoop() // Основной игровой цикл
    {

    }
    private void HandleTurn() // Метод обработки одного хода
    {

    }
    private void HandleEnemy() // Метод обработки встречи с обычным врагом
    {

    }    
    private void HandleBoss() // Метод обработки встречи с боссом
    {

    }
    private void HandleChest() // Метод обработки найденного сундука
    {

    }
    private void OfferEquipmentChoice(Item newItem) // Метод предложения выбора экипировки
    {

    }
    private void Battle(Enemy enemy) // Метод проведения боя
    {

    }
    private void ShowBattleMenu() // Метод отображения меню боя
    {

    }
    private Enemy GenerateRandomEnemy() // Метод генерации случайного обычного врага
    {

    }
    private Enemy GenerateRandomBoss() // Метод генерации случайного босса
    {

    }
}
class Program // Главный класс программы
{
    static void Main(string[] args)
    {
        Game game = new Game(); // Создание экземпляра игры
        game.StartGame(); // Запуск игры
        Console.WriteLine("\nСпасибо за игру!"); 
        Console.WriteLine("Нажмите любую клавишу для выхода"); 
        Console.ReadKey(); 
    }
}