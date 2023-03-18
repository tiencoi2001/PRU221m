public class Group1Factory : GroupFactory
{
    public override string[] CreateEnemy()
    {
        return new string[] { "goblin1", "golem1", "vampire1" };
    }
}