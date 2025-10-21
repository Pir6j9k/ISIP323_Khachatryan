public class Item
{
        
    public Item(string name, string type, int attackBonus = 0, int defenseBonus = 0) // Конструктор класса Item для инициализации свойств
    {
        
    }
    public virtual void Use(Player player) // Виртуальный метод использования предмета 
    {

    }    
    public virtual void ShowStats() // Виртуальный метод отображения статистики предмета
    {
        
    }
}
public class Weapon: Item
{

}
public class Armor: Item
{

}
public class Potion: Item
{

}
public class Player
{

}
public class Enemy
{

}
public class Goblin: Enemy
{

}
public class Skeleton: Enemy
{

}
public class Mage: Enemy
{

}
public class Boss: Enemy
{

}
public class VVG : Boss
{

}
public class Kovalsky: Boss
{

}
public class ArchimageCPP: Boss
{

}
public class PestovCMM: Boss
{

}
public class Chest
{

}
public class Game
{

}
class Program
{

}